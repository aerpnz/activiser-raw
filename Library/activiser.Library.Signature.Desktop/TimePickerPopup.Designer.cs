namespace activiser.Library
{
    partial class TimePickerPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TimeList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // TimeList
            // 
            this.TimeList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TimeList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.TimeList.FormattingEnabled = true;
            this.TimeList.Location = new System.Drawing.Point(0, 0);
            this.TimeList.Name = "TimeList";
            this.TimeList.ScrollAlwaysVisible = true;
            this.TimeList.Size = new System.Drawing.Size(112, 106);
            this.TimeList.TabIndex = 1;
            // 
            // TimePickerPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Magenta;
            this.ClientSize = new System.Drawing.Size(116, 113);
            this.ControlBox = false;
            this.Controls.Add(this.TimeList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TimePickerPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TimePickerPopup";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox TimeList;
    }
}