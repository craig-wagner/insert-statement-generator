#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
#endregion using

namespace Wagner.InsertStatementGenerator
{
    public partial class StatusForm : Form
    {
        #region Fields
        private readonly bool generateDependencies = false;
        private readonly Table[] tables;
        private readonly Dictionary<string, DataTable> filters;
        private readonly Server server = null;
        private readonly Database db = null;
        private readonly TextWriter output = null;
        private ScriptGenerator generator;
        private Thread generatorThread = null;
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

        public ScriptGenerator ScriptGenerator
        {
            get { return generator; }
        }
        #endregion Properties

        #region Constructors
        public StatusForm( TextWriter output, Server server, Database db, Table[] tables, Dictionary<string, DataTable> filters, bool generateDependencies )
        {
            InitializeComponent();

            this.output = output;
            this.server = server;
            this.db = db;
            this.tables = tables;
            this.filters = filters;
            this.generateDependencies = generateDependencies;
        }
        #endregion Constructors

        #region Private Methods
        private void ReportStatus( string tableName, string rowCount )
        {
            if( !txtTable.InvokeRequired )
            {
                txtTable.Text = tableName;
                txtRowCount.Text = rowCount;
            }
            else if( !this.IsDisposed )
            {
                ReportStatusDelegate reportStatus = new ReportStatusDelegate( ReportStatus );
                this.Invoke( reportStatus, new object[] { tableName, rowCount } );
            }
        }

        private void StatementGenerationComplete()
        {
            if( !this.InvokeRequired )
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if( !this.IsDisposed )
            {
                SignalCompleteDelegate signalComplete = new SignalCompleteDelegate( StatementGenerationComplete );
                this.BeginInvoke( signalComplete );
            }
        }
        #endregion Private Methods

        #region Event Handlers
        private void StatusForm_Load( object sender, EventArgs e )
        {
            generator = new ScriptGenerator( 
                output, server, db, tables, filters, generateDependencies, 
                ReportStatus, StatementGenerationComplete );

            generatorThread = new Thread( new ThreadStart( generator.GenerateScript ) );
            generatorThread.Start();
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void StatusForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            if( generatorThread.IsAlive && generator.ContinueProcessing )
            {
                generator.ContinueProcessing = false;
                generatorThread.Join();
            }
        }
        #endregion Event Handlers
    }
}
