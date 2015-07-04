namespace Wagner.InsertStatementGenerator
{
    #region using
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using Microsoft.Win32;
    #endregion

    /// <summary>
    /// Standard About dialog for a Windows application.
    /// </summary>
    internal class AboutForm : System.Windows.Forms.Form
    {
        #region Fields
        private string sysInfoPath;
        #endregion

        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSysInfo;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDisclaimer;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #region Constructors
        public AboutForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            #region Custom Constructor Logic
            RegistryKey root = Registry.LocalMachine;

            RegistryKey file = root.OpenSubKey( @"SOFTWARE\Microsoft\Shared Tools\MSINFO" );

            // Try to get system info program path\name from registry...
            if( file != null )
                sysInfoPath = file.GetValue("Path").ToString();
            else			
            {
                // Try to get system info program path only from registry...
                RegistryKey path = root.OpenSubKey( @"SOFTWARE\Microsoft\Shared Tools Location" );

                sysInfoPath = path.GetValue( "MsInfo" ).ToString() + "\\MSINFO32.EXE";
            }

            // Does the utility exist?
            if( File.Exists( sysInfoPath ) )
                btnSysInfo.Enabled = true;
            #endregion
        }
        #endregion

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( AboutForm ) );
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSysInfo = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            ( (System.ComponentModel.ISupportInitialize)( this.picIcon ) ).BeginInit();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picIcon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.picIcon.Image = ( (System.Drawing.Image)( resources.GetObject( "picIcon.Image" ) ) );
            this.picIcon.Location = new System.Drawing.Point( 9, 19 );
            this.picIcon.Name = "picIcon";
            this.picIcon.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.picIcon.Size = new System.Drawing.Size( 64, 64 );
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 2;
            this.picIcon.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOK.Location = new System.Drawing.Point( 361, 217 );
            this.btnOK.Name = "btnOK";
            this.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOK.Size = new System.Drawing.Size( 89, 25 );
            this.btnOK.TabIndex = 0;
            this.btnOK.Tag = "OK";
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // btnSysInfo
            // 
            this.btnSysInfo.BackColor = System.Drawing.SystemColors.Control;
            this.btnSysInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSysInfo.Enabled = false;
            this.btnSysInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSysInfo.Location = new System.Drawing.Point( 361, 246 );
            this.btnSysInfo.Name = "btnSysInfo";
            this.btnSysInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSysInfo.Size = new System.Drawing.Size( 89, 25 );
            this.btnSysInfo.TabIndex = 1;
            this.btnSysInfo.Tag = "&System Info...";
            this.btnSysInfo.Text = "&System Info...";
            this.btnSysInfo.UseVisualStyleBackColor = false;
            this.btnSysInfo.Click += new System.EventHandler( this.btnSysInfo_Click );
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.SystemColors.Control;
            this.lblDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point( 82, 87 );
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDescription.Size = new System.Drawing.Size( 319, 90 );
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Tag = "App Description";
            this.lblDescription.Text = "Used to generate insert statements based on the current data in selected table(s)" +
                ".";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point( 82, 19 );
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTitle.Size = new System.Drawing.Size( 319, 37 );
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Tag = "Application Title";
            this.lblTitle.Text = "Insert Statement Generator";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.SystemColors.Control;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVersion.Location = new System.Drawing.Point( 82, 60 );
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVersion.Size = new System.Drawing.Size( 319, 18 );
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Tag = "Version";
            this.lblVersion.Text = "Version 1.0.0";
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.BackColor = System.Drawing.SystemColors.Control;
            this.lblDisclaimer.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDisclaimer.ForeColor = System.Drawing.Color.Black;
            this.lblDisclaimer.Location = new System.Drawing.Point( 20, 202 );
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDisclaimer.Size = new System.Drawing.Size( 329, 64 );
            this.lblDisclaimer.TabIndex = 3;
            this.lblDisclaimer.Tag = "Warning: ...";
            this.lblDisclaimer.Text = "Copyright © 2006-2007 Craig Wagner. All Rights Reserved.";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size( 456, 279 );
            this.Controls.Add( this.picIcon );
            this.Controls.Add( this.btnOK );
            this.Controls.Add( this.btnSysInfo );
            this.Controls.Add( this.lblDescription );
            this.Controls.Add( this.lblTitle );
            this.Controls.Add( this.lblVersion );
            this.Controls.Add( this.lblDisclaimer );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Insert Statement Generator";
            this.Load += new System.EventHandler( this.About_Load );
            ( (System.ComponentModel.ISupportInitialize)( this.picIcon ) ).EndInit();
            this.ResumeLayout( false );

        }
		#endregion

        #region Event Handlers
        private void About_Load(object sender, System.EventArgs e)
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            lblVersion.Text = String.Format( "Version {0}", assemblyName.Version );
        }

        private void btnSysInfo_Click(object sender, System.EventArgs e)
        {
            Process.Start( sysInfoPath );
        }
        #endregion
    }
}
