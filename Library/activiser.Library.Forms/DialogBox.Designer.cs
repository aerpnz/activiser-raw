namespace activiser.Library.Forms
{
    partial class DialogBox
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
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.ImageList32 = new System.Windows.Forms.ImageList();
            this.InputPanelPanel = new System.Windows.Forms.Panel();
            this.CancelAbortButton = new System.Windows.Forms.MenuItem();
            this.OkYesButton = new System.Windows.Forms.MenuItem();
            this.MainMenu = new System.Windows.Forms.MenuItem();
            this.NoIgnoreButton = new System.Windows.Forms.MenuItem();
            this.ImageList64 = new System.Windows.Forms.ImageList();
            this.MenuStrip = new System.Windows.Forms.MainMenu();
            this.Caption = new System.Windows.Forms.Label();
            this.TextInput = new System.Windows.Forms.TextBox();
            this.myContextMenu1 = new activiser.Library.Forms.MyContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // IconBox
            // 
            this.IconBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconBox.Location = new System.Drawing.Point(0, 0);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(240, 48);
            this.IconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // ImageList32
            // 
            this.ImageList32.ImageSize = new System.Drawing.Size(32, 32);
            // 
            // InputPanelPanel
            // 
            this.InputPanelPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InputPanelPanel.Location = new System.Drawing.Point(0, 188);
            this.InputPanelPanel.Name = "InputPanelPanel";
            this.InputPanelPanel.Size = new System.Drawing.Size(240, 80);
            // 
            // CancelAbortButton
            // 
            this.CancelAbortButton.Text = "Cancel";
            // 
            // OkYesButton
            // 
            this.OkYesButton.Text = "OK";
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.Add(this.NoIgnoreButton);
            this.MainMenu.MenuItems.Add(this.CancelAbortButton);
            this.MainMenu.Text = "Menu";
            // 
            // NoIgnoreButton
            // 
            this.NoIgnoreButton.Text = "No";
            // 
            // ImageList64
            // 
            this.ImageList64.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // MenuStrip
            // 
            this.MenuStrip.MenuItems.Add(this.OkYesButton);
            this.MenuStrip.MenuItems.Add(this.MainMenu);
            // 
            // Caption
            // 
            this.Caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.Caption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Caption.Location = new System.Drawing.Point(0, 48);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(240, 80);
            this.Caption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TextInput
            // 
            this.TextInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextInput.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.TextInput.Location = new System.Drawing.Point(0, 128);
            this.TextInput.Name = "TextInput";
            this.TextInput.Size = new System.Drawing.Size(240, 19);
            this.TextInput.TabIndex = 11;
            // 
            // myContextMenu1
            // 
            this.myContextMenu1.MenuItems.Add(this.menuItem1);
            this.myContextMenu1.MenuItems.Add(this.menuItem2);
            this.myContextMenu1.MenuItems.Add(this.menuItem3);
            this.myContextMenu1.ShowCall = false;
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "ssss";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "ttt";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "dddd";
            // 
            // DialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.TextInput);
            this.Controls.Add(this.Caption);
            this.Controls.Add(this.IconBox);
            this.Controls.Add(this.InputPanelPanel);
            this.Menu = this.MenuStrip;
            this.Name = "DialogBox";
            this.Text = "DialogBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.ImageList ImageList32;
        private System.Windows.Forms.Panel InputPanelPanel;
        private System.Windows.Forms.MenuItem CancelAbortButton;
        private System.Windows.Forms.MenuItem OkYesButton;
        private System.Windows.Forms.MenuItem MainMenu;
        private System.Windows.Forms.MenuItem NoIgnoreButton;
        private System.Windows.Forms.ImageList ImageList64;
        private System.Windows.Forms.MainMenu MenuStrip;
        private System.Windows.Forms.Label Caption;
        private System.Windows.Forms.TextBox TextInput;
        private MyContextMenu myContextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
    }
}