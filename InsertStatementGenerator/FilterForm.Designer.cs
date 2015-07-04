namespace Wagner.InsertStatementGenerator
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( FilterForm ) );
            this.btnOK = new System.Windows.Forms.Button();
            this.lstTables = new System.Windows.Forms.ListBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgColumnValues = new System.Windows.Forms.DataGridView();
            this.columnColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.dgColumnValues ) ).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point( 258, 368 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lstTables
            // 
            this.lstTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTables.FormattingEnabled = true;
            this.lstTables.Location = new System.Drawing.Point( 0, 0 );
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size( 188, 342 );
            this.lstTables.TabIndex = 3;
            this.lstTables.SelectedIndexChanged += new System.EventHandler( this.lstTables_SelectedIndexChanged );
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.splitContainer.Location = new System.Drawing.Point( 12, 12 );
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add( this.lstTables );
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add( this.dgColumnValues );
            this.splitContainer.Size = new System.Drawing.Size( 567, 350 );
            this.splitContainer.SplitterDistance = 188;
            this.splitContainer.TabIndex = 4;
            // 
            // dgColumnValues
            // 
            this.dgColumnValues.AllowUserToAddRows = false;
            this.dgColumnValues.AllowUserToDeleteRows = false;
            this.dgColumnValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgColumnValues.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.columnColumnName,
            this.columnColumnValue} );
            this.dgColumnValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgColumnValues.Location = new System.Drawing.Point( 0, 0 );
            this.dgColumnValues.Name = "dgColumnValues";
            this.dgColumnValues.RowHeadersVisible = false;
            this.dgColumnValues.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgColumnValues.Size = new System.Drawing.Size( 375, 350 );
            this.dgColumnValues.TabIndex = 0;
            // 
            // columnColumnName
            // 
            this.columnColumnName.DataPropertyName = "ColumnName";
            this.columnColumnName.HeaderText = "Column Name";
            this.columnColumnName.Name = "columnColumnName";
            this.columnColumnName.ReadOnly = true;
            // 
            // columnColumnValue
            // 
            this.columnColumnValue.DataPropertyName = "ColumnValue";
            this.columnColumnValue.HeaderText = "Column Value";
            this.columnColumnValue.Name = "columnColumnValue";
            // 
            // FilterForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 591, 403 );
            this.Controls.Add( this.splitContainer );
            this.Controls.Add( this.btnOK );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.MinimizeBox = false;
            this.Name = "FilterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Specify Data Filters";
            this.Load += new System.EventHandler( this.FilterForm_Load );
            this.splitContainer.Panel1.ResumeLayout( false );
            this.splitContainer.Panel2.ResumeLayout( false );
            this.splitContainer.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.dgColumnValues ) ).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstTables;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgColumnValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnColumnValue;
    }
}