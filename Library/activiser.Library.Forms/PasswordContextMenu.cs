using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace activiser.Library.Forms
{
    [DesignTimeVisible(true), DesignerCategory("Component")]
    public class PasswordContextMenu : ContextMenu
    {

        public PasswordContextMenu()
            : base()
        {
            this.MenuItems.Add(ContextMenuBase.PasteMI);
            this.MenuItems.Add(ContextMenuBase.ClearMI);
            this.MenuItems.Add(ContextMenuBase.SelectAllMI);
        }

        public new MenuItemCollection MenuItems
        {
            get { return base.MenuItems; }
        }

        protected override void OnPopup(System.EventArgs e)
        {
            base.OnPopup(e);
            TextBox tb = SourceControl as TextBox;
            if (tb != null)
            {
                ContextMenuBase.PasteMI.Enabled = tb.Enabled && !tb.ReadOnly;
                ContextMenuBase.ClearMI.Enabled = ContextMenuBase.PasteMI.Enabled;
                ContextMenuBase.SelectAllMI.Enabled = ContextMenuBase.PasteMI.Enabled;
            }
            else
            {
                ContextMenuBase.PasteMI.Enabled = false;
                ContextMenuBase.ClearMI.Enabled = false;
                ContextMenuBase.SelectAllMI.Enabled = false;
            }
        }
    }
}
