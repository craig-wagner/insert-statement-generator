namespace Wagner.InsertStatementGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblTables = new System.Windows.Forms.Label();
            this.lblDatabases = new System.Windows.Forms.Label();
            this.lstDatabases = new System.Windows.Forms.ListBox();
            this.lstTables = new System.Windows.Forms.CheckedListBox();
            this.chkGenerateAll = new System.Windows.Forms.CheckBox();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.Filter = "SQL Scripts|*.sql";
            this.dlgFileSave.Title = "Save Procedure";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemHelp} );
            this.menuStrip.Location = new System.Drawing.Point( 0, 0 );
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding( 2, 2, 0, 2 );
            this.menuStrip.Size = new System.Drawing.Size( 664, 24 );
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemConnect,
            this.toolStripMenuItemDisconnect,
            this.toolStripSeparator1,
            this.toolStripMenuItemExit} );
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size( 35, 20 );
            this.toolStripMenuItemFile.Text = "&File";
            // 
            // toolStripMenuItemConnect
            // 
            this.toolStripMenuItemConnect.Image = global::Wagner.InsertStatementGenerator.Properties.Resources.connect;
            this.toolStripMenuItemConnect.Name = "toolStripMenuItemConnect";
            this.toolStripMenuItemConnect.Size = new System.Drawing.Size( 137, 22 );
            this.toolStripMenuItemConnect.Text = "&Connect";
            this.toolStripMenuItemConnect.Click += new System.EventHandler( this.DoConnect );
            // 
            // toolStripMenuItemDisconnect
            // 
            this.toolStripMenuItemDisconnect.Enabled = false;
            this.toolStripMenuItemDisconnect.Image = global::Wagner.InsertStatementGenerator.Properties.Resources.disconnect;
            this.toolStripMenuItemDisconnect.Name = "toolStripMenuItemDisconnect";
            this.toolStripMenuItemDisconnect.Size = new System.Drawing.Size( 137, 22 );
            this.toolStripMenuItemDisconnect.Text = "&Disconnect";
            this.toolStripMenuItemDisconnect.Click += new System.EventHandler( this.DoDisconnect );
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 134, 6 );
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Image = global::Wagner.InsertStatementGenerator.Properties.Resources.exit;
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size( 137, 22 );
            this.toolStripMenuItemExit.Text = "E&xit";
            this.toolStripMenuItemExit.Click += new System.EventHandler( this.DoExit );
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout} );
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size( 40, 20 );
            this.toolStripMenuItemHelp.Text = "&Help";
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size( 126, 22 );
            this.toolStripMenuItemAbout.Text = "&About...";
            this.toolStripMenuItemAbout.Click += new System.EventHandler( this.DoAbout );
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonConnect,
            this.toolStripButtonDisconnect,
            this.toolStripSeparator3,
            this.toolStripButtonExit} );
            this.toolStrip.Location = new System.Drawing.Point( 0, 24 );
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size( 664, 25 );
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonConnect
            // 
            this.toolStripButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonConnect.Image = global::Wagner.InsertStatementGenerator.Properties.Resources.connect;
            this.toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size( 23, 22 );
            this.toolStripButtonConnect.Text = "Connect";
            this.toolStripButtonConnect.Click += new System.EventHandler( this.DoConnect );
            // 
            // toolStripButtonDisconnect
            // 
            this.toolStripButtonDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDisconnect.Image = global::Wagner.InsertStatementGenerator.Properties.Resources.disconnect;
            this.toolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            this.toolStripButtonDisconnect.Size = new System.Drawing.Size( 23, 22 );
            this.toolStripButtonDisconnect.Text = "Disconnect";
            this.toolStripButtonDisconnect.Click += new System.EventHandler( this.DoDisconnect );
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExit.Image = global::Wagner.InsertStatementGenerator.Properties.Resources.exit;
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size( 23, 22 );
            this.toolStripButtonExit.Text = "Exit";
            this.toolStripButtonExit.Click += new System.EventHandler( this.DoExit );
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnSelectAll.Location = new System.Drawing.Point( 533, 294 );
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size( 118, 23 );
            this.btnSelectAll.TabIndex = 9;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.Click += new System.EventHandler( this.btnSelectAll_Click );
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnDeselectAll.Location = new System.Drawing.Point( 533, 322 );
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size( 118, 23 );
            this.btnDeselectAll.TabIndex = 10;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.Click += new System.EventHandler( this.btnDeselectAll_Click );
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Location = new System.Drawing.Point( 533, 99 );
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size( 118, 23 );
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate INSERTs";
            this.btnGenerate.Click += new System.EventHandler( this.btnGenerate_Click );
            // 
            // lblTables
            // 
            this.lblTables.Location = new System.Drawing.Point( 237, 50 );
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size( 100, 20 );
            this.lblTables.TabIndex = 4;
            this.lblTables.Text = "Tables";
            this.lblTables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDatabases
            // 
            this.lblDatabases.Location = new System.Drawing.Point( 7, 50 );
            this.lblDatabases.Name = "lblDatabases";
            this.lblDatabases.Size = new System.Drawing.Size( 100, 20 );
            this.lblDatabases.TabIndex = 2;
            this.lblDatabases.Text = "Databases";
            this.lblDatabases.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstDatabases
            // 
            this.lstDatabases.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.lstDatabases.Enabled = false;
            this.lstDatabases.Location = new System.Drawing.Point( 7, 70 );
            this.lstDatabases.Name = "lstDatabases";
            this.lstDatabases.Size = new System.Drawing.Size( 221, 277 );
            this.lstDatabases.Sorted = true;
            this.lstDatabases.TabIndex = 3;
            this.lstDatabases.SelectedIndexChanged += new System.EventHandler( this.lstDatabases_SelectedIndexChanged );
            // 
            // lstTables
            // 
            this.lstTables.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.lstTables.CheckOnClick = true;
            this.lstTables.FormattingEnabled = true;
            this.lstTables.Location = new System.Drawing.Point( 237, 70 );
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size( 290, 274 );
            this.lstTables.Sorted = true;
            this.lstTables.TabIndex = 5;
            this.lstTables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler( this.lstTables_ItemCheck );
            // 
            // chkGenerateAll
            // 
            this.chkGenerateAll.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.chkGenerateAll.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkGenerateAll.Location = new System.Drawing.Point( 533, 129 );
            this.chkGenerateAll.Name = "chkGenerateAll";
            this.chkGenerateAll.Size = new System.Drawing.Size( 118, 117 );
            this.chkGenerateAll.TabIndex = 8;
            this.chkGenerateAll.Text = "Checking this box will cause all data dependencies to be generated. This may resu" +
                "lt in more tables being scripted than were selected.";
            this.chkGenerateAll.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkGenerateAll.UseVisualStyleBackColor = true;
            // 
            // btnApplyFilters
            // 
            this.btnApplyFilters.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnApplyFilters.Enabled = false;
            this.btnApplyFilters.Location = new System.Drawing.Point( 533, 70 );
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size( 118, 23 );
            this.btnApplyFilters.TabIndex = 6;
            this.btnApplyFilters.Text = "Apply Filters";
            this.btnApplyFilters.Click += new System.EventHandler( this.btnApplyFilters_Click );
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size( 664, 359 );
            this.Controls.Add( this.btnApplyFilters );
            this.Controls.Add( this.chkGenerateAll );
            this.Controls.Add( this.btnSelectAll );
            this.Controls.Add( this.btnDeselectAll );
            this.Controls.Add( this.btnGenerate );
            this.Controls.Add( this.lblTables );
            this.Controls.Add( this.lblDatabases );
            this.Controls.Add( this.lstDatabases );
            this.Controls.Add( this.lstTables );
            this.Controls.Add( this.toolStrip );
            this.Controls.Add( this.menuStrip );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Location = new System.Drawing.Point( 50, 50 );
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size( 672, 393 );
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Insert Statement Generator (Not Connected)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.MainForm_Closing );
            this.Load += new System.EventHandler( this.MainForm_Load );
            this.menuStrip.ResumeLayout( false );
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout( false );
            this.toolStrip.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnect;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonExit;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.Label lblDatabases;
        private System.Windows.Forms.ListBox lstDatabases;
        private System.Windows.Forms.CheckedListBox lstTables;
        private System.Windows.Forms.CheckBox chkGenerateAll;
        private System.Windows.Forms.Button btnApplyFilters;

    }
}