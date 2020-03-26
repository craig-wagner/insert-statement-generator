using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace Wagner.InsertStatementGenerator
{
    #region Delegates

    public delegate void ReportStatusDelegate(string tableName, string rowCount);

    public delegate void SignalCompleteDelegate();

    #endregion Delegates

    public class ScriptGenerator
    {
        #region Fields

        private readonly bool _generateDependencies = false;
        private readonly Table[] _tables;
        private readonly Dictionary<string, DataTable> _filters = null;
        private readonly Server _server = null;
        private readonly Database _db = null;
        private readonly TextWriter _output = null;
        private readonly ReportStatusDelegate _reportStatus;
        private readonly SignalCompleteDelegate _signalComplete;
        private readonly StringBuilder _script = null;
        private readonly object _theLock = new object();
        private SqlConnection _connection = null;
        private bool _continueProcessing = true;

        #endregion Fields

        #region Properties

        public bool ContinueProcessing
        {
            get
            {
                lock (_theLock)
                {
                    return _continueProcessing;
                }
            }
            set
            {
                lock (_theLock)
                {
                    _continueProcessing = value;
                }
            }
        }

        public string Script
        {
            get
            {
                lock (_theLock)
                {
                    return _script.ToString();
                }
            }
        }

        #endregion Properties

        #region Constructors

        public ScriptGenerator(TextWriter output, Server server, Database db, Table[] tables,
            Dictionary<string, DataTable> filters, bool generateDependencies, ReportStatusDelegate reportStatus,
            SignalCompleteDelegate signalComplete)
        {
            _output = output;
            _server = server;
            _db = db;
            _tables = tables;
            _filters = filters;
            _generateDependencies = generateDependencies;
            _reportStatus = reportStatus;
            _signalComplete = signalComplete;
        }

        #endregion Constructors

        #region Public Methods

        public void GenerateScript()
        {
            try
            {
                _connection = new SqlConnection($"{_server.ConnectionContext.ConnectionString};Initial Catalog={_db.Name}");
                _connection.Open();

                // Get the dependencies for the selected tables
                var walker = new DependencyWalker(_server);
                var dependencyTree = walker.DiscoverDependencies(_tables, DependencyType.Parents);
                var dependencyList = walker.WalkDependencies(dependencyTree);

                // Loop over the tables in dependency order when generating the INSERT statements
                foreach (DependencyCollectionNode node in dependencyList)
                {
                    if (!_continueProcessing)
                    {
                        break;
                    }

                    if (node.Urn.Type.ToLower() == "table")
                    {
                        for (var i = 0; i < _tables.Length && _continueProcessing; i++)
                        {
                            // The dependencies retrieved above include may include tables
                            // other than the ones selected by the user, so we need to check
                            // each one to ensure it was selected
                            if (_generateDependencies || node.Urn == _tables[i].Urn)
                            {
                                var table = _server.GetSmoObject(node.Urn) as Table;

                                CallReportStatus(table.ToString(), "Loading...");

                                GenerateTableInserts(table);

                                break;
                            }
                        }
                    }
                }

                if (_continueProcessing)
                {
                    CallSignalComplete();
                }
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void CallReportStatus(string table, string row) => _reportStatus?.Invoke(table, row);

        private void CallSignalComplete() => _signalComplete?.Invoke();

        private bool HasIdentityColumn(Table table)
        {
            var hasIdentityColumn = false;

            foreach (Column column in table.Columns)
            {
                if (column.Identity)
                {
                    hasIdentityColumn = true;
                    break;
                }
            }

            return hasIdentityColumn;
        }

        private bool IsInsertableType(string type)
        {
            var isInsertableType = true;

            if (type == "timestamp")
            {
                isInsertableType = false;
            }

            return isInsertableType;
        }

        private void GenerateTableInserts(Table table)
        {
            SqlDataReader reader = null;

            try
            {
                var hasIdentityColumn = HasIdentityColumn(table);

                // Get all rows of data for the table from the database
                reader = GetReaderForTable(table);

                if (reader.HasRows && _continueProcessing)
                {
                    var columnList = new StringBuilder($"INSERT INTO {table} (");

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var column = table.Columns[i];

                        if (IsInsertableType(column.DataType.Name))
                        {
                            columnList.Append(column.ToString());
                            if (i < table.Columns.Count - 1)
                            {
                                columnList.Append(", ");
                            }
                        }
                    }

                    // End of "INSERT INTO TABLE (Column1, Column2, ...)"
                    columnList.Append(")");

                    // Write out the 'header' statements for the table inserts
                    _output.Write("PRINT 'INSERTING DATA INTO TABLE ");
                    _output.Write(table.ToString());
                    _output.Write("'");
                    _output.Write(Environment.NewLine);

                    // Disable constraint checking so that foreign key constraints
                    // do not hinder bulk insert operations
                    _output.Write("ALTER TABLE ");
                    _output.Write(table.ToString());
                    _output.Write(" NOCHECK CONSTRAINT ALL");
                    _output.Write(Environment.NewLine); // Disable all constraints

                    // Disable identity columns insertion
                    if (hasIdentityColumn)
                    {
                        _output.Write("SET IDENTITY_INSERT ");
                        _output.Write(table.ToString());
                        _output.Write(" ON");
                        _output.Write(Environment.NewLine);
                    }

                    var rowCount = 0;
                    while (reader.Read() && _continueProcessing)
                    {
                        rowCount++;

                        CallReportStatus(table.ToString(), rowCount.ToString());

                        var valueList = new StringBuilder(50000);

                        valueList.Append(" VALUES (");

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            if (IsInsertableType(reader.GetDataTypeName(i)))
                            {
                                valueList.Append(GetValueForTableInsertStatement(reader.GetDataTypeName(i), reader[i]));

                                if (i != reader.FieldCount - 1)
                                {
                                    valueList.Append(", ");
                                }
                            }
                        }

                        valueList.Append(")");

                        // Combine the ColumnNames and Values section of the INSERT statement
                        _output.Write(columnList);
                        _output.Write(valueList);
                        _output.Write(Environment.NewLine);
                    }

                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }

                    // Write out the 'footer' statements for the table inserts

                    // Disable identity columns insertion
                    if (hasIdentityColumn)
                    {
                        _output.Write("SET IDENTITY_INSERT ");
                        _output.Write(table.ToString());
                        _output.Write(" OFF");
                        _output.Write(Environment.NewLine);
                    }

                    // Turn constraint checking back on
                    _output.Write("ALTER TABLE ");
                    _output.Write(table.ToString());
                    _output.Write(" CHECK CONSTRAINT ALL");
                    _output.Write(Environment.NewLine);
                    _output.Write(Environment.NewLine);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        private string GetValueForTableInsertStatement(string dataTypeName, object objectValue)
        {
            DateTime dtValue;
            var originalValue = objectValue.ToString();
            string valueToInsert;

            if (objectValue == DBNull.Value)
            {
                valueToInsert = "NULL";
            }
            else
            {
                switch (dataTypeName.ToLower())
                {
                    // Numeric types
                    case "bigint":
                    case "int":
                    case "smallint":
                    case "tinyint":
                    case "decimal":
                    case "numeric":
                    case "money":
                    case "smallmoney":
                    case "float":
                    case "real":
                        valueToInsert = originalValue;
                        break;

                    // Character/String types
                    case "char":
                    case "varchar":
                    case "text":
                    case "nchar":
                    case "nvarchar":
                    case "ntext":
                    case "uniqueidentifier":
                    case "xml":
                        valueToInsert = "'" + originalValue.Replace("'", "''") + "'";
                        break;

                    // DateTime types
                    case "datetime":
                    case "datetime2":
                    case "smalldatetime":
                        dtValue = Convert.ToDateTime(originalValue);
                        valueToInsert = "'" + dtValue.ToString("yyyy/MM/dd hh:mm:ss tt") + "'";
                        break;

                    case "bit":
                        valueToInsert = Convert.ToInt16(Convert.ToBoolean(originalValue)).ToString();
                        break;

                    case "binary":
                    case "varbinary":
                    case "image":
                        var hexString = new StringBuilder(50000);
                        var theBytes = (byte[])objectValue;

                        for (int i = 0; i < theBytes.Length; i++)
                            hexString.Append(theBytes[i].ToString("X2"));

                        valueToInsert = "'0x" + hexString.ToString().ToUpper() + "'";
                        break;

                    default:
                        valueToInsert = "<<NOT IMPLEMENTED>>";
                        break;
                }
            }

            return valueToInsert;
        }

        private SqlDataReader GetReaderForTable(Table table)
        {
            var sql = new StringBuilder(50000);
            var columns = new StringBuilder(50000);
            var where = new StringBuilder(50000);

            foreach (Column column in table.Columns)
            {
                if (columns.Length > 0)
                {
                    columns.Append(", ");
                }

                columns.Append(column.ToString());
            }

            sql.Append("SELECT ");
            sql.Append(columns);

            sql.AppendFormat(" FROM {0}", table);

            if (_filters != null && _filters.ContainsKey(table.Name) && _filters[table.Name] != null)
            {
                foreach (DataRow row in _filters[table.Name].Rows)
                {
                    if (row[2] != DBNull.Value && row[2].ToString().Length > 0)
                    {
                        if (where.Length > 0)
                        {
                            where.Append(" and ");
                        }

                        var datatype = row[1].ToString();

                        if (datatype == "varchar" || datatype == "nvarchar" || datatype == "char" || datatype == "nchar")
                        {
                            row[2] = "'" + row[2] + "'";
                        }

                        where.AppendFormat("{0} = {1}", row[0], row[2]);
                    }
                }
            }

            if (where.Length > 0)
            {
                sql.AppendFormat(" WHERE {0}", where);
            }

            var command = new SqlCommand(sql.ToString(), _connection);

            return command.ExecuteReader();
        }

        #endregion Private Methods
    }
}