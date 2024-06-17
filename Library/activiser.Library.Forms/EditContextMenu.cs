using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace activiser.Library.Forms
{
    [DesignTimeVisible(true), DesignerCategory("Component")]
    public class EditContextMenu : ContextMenu
    {
        EditContextMenu()
            : base()
        {
            MenuItems.Add(ContextMenuBase.CutMI);
            MenuItems.Add(ContextMenuBase.CopyMI);
            MenuItems.Add(ContextMenuBase.PasteMI);
            MenuItems.Add(ContextMenuBase.ClearMI);
            MenuItems.Add(ContextMenuBase.SelectAllMI);

            if (Phone.HavePhone())
            {
                MenuItems.Add(ContextMenuBase.CallMI);
            }
        }
        //private bool _showCall = Phone.HavePhone();
        //public bool ShowCall
        //{
        //    get { return _showCall; }
        //    set
        //    {
        //        _showCall = value;
        //        if (_showCall && Phone.HavePhone() && !this.MenuItems.Contains(ContextMenuBase.CallMI))
        //        {
        //            this.MenuItems.Add(ContextMenuBase.CallMI);
        //        }
        //        else
        //        {
        //            if (this.MenuItems.Contains(ContextMenuBase.CallMI)) this.MenuItems.Remove(ContextMenuBase.CallMI);
        //        }
        //    }
        //}

        protected override void OnPopup(System.EventArgs e)
        {
            base.OnPopup(e);
            //bool enableCuts = false;
            //bool enablePastes = false;
            //bool enablePhone = false;
            //bool enableSelectAll = false;
            ////Dim enableMail As Boolean 

            //if (!TestTextBox(this.SourceControl as TextBox, ref enableCuts, ref enablePastes, ref enablePhone, ref enableSelectAll))
            //    if (!TestNumberPicker(this.SourceControl as NumberPicker, ref enableCuts, ref enablePastes, ref enablePhone, ref enableSelectAll))
            //        if (!TestComboBox(this.SourceControl as ComboBox, ref enableCuts, ref enablePastes, ref enablePhone, ref enableSelectAll))
            //            if (!TestDateTimePicker(this.SourceControl as DateTimePicker, ref enableCuts, ref enablePastes, ref enablePhone, ref enableSelectAll))
            //                TestLabel(this.SourceControl as Label, ref enableCuts, ref enablePastes, ref enablePhone, ref enableSelectAll);

            //ContextMenuBase.CutMI.Enabled = enableCuts;
            //ContextMenuBase.ClearMI.Enabled = enableCuts;
            //ContextMenuBase.PasteMI.Enabled = enablePastes;
            //ContextMenuBase.SelectAllMI.Enabled = enableSelectAll;

            //if (ShowCall) ContextMenuBase.CallMI.Enabled = enablePhone;
        }

        private static bool TestTextBox(TextBox tb, ref bool enableCuts, ref bool enablePastes, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (tb == null)
                return false;
            enableCuts = tb.Enabled && !tb.ReadOnly;
            enablePastes = enableCuts;
            enableSelectAll = true;
            enablePhone = Phone.CanPhone(tb);
            return true;
        }

        private static bool TestNumberPicker(NumberPicker nb, ref bool enableCuts, ref bool enablePastes, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (nb == null)
                return false;
            enableCuts = nb.Enabled && !nb.ReadOnly;
            enablePastes = enableCuts;
            enableSelectAll = true;
            if (string.IsNullOrEmpty(nb.SelectedText))
                enablePhone = Phone.CanPhone(nb.SelectedText);
            else
                enablePhone = Phone.CanPhone(nb.ToString());

            return true;
        }

        private static bool TestComboBox(ComboBox cb, ref bool enableCuts, ref bool enablePastes, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (cb == null)
                return false;
            enableCuts = cb.Enabled && cb.DropDownStyle == ComboBoxStyle.DropDown;
            enablePastes = cb.Enabled;
            enableSelectAll = false;
            enablePhone = Phone.CanPhone(cb.Text);
            return true;
        }

        private static bool TestDateTimePicker(DateTimePicker dt, ref bool enableCuts, ref bool enablePastes, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (dt == null)
                return false;
            enableCuts = false;
            enablePastes = dt.Enabled;
            enablePhone = false;
            enableSelectAll = false;
            return true;
        }

        private static bool TestLabel(Label lb, ref bool enableCuts, ref bool enablePastes, ref bool enablePhone, ref bool enableSelectAll)
        {
            if (lb == null)
                return false;
            enableCuts = false;
            enablePastes = false;
            enableSelectAll = false;
            enablePhone = Phone.CanPhone(lb.Text);
            return true;
        }
    } 
}
