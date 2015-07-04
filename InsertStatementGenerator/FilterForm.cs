#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
#endregion using

namespace Wagner.InsertStatementGenerator
{
    public partial class FilterForm : Form
    {
        #region Fields
        Table[] tables;
        Dictionary<string, DataTable> filters;
        #endregion Fields

        #region Constructors & Destructors
        public FilterForm( Table[] tables, Dictionary<string, DataTable> filters )
        {
            InitializeComponent();

            this.tables = tables;
            this.filters = filters;

            dgColumnValues.AutoGenerateColumns = false;
        }
        #endregion Constructors & Destructors

        #region Event Handlers
        private void FilterForm_Load( object sender, EventArgs e )
        {
            lstTables.Items.AddRange( tables );
            lstTables.SetSelected( 0, true );
        }

        private void lstTables_SelectedIndexChanged( object sender, EventArgs e )
        {
            Table table = (Table)lstTables.SelectedItem;

            if( !filters.ContainsKey( table.Name ) )
            {
                filters[table.Name] = new DataTable();
                filters[table.Name].Columns.Add( "ColumnName", typeof( String ) );
                filters[table.Name].Columns.Add( "ColumnType", typeof( String ) );
                filters[table.Name].Columns.Add( "ColumnValue", typeof( String ) );

                foreach( Column column in table.Columns )
                {
                    filters[table.Name].Rows.Add( column.Name, column.DataType, DBNull.Value );
                }
            }

            dgColumnValues.DataSource = filters[table.Name];
            dgColumnValues.AutoResizeColumns();

            // Set the width of the second column to fill the view. We need to
            // adjust the width by a fudge-factor to prevent the control from
            // scrolling horizontally.
            dgColumnValues.Columns[1].Width = dgColumnValues.DisplayRectangle.Width - dgColumnValues.Columns[0].Width - 1;
        }
        #endregion Event Handlers
    }
}