namespace Wagner.InsertStatementGenerator
{
    partial class LogonForm
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
            this.rbSqlServer = new System.Windows.Forms.RadioButton();
            this.rbWindowsAuth = new System.Windows.Forms.RadioButton();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbSqlServer
            // 
            this.rbSqlServer.Location = new System.Drawing.Point( 23, 86 );
            this.rbSqlServer.Name = "rbSqlServer";
            this.rbSqlServer.Size = new System.Drawing.Size( 196, 16 );
            this.rbSqlServer.TabIndex = 13;
            this.rbSqlServer.Text = "Use SQL Server Authentication";
            this.rbSqlServer.CheckedChanged += new System.EventHandler( this.rb_CheckedChanged );
            // 
            // rbWindowsAuth
            // 
            this.rbWindowsAuth.Checked = true;
            this.rbWindowsAuth.Location = new System.Drawing.Point( 23, 62 );
            this.rbWindowsAuth.Name = "rbWindowsAuth";
            this.rbWindowsAuth.Size = new System.Drawing.Size( 196, 16 );
            this.rbWindowsAuth.TabIndex = 12;
            this.rbWindowsAuth.TabStop = true;
            this.rbWindowsAuth.Text = "Use Windows Authentication";
            this.rbWindowsAuth.CheckedChanged += new System.EventHandler( this.rb_CheckedChanged );
            // 
            // lblServer
            // 
            this.lblServer.Location = new System.Drawing.Point( 13, 13 );
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size( 49, 23 );
            this.lblServer.TabIndex = 10;
            this.lblServer.Text = "Server:";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point( 38, 145 );
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size( 61, 15 );
            this.lblPassword.TabIndex = 16;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point( 38, 116 );
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size( 61, 15 );
            this.lblUsername.TabIndex = 14;
            this.lblUsername.Text = "Username:";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point( 105, 142 );
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size( 173, 20 );
            this.txtPassword.TabIndex = 17;
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point( 105, 113 );
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size( 173, 20 );
            this.txtUsername.TabIndex = 15;
            // 
            // cboServer
            // 
            this.cboServer.Location = new System.Drawing.Point( 63, 14 );
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size( 217, 21 );
            this.cboServer.Sorted = true;
            this.cboServer.TabIndex = 11;
            this.cboServer.SelectedIndexChanged += new System.EventHandler( this.cboServer_SelectedIndexChanged );
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point( 152, 180 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point( 66, 180 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
            // 
            // LogonForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size( 292, 219 );
            this.Controls.Add( this.rbSqlServer );
            this.Controls.Add( this.rbWindowsAuth );
            this.Controls.Add( this.lblServer );
            this.Controls.Add( this.lblPassword );
            this.Controls.Add( this.lblUsername );
            this.Controls.Add( this.txtPassword );
            this.Controls.Add( this.txtUsername );
            this.Controls.Add( this.cboServer );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.btnOK );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect To...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.LogonForm_Closing );
            this.Load += new System.EventHandler( this.LogonForm_Load );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSqlServer;
        private System.Windows.Forms.RadioButton rbWindowsAuth;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.ComboBox cboServer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}