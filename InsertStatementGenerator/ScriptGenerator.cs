#region using
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
#endregion using

namespace Wagner.InsertStatementGenerator
{
    #region Delegates
    public delegate void ReportStatusDelegate( string tableName, string rowCount );
    public delegate void SignalCompleteDelegate();
    #endregion Delegates

    public class ScriptGenerator
    {
        #region Fields
        private readonly bool generateDependencies = false;
        private readonly Table[] tables;
        private readonly Dictionary<string, DataTable> filters = null;
        private readonly Server server = null;
        private readonly Database db = null;
        private readonly TextWriter output = null;
        private readonly ReportStatusDelegate reportStatus;
        private readonly SignalCompleteDelegate signalComplete;
        private SqlConnection connection = null;
        private bool continueProcessing = true;
        private StringBuilder script = null;
        private object theLock = new object();
        #endregion Fields

        #region Properties
        public bool ContinueProcessing
        {
            get
            {
                lock( theLock )
                {
                    return continueProcessing;
                }
            }
            set
            {
                lock( theLock )
                {
                    continueProcessing = value;
                }
            }
        }

        public string Script
        {
            get
            {
                lock( theLock )
                {
                    return script.ToString();
                }
            }
        }
        #endregion Properties

        #region Constructors
        public ScriptGenerator( TextWriter output, Server server, Database db, Table[] tables, Dictionary<string, DataTable> filters, bool generateDependencies, ReportStatusDelegate reportStatus, SignalCompleteDelegate signalComplete )
        {
            this.output = output;
            this.server = server;
            this.db = db;
            this.tables = tables;
            this.filters = filters;
            this.generateDependencies = generateDependencies;
            this.reportStatus = reportStatus;
            this.signalComplete = signalComplete;
        }
        #endregion Constructors

        #region Public Methods
        public void GenerateScript()
        {
            try
            {
                connection = new SqlConnection(
                    server.ConnectionContext.ConnectionString + ";Initial Catalog=" + db.Name );
                connection.Open();

                // Get the dependencies for the selected tables
                DependencyWalker walker = new DependencyWalker( server );
                DependencyTree dependencyTree = walker.DiscoverDependencies( tables, DependencyType.Parents );
                DependencyCollection dependencyList = walker.WalkDependencies( dependencyTree );

                // Loop over the tables in dependency order when generating the INSERT
                // statements
                foreach( DependencyCollectionNode node in dependencyList )
                {
                    if( !continueProcessing )
                        break;

                    if( node.Urn.Type.ToLower() == "table" )
                    {
                        for( int i = 0; i < tables.Length && continueProcessing; i++ )
                        {
                            // The dependencies retrieved above include may include tables 
                            // other than the ones selected by the user, so we need to check 
                            // each one to ensure it was selected
                            if( generateDependencies || node.Urn == tables[i].Urn )
                            {
                                Table table = server.GetSmoObject( node.Urn ) as Table;

                                CallReportStatus( table.ToString(), "Loading..." );

                                GenerateTableInserts( table );

                                break;
                            }
                        }
                    }
                }

                if( continueProcessing )
                    CallSignalComplete();
            }
            finally
            {
                if( connection != null )
                    connection.Dispose();
            }
        }
        #endregion Public Methods

        #region Private Methods
        private void CallReportStatus( string table, string row )
        {
            if( reportStatus != null )
                reportStatus( table, row );
        }

        private void CallSignalComplete()
        {
            if( signalComplete != null )
                signalComplete();
        }

        private bool HasIdentityColumn( Table table )
        {
            bool hasIdentityColumn = false;

            foreach( Column column in table.Columns )
            {
                if( column.Identity )
                {
                    hasIdentityColumn = true;
                    break;
                }
            }

            return hasIdentityColumn;
        }

        private bool IsInsertableType( string type )
        {
            bool isInsertableType = true;

            if( type == "timestamp" )
            {
                isInsertableType = false;
            }

            return isInsertableType;
        }

