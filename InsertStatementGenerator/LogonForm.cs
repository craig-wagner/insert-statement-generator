using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;

namespace Wagner.InsertStatementGenerator
{
    public partial class LogonForm : Form
    {
        private const string _regKey = @"Software\InsertStatementGenerator";

        private Server _server = null;
        private readonly string _connectString = String.Empty;

        public Server Server => _server;

        public string ConnectString => _connectString;

        public LogonForm() => InitializeComponent();

        private void rb_CheckedChanged(object sender, System.EventArgs e) => SetInputControls(rbSqlServer.Checked);

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                if (cboServer.Text.Length == 0)
                {
                    MessageBox.Show(
                        "You must specify a server.",
                        Program.AppName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    cboServer.Focus();
                }

                _server = new Server(cboServer.Text);

                if (rbSqlServer.Checked)
                {
                    if (txtUsername.TextLength == 0)
                    {
                        MessageBox.Show(
                            "Username cannot be left blank.",
                            Program.AppName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txtUsername.Focus();
                    }
                    else
                    {
                        _server.ConnectionContext.ConnectionString = "Server=sidsqlserver-uat.database.windows.net;" +
                            "Database=ipseity-identity-programs-0001;" +
                            "User ID=craig.wagner@sterlingts.com;" +
                            "Password=W!ld.fir3b;" +
                            "Encrypt=true;" +
                            "Authentication=Active Directory Password";

                        //_server.ConnectionContext.LoginSecure = false;
                        //_server.ConnectionContext.Login = txtUsername.Text;
                        //_server.ConnectionContext.Password = txtPassword.Text;
                    }
                }

                if (_server != null)
                {
                    ServerLogon serverLogon;
                    serverLogon.ServerName = cboServer.Text;
                    serverLogon.Username = txtUsername.Text;
                    serverLogon.UseWindowsAuth = rbWindowsAuth.Checked;

                    var index = cboServer.FindString(cboServer.Text);
                    if (index > -1)
                    {
                        cboServer.Items.RemoveAt(index);
                    }

                    cboServer.Items.Add(serverLogon);
                    cboServer.SelectedItem = serverLogon;

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                var msg = String.Format("Unable to connect to server {0}.", cboServer.Text);
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LogonForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                var lastUsedServerIndex = -1;
                var lastUsedServerName = String.Empty;

                var hkcu = Microsoft.Win32.Registry.CurrentUser;

                var appkey = hkcu.OpenSubKey(_regKey);

                if (appkey != null)
                {
                    var keys = appkey.GetValueNames();
                    lastUsedServerName = appkey.GetValue("LastServer", String.Empty).ToString();

                    foreach (var key in keys)
                    {
                        ServerLogon serverLogon;

                        if (key.StartsWith("Server"))
                        {
                            var serverInfo = appkey.GetValue(key).ToString().Split(';');

                            serverLogon.ServerName = serverInfo[0];
                            serverLogon.Username = serverInfo[1];
                            serverLogon.UseWindowsAuth = serverLogon.Username.Length == 0;
                            if (serverInfo.Length == 3)
                            {
                                serverLogon.UseWindowsAuth = Convert.ToBoolean(serverInfo[2]);
                            }

                            var i = cboServer.Items.Add(serverLogon);

                            if (lastUsedServerName == serverLogon.ServerName)
                            {
                                lastUsedServerIndex = i;
                            }
                        }
                    }

                    cboServer.SelectedIndex = lastUsedServerIndex;
                }
            }
            catch (Exception ex)
            {
                var msg = "There was a problem getting your saved settings from the registry.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
        }

        private void LogonForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Cancel)
            {
                var hkcu = Microsoft.Win32.Registry.CurrentUser;

                var appkey = hkcu.CreateSubKey(_regKey);

                if (appkey != null)
                {
                    var keys = appkey.GetValueNames();

                    appkey.SetValue("LastServer", cboServer.Text);

                    foreach (var key in keys)
                    {
                        if (key.StartsWith("Server"))
                        {
                            appkey.DeleteValue(key);
                        }
                    }

                    for (var i = 0; i < cboServer.Items.Count; i++)
                    {
                        var serverLogon = (ServerLogon)(cboServer.Items[i]);

                        appkey.SetValue(String.Format("Server{0:00}", i), serverLogon.ServerName + ";" + serverLogon.Username + ";" + serverLogon.UseWindowsAuth.ToString());
                    }
                }
            }
        }

        private void cboServer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var serverLogon = (ServerLogon)(cboServer.SelectedItem);

            if (serverLogon.UseWindowsAuth)
            {
                rbWindowsAuth.Checked = true;
            }
            else
            {
                rbSqlServer.Checked = true;
            }

            SetInputControls(rbSqlServer.Checked);
            txtUsername.Text = serverLogon.Username;
        }

        private void SetInputControls(bool enabled)
        {
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
        }

        private struct ServerLogon
        {
            public string ServerName;
            public string Username;
            public bool UseWindowsAuth;

            public override string ToString() => ServerName;
        }
    }
}