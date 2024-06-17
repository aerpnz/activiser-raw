using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace activiser.Library.Forms
{
    public class DataListView : System.Windows.Forms.ListView
    {
        DataTable _rowSource;
        ColumnHeader _sortColumn;
        List<string> _visibleColumns = new List<string>();

        public DataTable RowSource
        {
            get
            {
                return _rowSource;
            }
            set
            {
                _rowSource = value;
            }
        }

        public ColumnHeader SortColumn
        {
            get
            {
                return _sortColumn;
            }
            set
            {
                _sortColumn = value;
            }
        }

        public List<string> VisibleColumns
        {
            get
            {
                return _visibleColumns;
            }
        }
    }
}
