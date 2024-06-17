using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace activiser.Library.Forms
{
    [DesignTimeVisible(true),DesignerCategory("Component")]
    public class ReadOnlyContextMenu : ContextMenu
    {
        private System.Windows.Forms.MenuItem CopyMI;
        private System.Windows.Forms.MenuItem SelectAllMI;
        private System.Windows.Forms.MenuItem CallMI;

        internal ReadOnlyContextMenu()
            : base()
        {
            CopyMI = new System.Windows.Forms.MenuItem();
            CopyMI.Text = Properties.Resources.ContextMenuCopy;
            this.MenuItems.Add(CopyMI);

            SelectAllMI = new System.Windows.Forms.MenuItem();
            SelectAllMI.Text = Properties.Resources.ContextMenuSelectAll;
            this.MenuItems.Add(SelectAllMI);

            // always instantiate, just only add when needed.
            CallMI = new System.Windows.Forms.MenuItem();
            CallMI.Text = Properties.Resources.ContextMenuCall;
        }

        private bool _showCall = false;
        public bool ShowCall
        {
            get { return _showCall; }
            set
            {
                _showCall = value;
                if (_showCall && !this.MenuItems.Contains(CallMI))
                    this.MenuItems.Add(CallMI);
                else if (this.MenuItems.Contains(CallMI)) 
                    this.MenuItems.Remove(CallMI);
            }
        }

        protected override void OnPopup(System.EventArgs e)
        {
            base.OnPopup(e);

            
            //bool enablePhone = false;
            //bool enableSelectAll = false;
            ////Dim enableMail As Boolean 

            //if (!TestTextBox(this.SourceControl as TextBox, ref enablePhone, ref enableSelectAll))
            //    if (!TestNumberPicker(this.SourceControl as NumberPicker, ref enablePhone, ref enableSelectAll))
            //        if (!TestComboBox(this.SourceControl as ComboBox, ref enablePhone, ref enableSelectAll))
            //            if (!TestDateTimePicker(this.SourceControl as DateTimePicker, ref enablePhone, ref enableSelectAll))
            //                TestLabel(this.SourceControl as Label, ref enablePhone, ref enableSelectAll);

            //ContextMenuBase.SelectAllMI.Enabled = enableSelectAll;

            //if (ShowCall) ContextMenuBase.CallMI.Enabled = enablePhone;
        }

        private static bool TestTextBox(TextBox tb, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (tb == null) return false;
            enableSelectAll = true;
            enablePhone = Phone.CanPhone(tb);
            return true;
        }

        private static bool TestNumberPicker(NumberPicker nb, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (nb == null)
                return false;
            enableSelectAll = true;
            if (string.IsNullOrEmpty(nb.SelectedText))
                enablePhone = Phone.CanPhone(nb.SelectedText);
            else
                enablePhone = Phone.CanPhone(nb.ToString());

            return true;
        }

        private static bool TestComboBox(ComboBox cb, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (cb == null)
                return false;
            enableSelectAll = false;
            enablePhone = Phone.CanPhone(cb.Text);
            return true;
        }

        private static bool TestDateTimePicker(DateTimePicker dt, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (dt == null)
                return false;
            enablePhone = false;
            enableSelectAll = false;
            return true;
        }

        private static bool TestLabel(Label lb, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (lb == null)
                return false;
            enableSelectAll = false;
            enablePhone = Phone.CanPhone(lb.Text);
            return true;
        }
    } 
}
