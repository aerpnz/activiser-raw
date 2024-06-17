using System.Windows.Forms;

namespace activiser.Library.Forms
{
    public partial class NumberPicker : Control
    {
        ///// <summary> 
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.NumberTextBox = new System.Windows.Forms.TextBox();
            this.UpArrow = new System.Windows.Forms.PictureBox();
            this.DownArrow = new System.Windows.Forms.PictureBox();
            this.UpTimer = new System.Windows.Forms.Timer();
            this.DownTimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // NumberTextBox
            // 
            this.NumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumberTextBox.Location = new System.Drawing.Point(0, 0);
            this.NumberTextBox.MaxLength = 10;
            this.NumberTextBox.Name = "NumberTextBox";
            this.NumberTextBox.Size = new System.Drawing.Size(100, 21);
            this.NumberTextBox.TabIndex = 0;
            this.NumberTextBox.Text = "0";
            this.NumberTextBox.TextChanged += new System.EventHandler(this.NumberTextBox_TextChanged);
            this.NumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberTextBox_KeyPress);
            // 
            // UpArrow
            // 
            this.UpArrow.Location = new System.Drawing.Point(76, 0);
            this.UpArrow.Name = "UpArrow";
            this.UpArrow.Size = new System.Drawing.Size(100, 50);
            this.UpArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.UpArrow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpArrow_MouseDown);
            this.UpArrow.Paint += new System.Windows.Forms.PaintEventHandler(this.UpArrow_Paint);
            this.UpArrow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UpArrow_MouseUp);
            // 
            // DownArrow
            // 
            this.DownArrow.Location = new System.Drawing.Point(88, 0);
            this.DownArrow.Name = "DownArrow";
            this.DownArrow.Size = new System.Drawing.Size(100, 50);
            this.DownArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DownArrow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DownArrow_MouseDown);
            this.DownArrow.Paint += new System.Windows.Forms.PaintEventHandler(this.DownArrow_Paint);
            this.DownArrow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DownArrow_MouseUp);
            // 
            // UpTimer
            // 
            this.UpTimer.Tick += new System.EventHandler(this.UpTimer_Tick);
            // 
            // DownTimer
            // 
            this.DownTimer.Tick += new System.EventHandler(this.DownTimer_Tick);
            // 
            // NumberPicker
            // 
            this.Controls.Add(this.NumberTextBox);
            this.Controls.Add(this.DownArrow);
            this.Controls.Add(this.UpArrow);
            this.ResumeLayout(false);

        }

        private TextBox NumberTextBox;
        private PictureBox UpArrow;
        private PictureBox DownArrow;
        private Timer UpTimer;
        private Timer DownTimer;
    }
}
