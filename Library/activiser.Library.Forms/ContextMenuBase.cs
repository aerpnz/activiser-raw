using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using activiser.Library;

namespace activiser.Library.Forms
{
    internal sealed class ContextMenuBase
    {

        internal static System.Windows.Forms.MenuItem CutMI = new System.Windows.Forms.MenuItem();
        internal static System.Windows.Forms.MenuItem CopyMI = new System.Windows.Forms.MenuItem();
        internal static System.Windows.Forms.MenuItem PasteMI = new System.Windows.Forms.MenuItem();
        internal static System.Windows.Forms.MenuItem ClearMI = new System.Windows.Forms.MenuItem();
        internal static System.Windows.Forms.MenuItem SelectAllMI = new System.Windows.Forms.MenuItem();
        internal static System.Windows.Forms.MenuItem CallMI;

        static ContextMenuBase()
        {
            //CutMI = new System.Windows.Forms.MenuItem();
            //CopyMI = new System.Windows.Forms.MenuItem();
            //PasteMI = new System.Windows.Forms.MenuItem();
            //ClearMI = new System.Windows.Forms.MenuItem();
            //SelectAllMI = new System.Windows.Forms.MenuItem();
            //MailMI = New System.Windows.Forms.MenuItem 

            CutMI.Text = Properties.Resources.ContextMenuCut;
            CopyMI.Text = Properties.Resources.ContextMenuCopy;
            PasteMI.Text = Properties.Resources.ContextMenuPaste;
            ClearMI.Text = Properties.Resources.ContextMenuClear;
            SelectAllMI.Text = Properties.Resources.ContextMenuSelectAll;
            //MailMI.Text = "Send E-Mail" 


            CutMI.Click += ContextMenuCut_Click;
            CopyMI.Click += ContextMenuCopy_Click;
            PasteMI.Click += ContextMenuPaste_Click;
            ClearMI.Click += ContextMenuClear_Click;
            SelectAllMI.Click += ContextMenuSelectAll_Click;

            if (Phone.HavePhone())
            {
                CallMI = new System.Windows.Forms.MenuItem();
                CallMI.Text = Properties.Resources.ContextMenuCall;
                CallMI.Click += ContextMenuCall_Click;

                //TODO: Add SMS Support 
                //SmsMI = New System.Windows.Forms.MenuItem 
                //Me.SmsMI.Text = "Send Text" 
            }

        }

        internal static void ContextMenuSelectAll_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null)
                return;
            // Que ?! 

            ContextMenu cm = mi.Parent as ContextMenu;
            if (cm == null)
                return;
            // Que ?! 

            TextBox tb = cm.SourceControl as TextBox;
            if (tb != null)
            {
                tb.Focus();
                tb.SelectAll();
            }

