#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;

#endregion using

namespace Wagner.InsertStatementGenerator
{
    public partial class StatusForm : Form
    {
        #region Fields

        private readonly bool _generateDependencies = false;
        private readonly Table[] _tables;
        private readonly Dictionary<string, DataTable> _filters;
        private readonly Server _server = null;
        private readonly Database _db = null;
        private readonly TextWriter _output = null;
        private Thread _generatorThread = null;

        #endregion Fields

        #region Properties

        public string TableName
        {
            set { txtTable.Text = value; }
        }

        public string RowCount
        {
            set { txtRowCount.Text = value; }
        }

        public ScriptGenerator ScriptGenerator { get; private set; }

        #endregion Properties

        #region Constructors

        public StatusForm(TextWriter output, Server server, Database db, Table[] tables, Dictionary<string, DataTable> filters, bool generateDependencies)
        {
            InitializeComponent();

            _output = output;
            _server = server;
            _db = db;
            _tables = tables;
            _filters = filters;
            _generateDependencies = generateDependencies;
        }

        #endregion Constructors

        #region Private Methods

        private void ReportStatus(string tableName, string rowCount)
        {
            if (!txtTable.InvokeRequired)
            {
                txtTable.Text = tableName;
                txtRowCount.Text = rowCount;
            }
            else if (!IsDisposed)
            {
                var reportStatus = new ReportStatusDelegate(ReportStatus);
                Invoke(reportStatus, new object[] { tableName, rowCount });
            }
        }

        private void StatementGenerationComplete()
        {
            if (!InvokeRequired)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (!IsDisposed)
            {
                var signalComplete = new SignalCompleteDelegate(StatementGenerationComplete);
                BeginInvoke(signalComplete);
            }
        }

        #endregion Private Methods

        #region Event Handlers

        private void StatusForm_Load(object sender, EventArgs e)
        {
            ScriptGenerator = new ScriptGenerator(
                _output, _server, _db, _tables, _filters, _generateDependencies,
                ReportStatus, StatementGenerationComplete);

            _generatorThread = new Thread(new ThreadStart(ScriptGenerator.GenerateScript));
            _generatorThread.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void StatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_generatorThread.IsAlive && ScriptGenerator.ContinueProcessing)
            {
                ScriptGenerator.ContinueProcessing = false;
                _generatorThread.Join();
            }
        }

        #endregion Event Handlers
    }
}