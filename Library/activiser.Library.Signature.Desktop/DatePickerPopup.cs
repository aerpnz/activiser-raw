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
    public partial class DatePickerPopup : Form
    {
        public DatePickerPopup()
        {
            InitializeComponent();
        }

        private Size borderSize = new Size(2, 2);
        private DateTime _value;
        private Control _anchorControl;

        internal DateTime Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                this.Calendar.SelectionStart = value;
            }
        }

        internal Control AnchorControl
        {
            get
            {
                return _anchorControl;
            }
            set
            {
                _anchorControl = value;
                if (value != null) Utilities.LocateDropDown(this, this._anchorControl, true);
            }
        }

        internal event EventHandler SelectedValueChanged;

        internal void Popup()
        {
            if (this._anchorControl != null) Utilities.LocateDropDown(this, this._anchorControl, true);
            
            this.Show();
            this.TopMost = true;
        }

        private void Calendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            this._value = Calendar.SelectionStart;
            SelectedValueChanged(this, new System.EventArgs());
        }

        private void CalendarPopup_Deactivate(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        //  for changes to calendar size we don't control, like when week numbers are added
        private void Calendar_SizeChanged(object sender, System.EventArgs e)
        {
            SizeMe();
        }

        private void SizeMe()
        {
            this.Border.Size = Size.Add(this.Calendar.Size, borderSize);
            this.Size = Size.Add(this.Calendar.Size, borderSize);
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            SizeMe();
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnFontChanged(e);
            SizeMe();
        }

        protected override void OnShown(System.EventArgs e)
        {
            base.OnShown(e);
            if (this._anchorControl != null) Utilities.LocateDropDown(this, this._anchorControl, true);
        }

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            base.OnForeColorChanged(e);
            this.Calendar.ForeColor = base.ForeColor;
        }
    }
}