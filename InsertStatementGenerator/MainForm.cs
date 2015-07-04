#region using
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;
#endregion

namespace Wagner.InsertStatementGenerator
{
    public partial class MainForm : Form
    {
        #region Constants
        private const string regKey = @"Software\InsertStatementGenerator";
        private const string defaultTitle = "Insert Statement Generator";
        private const string notConnected = "(Not Connected)";
        #endregion

        #region Fields
        private Server server = null;
        private Database db = null;
        private string connectString = String.Empty;
        private Dictionary<string, DataTable> filters = null;
        #endregion Fields

        #region Constructors
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Event Handlers
        private void DoConnect( object sender, System.EventArgs e )
        {
            Connect();
        }

        private void DoExit( object sender, System.EventArgs e )
        {
            this.Close();
        }

        private void MainForm_Closing( object sender, FormClosingEventArgs e )
        {
            Disconnect();

            try
            {
                RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                RegistryKey appkey;

                appkey = hkcu.CreateSubKey( regKey );

                if( this.WindowState == FormWindowState.Normal )
                {
                    appkey.SetValue( "Left", this.Left );
                    appkey.SetValue( "Top", this.Top );
                    appkey.SetValue( "Width", this.Width );
                    appkey.SetValue( "Height", this.Height );
                }
            }
            catch( Exception ex )
            {
                string msg = "There was a problem saving your settings to the registry.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
        }

        private void DoDisconnect( object sender, System.EventArgs e )
        {
            Disconnect();
        }

        private void DoAbout( object sender, System.EventArgs e )
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void lstDatabases_SelectedIndexChanged( object sender, System.EventArgs e )
        {
            try
            {
                db = null;

                if( lstDatabases.SelectedIndex > -1 )
                {
                    db = (Database)lstDatabases.SelectedItem;

                    GetTables( db );
                }
            }
            catch( Exception ex )
            {
                string msg = "Unable to get list of tables from the selected database.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
        }

        private void lstTables_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( e.NewValue == CheckState.Checked )
            {
                btnGenerate.Enabled = true;
                btnApplyFilters.Enabled = true;
            }
            else if( lstTables.CheckedItems.Count == 1 )
            {
                btnGenerate.Enabled = false;
                btnApplyFilters.Enabled = false;
            }
        }

        private void btnGenerate_Click( object sender, System.EventArgs e )
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                GenerateScript();
            }
            catch( Exception ex )
            {
                string msg = "Unable to generate script for the selected tables.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MainForm_Load( object sender, System.EventArgs e )
        {
            try
            {
                RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                RegistryKey appkey = hkcu.OpenSubKey( regKey );

                if( appkey != null )
                {
                    int left = (int)appkey.GetValue( "Left", this.Left );
                    int top = (int)appkey.GetValue( "Top", this.Top );
                    int width = (int)appkey.GetValue( "Width", this.Width );
                    int height = (int)appkey.GetValue( "Height", this.Height );

                    this.Location = new Point( left, top );
                    this.Size = new Size( width, height );
                }
            }
            catch( Exception ex )
            {
                string msg = "There was a problem getting your saved settings from the registry.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
        }

        private void btnSelectAll_Click( object sender, EventArgs e )
        {
            SetTableSelectState( true );
        }

        private void btnDeselectAll_Click( object sender, EventArgs e )
        {
            SetTableSelectState( false );
        }

        private void btnApplyFilters_Click( object sender, EventArgs e )
        {
            ApplyFilters();
        }
        #endregion Event Handlers

        #region Private Methods
        private void ApplyFilters()
        {
            try
            {
                Table[] tables = GetSelectedTables();

                if( filters == null )
                    filters = new Dictionary<string, DataTable>( tables.Length );

                FilterForm filterForm = new FilterForm( tables, filters );

                filterForm.ShowDialog();
            }
            catch( Exception ex )
            {
                string msg = "Problem saving filter information.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
        }

        private void SetTableSelectState( bool state )
        {
            for( int i = 0; i < lstTables.Items.Count; i++ )
                lstTables.SetItemChecked( i, state );
        }

        private void GetDatabases()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                lstDatabases.Items.Clear();

                foreach( Database db in server.Databases )
                {
                    if( !db.IsSystemObject )
                    {
                        lstDatabases.Items.Add( db );
                    }
                }
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void GetTables( Database db )
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                lstTables.Items.Clear();

                foreach( Table table in db.Tables )
                {
                    if( !table.IsSystemObject )
                    {
                        lstTables.Items.Add( table );
                    }
                }

                lstTables.Enabled = true;

                btnGenerate.Enabled = false;
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void Connect()
        {
            try
            {
                LogonForm logon = new LogonForm();
                logon.ShowDialog( this );

                if( logon.DialogResult == DialogResult.OK )
                {
                    server = logon.Server;

                    GetDatabases();

                    this.Text = String.Format( "{0} ({1})", defaultTitle, server.Name );

                    lstDatabases.Enabled = true;
                    toolStripMenuItemDisconnect.Enabled = true;
                    toolStripButtonDisconnect.Enabled = true;

                    // The table filters that might have been set before no
                    // longer apply because the database was changed
                    filters = null;
                }
            }
            catch( Exception ex )
            {
                string msg = "Unable to load the list of databases on the server.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
        }

        private void Disconnect()
        {
            if( server != null )
                server.ConnectionContext.Disconnect();

            this.Text = String.Format( "{0} {1}", defaultTitle, notConnected );

            lstTables.Items.Clear();
            lstTables.Enabled = false;
            lstDatabases.Items.Clear();
            lstDatabases.Enabled = false;
            btnGenerate.Enabled = false;
            toolStripMenuItemDisconnect.Enabled = false;
            toolStripButtonDisconnect.Enabled = false;
        }

        private void GenerateScript()
        {
            StreamWriter writer = null;

            try
            {
                DialogResult result = dlgFileSave.ShowDialog();

                if( result == DialogResult.OK )
                {
                    writer = new StreamWriter( dlgFileSave.FileName, false, Encoding.Unicode );

                    Table[] tables = GetSelectedTables();

                    StatusForm statusForm = new StatusForm( writer, server, db, tables, filters, chkGenerateAll.Checked );

                    if( statusForm.ShowDialog() != DialogResult.OK )
                    {
                        MessageBox.Show( "Operation cancelled by user." );
                    }
                    else
                    {
                        writer.Close();

                        string scriptEditor = ConfigurationManager.AppSettings["scriptEditor"];
                        if( scriptEditor == null || scriptEditor.Length == 0 )
                            scriptEditor = "notepad.exe";

                        Process.Start( scriptEditor, dlgFileSave.FileName );
                    }
                }
            }
            catch( Exception ex )
            {
                string msg = "Unable to save the script to disk.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
            finally
            {
                if( writer != null )
                    writer.Close();
            }
        }

        private Table[] GetSelectedTables()
        {
            Table[] tables = new Table[lstTables.CheckedItems.Count];
            lstTables.CheckedItems.CopyTo( tables, 0 );

            return tables;
        }
        #endregion Private Methods
    }
}