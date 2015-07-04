namespace Wagner.InsertStatementGenerator
{
    partial class StatusForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.txtRowCount = new System.Windows.Forms.TextBox();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 143, 72 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point( 22, 13 );
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size( 68, 13 );
            this.lblTable.TabIndex = 1;
            this.lblTable.Text = "Table Name:";
            this.lblTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Location = new System.Drawing.Point( 58, 39 );
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size( 32, 13 );
            this.lblRowCount.TabIndex = 2;
            this.lblRowCount.Text = "Row:";
            this.lblRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRowCount
            // 
            this.txtRowCount.Location = new System.Drawing.Point( 96, 36 );
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.ReadOnly = true;
            this.txtRowCount.Size = new System.Drawing.Size( 84, 20 );
            this.txtRowCount.TabIndex = 3;
            // 
            // txtTable
            // 
            this.txtTable.Location = new System.Drawing.Point( 96, 10 );
            this.txtTable.Name = "txtTable";
            this.txtTable.ReadOnly = true;
            this.txtTable.Size = new System.Drawing.Size( 243, 20 );
            this.txtTable.TabIndex = 4;
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 361, 109 );
            this.ControlBox = false;
            this.Controls.Add( this.txtTable );
            this.Controls.Add( this.txtRowCount );
            this.Controls.Add( this.lblRowCount );
            this.Controls.Add( this.lblTable );
            this.Controls.Add( this.btnCancel );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatusForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.StatusForm_FormClosing );
            this.Load += new System.EventHandler( this.StatusForm_Load );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.TextBox txtRowCount;
        private System.Windows.Forms.TextBox txtTable;
    }
}