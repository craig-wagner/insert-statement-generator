#region using
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.SqlServer.Management.Smo;
#endregion using

namespace Wagner.InsertStatementGenerator
{
    public partial class LogonForm : Form
    {
        #region Constants
        private const int initialCapacity = 1024 * 5;
        private const string regKey = @"Software\InsertStatementGenerator";
        private const string defaultTitle = "Insert Statement Generator";
        private const string notConnected = "(Not Connected)";
        #endregion

        #region Fields
        private Server server = null;
        private string connectString = String.Empty;
        #endregion

        #region Public Properties
        public Server Server
        {
            get { return server; }
        }

        public string ConnectString
        {
            get { return connectString; }
        }
        #endregion

        #region Constructors
        public LogonForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void rb_CheckedChanged( object sender, System.EventArgs e )
        {
            SetInputControls( rbSqlServer.Checked );
        }

        private void btnOK_Click( object sender, System.EventArgs e )
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                if( cboServer.Text.Length == 0 )
                {
                    MessageBox.Show(
                        "You must specify a server.", 
                        Program.AppName, 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning );
                    cboServer.Focus();
                }

                server = new Server( cboServer.Text );

                if( rbSqlServer.Checked )
                {
                    if( txtUsername.TextLength == 0 )
                    {
                        MessageBox.Show(
                            "Username cannot be left blank.", 
                            Program.AppName, 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning );
                        txtUsername.Focus();
                    }
                    else
                    {
                        server.ConnectionContext.LoginSecure = false;
                        server.ConnectionContext.Login = txtUsername.Text;
                        server.ConnectionContext.Password = txtPassword.Text;
                    }
                }

                if( server != null )
                {
                    ServerLogon serverLogon;
                    serverLogon.ServerName = cboServer.Text;
                    serverLogon.Username = txtUsername.Text;
                    serverLogon.UseWindowsAuth = rbWindowsAuth.Checked;

                    int index = cboServer.FindString( cboServer.Text );
                    if( index > -1 )
                        cboServer.Items.RemoveAt( index );

                    cboServer.Items.Add( serverLogon );
                    cboServer.SelectedItem = serverLogon;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch( Exception ex )
            {
                string msg = String.Format( "Unable to connect to server {0}.", cboServer.Text );
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void LogonForm_Load( object sender, System.EventArgs e )
        {
            try
            {
                int lastUsedServerIndex = -1;
                string lastUsedServerName = String.Empty;

                RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                RegistryKey appkey = hkcu.OpenSubKey( regKey );

                if( appkey != null )
                {
                    string[] keys = appkey.GetValueNames();
                    lastUsedServerName = appkey.GetValue( "LastServer", String.Empty ).ToString();

                    foreach( string key in keys )
                    {
                        ServerLogon serverLogon;

                        if( key.StartsWith( "Server" ) )
                        {
                            string[] serverInfo = appkey.GetValue( key ).ToString().Split( ';' );

                            serverLogon.ServerName = serverInfo[0];
                            serverLogon.Username = serverInfo[1];
                            serverLogon.UseWindowsAuth = serverLogon.Username.Length == 0;
                            if( serverInfo.Length == 3 )
                                serverLogon.UseWindowsAuth = Convert.ToBoolean( serverInfo[2] );

                            int i = cboServer.Items.Add( serverLogon );

                            if( lastUsedServerName == serverLogon.ServerName )
                                lastUsedServerIndex = i;
                        }
                    }

                    cboServer.SelectedIndex = lastUsedServerIndex;
                }
            }
            catch( Exception ex )
            {
                string msg = "There was a problem getting your saved settings from the registry.";
                ErrorHandler.ShowLoggedErrorMessage( msg );
                ErrorHandler.LogException( msg, ex );
            }
        }

        private void LogonForm_Closing( object sender, FormClosingEventArgs e )
        {
            if( this.DialogResult != DialogResult.Cancel )
            {
                RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                RegistryKey appkey = hkcu.CreateSubKey( regKey );

                if( appkey != null )
                {
                    string[] keys = appkey.GetValueNames();

                    appkey.SetValue( "LastServer", cboServer.Text );

                    foreach( string key in keys )
                    {
                        if( key.StartsWith( "Server" ) )
                            appkey.DeleteValue( key );
                    }

                    for( int i = 0; i < cboServer.Items.Count; i++ )
                    {
                        ServerLogon serverLogon = (ServerLogon)( cboServer.Items[i] );

                        appkey.SetValue( String.Format( "Server{0:00}", i ), serverLogon.ServerName + ";" + serverLogon.Username + ";" + serverLogon.UseWindowsAuth.ToString() );
                    }
                }
            }
        }

        private void cboServer_SelectedIndexChanged( object sender, System.EventArgs e )
        {
            ServerLogon serverLogon = (ServerLogon)( cboServer.SelectedItem );

            if( serverLogon.UseWindowsAuth )
                rbWindowsAuth.Checked = true;
            else
                rbSqlServer.Checked = true;

            SetInputControls( rbSqlServer.Checked );
            txtUsername.Text = serverLogon.Username;
        }
        #endregion Event Handlers

        #region Private Methods
        private void SetInputControls( bool enabled )
        {
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
        }
        #endregion Private Methods

        #region Private Structures
        private struct ServerLogon
        {
            public string ServerName;
            public string Username;
            public bool UseWindowsAuth;

            public override string ToString()
            {
                return ServerName;
            }
        }
        #endregion Private Structures
    }
}