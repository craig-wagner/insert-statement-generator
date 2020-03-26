#region using

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;

#endregion using

namespace Wagner.InsertStatementGenerator
{
    public partial class MainForm : Form
    {
        #region Constants

        private const string _regKey = @"Software\InsertStatementGenerator";
        private const string _defaultTitle = "Insert Statement Generator";
        private const string _notConnected = "(Not Connected)";

        #endregion Constants

        #region Fields

        private Server _server = null;
        private Database _db = null;
        private Dictionary<string, DataTable> _filters = null;

        #endregion Fields

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Event Handlers

        private void DoConnect(object sender, System.EventArgs e)
        {
            Connect();
        }

        private void DoExit(object sender, System.EventArgs e)
        {
            Close();
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            Disconnect();

            try
            {
                RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                RegistryKey appkey;

                appkey = hkcu.CreateSubKey(_regKey);

                if (WindowState == FormWindowState.Normal)
                {
                    appkey.SetValue("Left", Left);
                    appkey.SetValue("Top", Top);
                    appkey.SetValue("Width", Width);
                    appkey.SetValue("Height", Height);
                }
            }
            catch (Exception ex)
            {
                string msg = "There was a problem saving your settings to the registry.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
        }

        private void DoDisconnect(object sender, System.EventArgs e)
        {
            Disconnect();
        }

        private void DoAbout(object sender, System.EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void lstDatabases_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                _db = null;

                if (lstDatabases.SelectedIndex > -1)
                {
                    _db = (Database)lstDatabases.SelectedItem;

                    GetTables(_db);
                }
            }
            catch (Exception ex)
            {
                string msg = "Unable to get list of tables from the selected database.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
        }

        private void lstTables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                btnGenerate.Enabled = true;
                btnApplyFilters.Enabled = true;
            }
            else if (lstTables.CheckedItems.Count == 1)
            {
                btnGenerate.Enabled = false;
                btnApplyFilters.Enabled = false;
            }
        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                GenerateScript();
            }
            catch (Exception ex)
            {
                string msg = "Unable to generate script for the selected tables.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                RegistryKey appkey = hkcu.OpenSubKey(_regKey);

                if (appkey != null)
                {
                    int left = (int)appkey.GetValue("Left", Left);
                    int top = (int)appkey.GetValue("Top", Top);
                    int width = (int)appkey.GetValue("Width", Width);
                    int height = (int)appkey.GetValue("Height", Height);

                    Location = new Point(left, top);
                    Size = new Size(width, height);
                }
            }
            catch (Exception ex)
            {
                string msg = "There was a problem getting your saved settings from the registry.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetTableSelectState(true);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            SetTableSelectState(false);
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
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

                if (_filters == null)
                    _filters = new Dictionary<string, DataTable>(tables.Length);

                FilterForm filterForm = new FilterForm(tables, _filters);

                filterForm.ShowDialog();
            }
            catch (Exception ex)
            {
                string msg = "Problem saving filter information.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
        }

        private void SetTableSelectState(bool state)
        {
            for (int i = 0; i < lstTables.Items.Count; i++)
                lstTables.SetItemChecked(i, state);
        }

        private void GetDatabases()
        {
            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                lstDatabases.Items.Clear();

                foreach (Database db in _server.Databases)
                {
                    if (!db.IsSystemObject)
                    {
                        lstDatabases.Items.Add(db);
                    }
                }
            }
            finally
            {
                Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void GetTables(Database db)
        {
            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                lstTables.Items.Clear();

                foreach (Table table in db.Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        lstTables.Items.Add(table);
                    }
                }

                lstTables.Enabled = true;

                btnGenerate.Enabled = false;
            }
            finally
            {
                Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void Connect()
        {
            try
            {
                LogonForm logon = new LogonForm();
                logon.ShowDialog(this);

                if (logon.DialogResult == DialogResult.OK)
                {
                    _server = logon.Server;

                    GetDatabases();

                    Text = String.Format("{0} ({1})", _defaultTitle, _server.Name);

                    lstDatabases.Enabled = true;
                    toolStripMenuItemDisconnect.Enabled = true;
                    toolStripButtonDisconnect.Enabled = true;

                    // The table filters that might have been set before no
                    // longer apply because the database was changed
                    _filters = null;
                }
            }
            catch (Exception ex)
            {
                string msg = "Unable to load the list of databases on the server.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
        }

        private void Disconnect()
        {
            if (_server != null)
                _server.ConnectionContext.Disconnect();

            Text = String.Format("{0} {1}", _defaultTitle, _notConnected);

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
                var result = dlgFileSave.ShowDialog();

                if (result == DialogResult.OK)
                {
                    writer = new StreamWriter(dlgFileSave.FileName, false, Encoding.Unicode);

                    var tables = GetSelectedTables();

                    var statusForm = new StatusForm(writer, _server, _db, tables, _filters, chkGenerateAll.Checked);

                    if (statusForm.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Operation cancelled by user.");
                    }
                    else
                    {
                        writer.Close();

                        string scriptEditor = ConfigurationManager.AppSettings["scriptEditor"];
                        if (scriptEditor == null || scriptEditor.Length == 0)
                        {
                            scriptEditor = "notepad.exe";
                        }

                        Process.Start(scriptEditor, dlgFileSave.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Unable to save the script to disk.";
                ErrorHandler.ShowLoggedErrorMessage(msg);
                ErrorHandler.LogException(msg, ex);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private Table[] GetSelectedTables()
        {
            var tables = new Table[lstTables.CheckedItems.Count];
            lstTables.CheckedItems.CopyTo(tables, 0);

            return tables;
        }

        #endregion Private Methods
    }
}