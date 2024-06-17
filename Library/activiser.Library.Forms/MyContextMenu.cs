using System.ComponentModel;
using System.Windows.Forms;

namespace activiser.Library.Forms
{
    [DesignTimeVisible(true), DesignerCategory("Component")]
    public class MyContextMenu : System.Windows.Forms.ContextMenu
    {
        private System.Windows.Forms.MenuItem CopyMI;
        private System.Windows.Forms.MenuItem SelectAllMI;
        private System.Windows.Forms.MenuItem CallMI;

        private bool _showCall = false;
        public bool ShowCall
        {
            get { return _showCall; }
            set
            {
                _showCall = value;
            }
        }

        public MyContextMenu()
            : base()
        {
            CopyMI = new MenuItem();
            CopyMI.Text = Properties.Resources.ContextMenuCall;
            this.MenuItems.Add(CopyMI);


            SelectAllMI = new MenuItem();
            SelectAllMI.Text = Properties.Resources.ContextMenuSelectAll;
            this.MenuItems.Add(SelectAllMI);

            CallMI = new System.Windows.Forms.MenuItem();
            CallMI.Text = Properties.Resources.ContextMenuCall;
        }

        bool enablePhone = false;
        bool enableSelectAll = false;
        protected override void OnPopup(System.EventArgs e)
        {
            base.OnPopup(e);

            enablePhone = false;
            enableSelectAll = false; 
            
            //if (!TestTextBox(this.SourceControl as TextBox))
            //    if (!TestNumberPicker(this.SourceControl as NumberPicker))
            //        if (!TestComboBox(this.SourceControl as ComboBox))
            //            if (!TestDateTimePicker(this.SourceControl as DateTimePicker))
            //                TestLabel(this.SourceControl as Label);

            SelectAllMI.Enabled = enableSelectAll;

            //if (ShowCall && enablePhone && !this.MenuItems.Contains(CallMI))
            //{
            //    this.MenuItems.Add(CallMI);
            //}
            //else 
            //{
            //    if (this.MenuItems.Contains(CallMI))
            //        this.MenuItems.Remove(CallMI);
            //}
        }

        //private bool TestTextBox(TextBox tb)
        //{
        //    if (tb == null) return false;
        //    enableSelectAll = true;
        //    enablePhone = Phone.CanPhone(tb);
        //    return true;
        //}

        //private bool TestNumberPicker(NumberPicker nb)
        //{
        //    if (nb == null) return false;
        //    enableSelectAll = true;
        //    if (string.IsNullOrEmpty(nb.SelectedText))
        //        enablePhone = Phone.CanPhone(nb.SelectedText);
        //    else
        //        enablePhone = Phone.CanPhone(nb.ToString());

        //    return true;
        //}

        //private bool TestComboBox(ComboBox cb)
        //{
        //    if (cb == null) return false;
        //    enableSelectAll = false;
        //    enablePhone = Phone.CanPhone(cb.Text);
        //    return true;
        //}

        //private bool TestDateTimePicker(DateTimePicker dt)
        //{
        //    if (dt == null) return false;
        //    enablePhone = false;
        //    enableSelectAll = false;
        //    return true;
        //}

        //private bool TestLabel(Label lb)
        //{
        //    if (lb == null) return false;
        //    enableSelectAll = false;
        //    enablePhone = Phone.CanPhone(lb.Text);
        //    return true;
        //}
    }
}