        private void GenerateTableInserts( Table table )
        {
            SqlDataReader reader = null;
            StringBuilder columnList = new StringBuilder( 50000 );
            StringBuilder valueList = new StringBuilder( 50000 );
            StringBuilder insertStatement = new StringBuilder( 50000 );
            int rowCount = 0;

            try
            {
                bool hasIdentityColumn = HasIdentityColumn( table );

                // Get all rows of data for the table from the database 
                reader = GetReaderForTable( table );

                if( reader.HasRows && continueProcessing )
                {
                    columnList.AppendFormat( "INSERT INTO {0} (", table );

                    for( int i = 0; i < reader.FieldCount; i++ )
                    {
                        Column column = table.Columns[i];

                        if( IsInsertableType( column.DataType.Name ) )
                        {
                            columnList.Append( column.ToString() );
                            if( i < table.Columns.Count - 1 )
                                columnList.Append( ", " );
                        }
                    }

                    // End of "INSERT INTO TABLE (Column1, Column2, ...)"
                    columnList.Append( ")" );

                    // Write out the 'header' statements for the table inserts
                    output.Write( "PRINT 'INSERTING DATA INTO TABLE " );
                    output.Write( table.ToString() );
                    output.Write( "'" );
                    output.Write( Environment.NewLine );

                    // Disable constraint checking so that foreign key constraints 
                    // do not hinder bulk insert operations
                    output.Write( "ALTER TABLE " );
                    output.Write( table.ToString() );
                    output.Write( " NOCHECK CONSTRAINT ALL" );
                    output.Write( Environment.NewLine ); // Disable all constraints

                    // Disable identity columns insertion
                    if( hasIdentityColumn )
                    {
                        output.Write( "SET IDENTITY_INSERT " );
                        output.Write( table.ToString() );
                        output.Write( " ON" );
                        output.Write( Environment.NewLine );
                    }

                    while( reader.Read() && continueProcessing )
                    {
                        rowCount++;

                        CallReportStatus( table.ToString(), rowCount.ToString() );

                        valueList = new StringBuilder( 50000 );

                        valueList.Append( " VALUES (" );

                        for( int i = 0; i < reader.FieldCount; i++ )
                        {
                            if( IsInsertableType( reader.GetDataTypeName( i ) ) )
                            {
                                valueList.Append(
                                    GetValueForTableInsertStatement( reader.GetDataTypeName( i ), reader[i] ) );

                                if( i != reader.FieldCount - 1 )
                                {
                                    valueList.Append( ", " );
                                }
                            }
                        }

                        valueList.Append( ")" );

                        // Combine the ColumnNames and Values section of the INSERT statement
                        output.Write( columnList );
                        output.Write( valueList );
                        output.Write( Environment.NewLine );
                    }

                    if( reader != null && !reader.IsClosed )
                        reader.Close();

                    // Write out the 'footer' statements for the table inserts

                    // Disable identity columns insertion
                    if( hasIdentityColumn )
                    {
                        output.Write( "SET IDENTITY_INSERT " );
                        output.Write( table.ToString() );
                        output.Write( " OFF" );
                        output.Write( Environment.NewLine );
                    }

                    // Turn constraint checking back on
                    output.Write( "ALTER TABLE " );
                    output.Write( table.ToString() );
                    output.Write( " CHECK CONSTRAINT ALL" );
                    output.Write( Environment.NewLine );
                    output.Write( Environment.NewLine );
                }
            }
            finally
            {
                if( reader != null )
                    reader.Dispose();
            }
        }

        private string GetValueForTableInsertStatement( string dataTypeName, object objectValue )
        {
            DateTime dtValue;
            string valueToInsert = String.Empty;
            string originalValue = objectValue.ToString();

            if( objectValue == DBNull.Value )
            {
                valueToInsert = "NULL";
            }
            else
            {
                switch( dataTypeName.ToLower() )
                {
                    // Numberic types
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
                        valueToInsert = "'" + originalValue.Replace( "'", "''" ) + "'";
                        break;

                    // DateTime types
                    case "datetime":
                    case "smalldatetime":
                        dtValue = Convert.ToDateTime( originalValue );
                        valueToInsert = "'" + dtValue.ToString( "yyyy/MM/dd hh:mm:ss tt" ) + "'";
                        break;

                    case "bit":
                        valueToInsert = Convert.ToInt16( Convert.ToBoolean( originalValue ) ).ToString();
                        break;

                    case "binary":
                    case "varbinary":
                    case "image":
                        StringBuilder hexString = new StringBuilder( 50000 );
                        byte[] theBytes = (byte[])objectValue;

                        for( int i = 0; i < theBytes.Length; i++ )
                            hexString.Append( theBytes[i].ToString( "X2" ) );

                        valueToInsert = "'0x" + hexString.ToString().ToUpper() + "'";
                        break;

                    default:
                        valueToInsert = "<<NOT IMPLEMENTED>>";
                        break;
                }
            }

            return valueToInsert;
        }

        private SqlDataReader GetReaderForTable( Table table )
        {
            StringBuilder sql = new StringBuilder( 50000 );
            StringBuilder columns = new StringBuilder( 50000 );
            StringBuilder where = new StringBuilder( 50000 );

            foreach( Column column in table.Columns )
            {
                if( columns.Length > 0 )
                    columns.Append( ", " );

                columns.Append( column.ToString() );
            }

            sql.Append( "SELECT " );
            sql.Append( columns );

            sql.AppendFormat( " FROM {0}", table );

            if( filters != null && filters.ContainsKey( table.Name ) && filters[table.Name] != null )
            {
                foreach( DataRow row in filters[table.Name].Rows )
                {
                    if( row[2] != DBNull.Value && row[2].ToString().Length > 0 )
                    {
                        if( where.Length > 0 )
                            where.Append( " and " );

                        string datatype = row[1].ToString();

                        if( datatype == "varchar" || datatype == "nvarchar" || datatype == "char" || datatype == "nchar" )
                            row[2] = "'" + row[2] + "'";

                        where.AppendFormat( "{0} = {1}", row[0], row[2] );
                    }
                }
            }

            if( where.Length > 0 )
                sql.AppendFormat( " WHERE {0}", where );

            SqlCommand command = new SqlCommand( sql.ToString(), connection );

            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }

        private bool IsBinaryData( string dataTypeName )
        {
            const string binaryTypes = "image,varbinary";

            bool isBinaryData = false;

            if( binaryTypes.Contains( dataTypeName ) )
            {
                isBinaryData = true;
            }

            return isBinaryData;
        }
        #endregion Private Methods
    }
}
