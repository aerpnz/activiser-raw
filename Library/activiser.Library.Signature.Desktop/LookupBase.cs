using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace activiser.Library
{
    public abstract partial class LookupBase : Control
    {
        public event EventHandler DataSourceChanged;

        public event EventHandler DisplayMemberChanged;

        public event EventHandler SelectedValueChanged;

        public event EventHandler ValueMemberChanged;

        public LookupBase()
        {
            InitializeComponent();
        }

        private Object _dataSource;
        /// <summary>
        /// Gets or sets the data source for this ListControl.
        /// </summary>
        /// 
        public Object DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        private string _displayMember;
        /// <summary>
        /// Gets or sets the property to display for this LookupControl
        /// </summary>
        public string DisplayMember
        {
            get
            {
                return _displayMember;
            }
            set
            {
                _displayMember = value;
            }
        }

        private string _valueMember;
        public string ValueMember
        {
            get
            {
                return _valueMember;
            }
            set
            {
                _valueMember = value;
            }
        }

        private object _selectedValue;
        public object SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                _selectedValue = value;
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
            }
        }

        /// <summary>
        /// Gets the CurrencyManager associated with this control.
        /// </summary>
        /// <remarks>The DataManager property is valid if the DataSource property is set. If this is not a data-bound control, the default is a null reference (Nothing in Visual Basic).</remarks>
        /// <value>The CurrencyManager associated with this control. The default is a null reference (Nothing in Visual Basic).</value>
        public CurrencyManager DataManager
        {
            get
            {
                return _dataSource != null ? this.BindingContext[_dataSource, _valueMember] as CurrencyManager : null;  
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public string GetItemText(object item)
        {
            throw new System.NotImplementedException();
        }


    }
}