            NumberPicker nb = cm.SourceControl as NumberPicker;
            if (nb != null)
            {
                nb.Focus();
                nb.SelectAll();
                return;
            }

        }

        internal static void ContextMenuClear_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null)
                return;
            // Que ?! 

            ContextMenu cm = mi.Parent as ContextMenu;
            if (cm == null)
                return;
            // Que ?! 

            TextBox tb = cm.SourceControl as TextBox;
            if (tb != null)
            {
                if (tb.Enabled && !tb.ReadOnly)
                    tb.SelectedText = string.Empty;
                return;
            }

            NumberPicker nb = cm.SourceControl as NumberPicker;
            if (nb != null)
            {
                if (nb.Enabled && !nb.ReadOnly)
                    nb.SelectedText = string.Empty;
                return;
            }
        }

        internal static void ContextMenuCopy_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null)
                return;
            // Que ?! 

            ContextMenu cm = mi.Parent as ContextMenu;
            if (cm == null)
                return;
            // Que ?! 

            TextBox tb = cm.SourceControl as TextBox;
            if (tb != null)
            {
                Clipboard.SetText(tb.SelectedText);
                return;
            }

            DateTimePicker dt = cm.SourceControl as DateTimePicker;
            if (dt != null)
            {
                Clipboard.SetText(dt.Text);
                return;
            }

            NumberPicker nb = cm.SourceControl as NumberPicker;
            if (nb != null)
            {
                Clipboard.SetText(nb.SelectedText);
                return;
            }

            ComboBox cb = cm.SourceControl as ComboBox;
            if (cb != null)
            {
                Clipboard.SetText(cb.Text);
                return;
            }

            ListBox lbx = cm.SourceControl as ListBox;
            if (lbx != null)
            {
                Clipboard.SetText(lbx.Text);
                return;
            }

            Label lb = cm.SourceControl as Label;
            if (lb != null)
            {
                Clipboard.SetText(lb.Text);
                return;
            }
        }

        internal static void ContextMenuCut_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null)
                return;
            // Que ?! 

            ContextMenu cm = mi.Parent as ContextMenu;
            if (cm == null)
                return;
            // Que ?! 

            TextBox tb = cm.SourceControl as TextBox;
            if (tb != null)
            {
                string clipText = tb.SelectedText;
                Clipboard.SetText(clipText);
                if (tb.Enabled && !tb.ReadOnly)
                    tb.SelectedText = string.Empty;
                return;
            }

            NumberPicker nb = cm.SourceControl as NumberPicker;
            if (nb != null)
            {
                string clipText = nb.SelectedText;
                Clipboard.SetText(clipText);
                if (nb.Enabled && !nb.ReadOnly)
                    nb.SelectedText = string.Empty;
                return;
            }
        }

        internal static void ContextMenuPaste_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null)
                return;
            // Que ?! 

            ContextMenu cm = mi.Parent as ContextMenu;
            if (cm == null)
                return;
            // Que ?! 

            TextBox tb = cm.SourceControl as TextBox;
            if (tb != null)
            {
                string clipText = Clipboard.GetText();
                if (tb.Text.Length - tb.SelectedText.Length + clipText.Length <= tb.MaxLength)
                {
                    if (tb.Enabled && !tb.ReadOnly)
                        tb.SelectedText = clipText;
                }
                return;
            }

            DateTimePicker dt = cm.SourceControl as DateTimePicker;
            if (dt != null)
            {
                string clipText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(clipText))
                {
                    DateTime newValue;
                    try
                    {
                        newValue = DateTime.Parse(clipText, CultureInfo.CurrentCulture);
                        dt.Value = newValue;
                    }
                    catch (FormatException ex)
                    {
                        Sound.Beep();
                    }
                }
                return;
            }

            NumberPicker nb = cm.SourceControl as NumberPicker;
            if (nb != null)
            {
                string clipText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(clipText))
                {
                    decimal newValue;
                    try
                    {
                        newValue = decimal.Parse(clipText, CultureInfo.CurrentCulture);
                        nb.Value = newValue;
                    }
                    catch (FormatException ex)
                    {
                        Sound.Beep();
                    }
                }
                return;
            }

            //TODO: test that this works. 
            ComboBox cb = cm.SourceControl as ComboBox;
            if (cb != null)
            {
                cb.Focus();
                cb.Text = Clipboard.GetText();
            }

            ListBox lbx = cm.SourceControl as ListBox;
            if (lbx != null)
            {
                lbx.Focus();
                lbx.Text = Clipboard.GetText();
            }

        }

        internal static void ContextMenuCall_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null)
                return;
            // Que ?! 

            ContextMenu cm = mi.Parent as ContextMenu;
            if (cm == null)
                return;
            // Que ?! 

            TextBox tb = cm.SourceControl as TextBox;
            if (tb != null)
            {
                Phone.MakePhoneCall(tb);
                return;
            }

            NumberPicker nb = cm.SourceControl as NumberPicker;
            if (nb != null)
            {
                Phone.MakePhoneCall(nb.SelectedText);
                return;
            }

            ComboBox cb = cm.SourceControl as ComboBox;
            if (cb != null)
            {
                Phone.MakePhoneCall(cb.Text);
            }

            ListBox lbx = cm.SourceControl as ListBox;
            if (lbx != null)
            {
                Phone.MakePhoneCall(lbx.Text);
            }

        }

        //Private Function ValidateEmailAddress(ByVal strEmailAddress As String) As Boolean 
        // Dim regEx As New System.Text.RegularExpressions.Regex("^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") 
        // Return regEx.IsMatch(strEmailAddress) 
        //End Function 

        //Private Function CanMail(ByVal text As String) As Boolean 
        // Return ValidateEmailAddress(text) 
        //End Function 

        //Private Sub MailMI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MailMI.Click 
        // Dim tb As TextBox = TryCast(Me.SourceControl, TextBox) 
        // If tb IsNot Nothing Then 
        // SendEmail(tb) 
        // Return 
        // End If 

        // Dim nb As NumberPicker = TryCast(Me.SourceControl, NumberPicker) 
        // If nb IsNot Nothing Then 
        // SendEmail(nb.SelectedText) 
        // Return 
        // End If 

        // Dim cb As ComboBox = TryCast(Me.SourceControl, ComboBox) 
        // If cb IsNot Nothing Then 
        // SendEmail(cb.Text) 
        // End If 
        //End Sub 
    } 
}
