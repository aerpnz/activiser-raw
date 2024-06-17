using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace activiser.Library.Forms
{
    [DesignerCategory("Form")]
    public partial class InputPanelContainer : Control
    {
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();

        public InputPanelContainer()
        {
            this.SuspendLayout();
            this.Name = "InputPanelContainer";
            this.inputPanel1.EnabledChanged += new System.EventHandler(this.inputPanel1_EnabledChanged);
            this.Dock = DockStyle.Bottom;
            this.Height = this.inputPanel1.Bounds.Height;
            this.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Parent == null) return;
            if (this.inputPanel1.Enabled)
            {
                base.Height = this.inputPanel1.Bounds.Height;
            }
            else
            {
                base.Height = 0;
            }
            base.OnPaint(e);
        }
        private bool _inPanelSwitch;
        private void inputPanel1_EnabledChanged(object sender, EventArgs e)
        {
            if (_inPanelSwitch) return;
            if ((this.Parent != null) && (!this.Parent.Visible)) return; // ignore when parent not visible.
            _inPanelSwitch = true;
            try
            {
                this.Enabled = inputPanel1.Enabled;
                this.Visible = inputPanel1.Enabled;
                this.Height = inputPanel1.Bounds.Height;
                if (this.Parent != null)
                {
                    this.Parent.Refresh();
                }
            }
            finally
            {
                _inPanelSwitch = false;
            }
        }
    }
}
