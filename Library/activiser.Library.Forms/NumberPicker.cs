using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace activiser.Library.Forms
{
    [DesignerCategory("Form")]
    public partial class NumberPicker : Control
    {
        private bool _initialized;
        //private System.ComponentModel.IContainer components;

        private const int DesignDpi = 96;

        private float myScale = 1.0f;
        private int _Height = 22; // default height.
        private int myPadding50, myPadding100, myPadding150, myPadding200, myPadding300;

        private bool _enteringFirstDigit;
        private bool _inTextChanged;
        private decimal _value;

        private string _decimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        private string _groupSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator;
        private string _negative = System.Globalization.NumberFormatInfo.CurrentInfo.NegativeSign;

        private Bitmap _bmUpOut, _bmUpIn, _bmDownOut, _bmDownIn, _bmUpDisabled, _bmDownDisabled;

        public event EventHandler ValueChanged;

        #region "Properties"

        private int _decimalPlaces = 2;
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }
            set
            {
                if (_decimalPlaces == value) return;
                _decimalPlaces = value;
                ShowValue();
            }
        }

        private decimal _increment = 1;
        public decimal Increment
        {
            get { return _increment; }
            set
            {
                if (_increment == value) return;
                _increment = value;
            }
        }

        public int MaxLength
        {
            get { return NumberTextBox.MaxLength; }
            set
            {
                if (NumberTextBox.MaxLength == value) return;
                if (this.NumberTextBox.TextLength > value)
                {
                    string t = this.NumberTextBox.Text.Substring(0, value);
                    decimal d = 0;
                    if (TryParse(t, ref d))
                        this.Value = d;
                    else
                        this.Value = 0;
                }
                NumberTextBox.MaxLength = value;
            }
        }

        private decimal _maximum;
        public decimal Maximum
        {
            get { return _maximum; }
            set
            {
                if (_maximum == value) return;
                _maximum = value;
                if (this.Value > _maximum)
                    this.Value = _maximum;
            }
        }

        private decimal _minimum = 0;
        public decimal Minimum
        {
            get { return _minimum; }
            set
            {
                if (_minimum == value) return;
                _minimum = value;
                if (this.Value < _minimum)
                    this.Value = _minimum;
            }
        }

        public decimal Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;

                if (!this._inTextChanged) ShowValue();

                if (ValueChanged != null) ValueChanged(this, new EventArgs());
            }
        }

        private bool _upDownVisible = true;
        public bool UpDownVisible
        {
            get { return _upDownVisible; }
            set
            {
                if (_upDownVisible == value) return;
                _upDownVisible = value;
                this.UpArrow.Visible = value;
                this.DownArrow.Visible = value;
            }
        }

        public new string Text
        {
            get { return this.NumberTextBox.Text; }
        }

        public override System.Drawing.Font Font
        {
            get { return base.Font; }
            set
            {
                if (base.Font == value) return;
                base.Font = value;
                NumberTextBox.Font = value;
                _Height = this.NumberTextBox.Top + this.NumberTextBox.Height + myPadding300;
                resizeMe();
            }
        }

        public new Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                this.NumberTextBox.BackColor = value;
                this.UpArrow.BackColor = value;
                this.DownArrow.BackColor = value;
            }
        }

        public override System.Drawing.Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                this.NumberTextBox.ForeColor = value;
            }
        }

        public HorizontalAlignment TextAlign
        {
            get { return NumberTextBox.TextAlign; }
            set { NumberTextBox.TextAlign = value; }
        }

        public bool ReadOnly
        {
            get { return NumberTextBox.ReadOnly; }
            set
            {
                NumberTextBox.ReadOnly = value;
                this.UpArrow.Enabled = !value;
                this.DownArrow.Enabled = !value;
            }
        }

        #endregion

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (this.Enabled)
            {
                this.DownArrow.Image = this._bmDownOut;
                this.UpArrow.Image = this._bmUpOut;
            }
            else
            {
                this.DownArrow.Image = this._bmDownDisabled;
                this.UpArrow.Image = this._bmUpDisabled;
            }
            this.Invalidate();
        }

        private void NumberTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!_initialized) return;
            if (NumberTextBox.ReadOnly) return;

            if (e.KeyChar == _decimalSeparator[0])
            {
                if ((_decimalPlaces != 0) && (NumberTextBox.Text.IndexOf(_decimalSeparator, StringComparison.Ordinal) == -1))
                {
                    NumberTextBox.SelectedText = _decimalSeparator; // there's no decimal yet; add one, otherwise we'll just swallow the keypress.
                }
                else if (NumberTextBox.Text[NumberTextBox.SelectionStart] == _decimalSeparator[0])
                {
                    NumberTextBox.SelectionStart++;
                }
                e.Handled = true;
                return;
            }

            int selStart = NumberTextBox.SelectionStart; // save selection point

            if (e.KeyChar == _negative[0]) // allow sign change at start or end of number.
            {
                if (NumberTextBox.SelectionStart == 0 || NumberTextBox.SelectionStart == NumberTextBox.TextLength)
                {
                    this.Value = -this.Value;
                    if (selStart == 0)
                        this.NumberTextBox.SelectionStart = 0;
                    else
                        this.NumberTextBox.SelectionStart = NumberTextBox.TextLength;
                }
                e.Handled = true;
                return;
            }

            if (e.KeyChar == ' ') // swallow spaces.
            {
                e.Handled = true;
                return;
            }

            // allow default handling of backspace key, which is a valid KeyPress key, unlike Delete, cursor and function keys 
            if (e.KeyChar == '\b')
                return;

            int locationOfDecimal = NumberTextBox.Text.IndexOf(_decimalSeparator, StringComparison.Ordinal);
            if (this.DecimalPlaces != 0 && locationOfDecimal != -1 && selStart > locationOfDecimal + this.DecimalPlaces)
            {
                e.Handled = true; // decimal places already full
                return;
            }

            string stringToTest = this.NumberTextBox.Text.Remove(selStart, NumberTextBox.SelectionLength); // remove selected text 

            if (string.IsNullOrEmpty(stringToTest)) // contents erased 
            {
                stringToTest = e.KeyChar.ToString(); // change text to pressed key's value 
                _enteringFirstDigit = true;
            }
            else
            {
                stringToTest = stringToTest.Insert(selStart, e.KeyChar.ToString()); // insert pressed key 
                _enteringFirstDigit = false;
            }

            decimal result = 0M;
            if (!TryParse(stringToTest, ref result)) // test if new text is a valid number 
            {
                e.Handled = true; // if not, tell Windows to ignore the keypress. 
            }
        }

        private void NumberTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (this._inTextChanged || this._inShowValue)
            {
                return;
            }

            this._inTextChanged = true;
            decimal result = 0M;
            string value;
            base.Text = NumberTextBox.Text;
            value = NumberTextBox.Text.Replace(_groupSeparator, null);
            if (!TryParse(value, ref result))
            {
                result = 0;
            }
            this.Value = result;
            this._inTextChanged = false;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            ShowValue();
        }

        private string FormatValue(decimal value)
        {
            return string.Format(CultureInfo.CurrentUICulture, "{0:N" + _decimalPlaces + "}", value);
        }

        bool _inShowValue;
        private void ShowValue()
        {
            if (!_initialized)
                return;

            if (!_inTextChanged && !_inShowValue)
            {
                _inShowValue = true;
                NumberTextBox.Text = FormatValue(_value);
                if (_enteringFirstDigit)
                {
                    _enteringFirstDigit = false;
                    NumberTextBox.SelectionStart = 1;
                }
                _inShowValue = false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private bool TryParse(string StringValue, ref decimal result)
        {
            StringValue = StringValue.Replace(_groupSeparator, null);
            NumberStyles FormatOptions;
            if (this.DecimalPlaces == 0)
            {
                FormatOptions = NumberStyles.Integer;
                try
                {
                    int testResult = int.Parse(StringValue, FormatOptions, NumberFormatInfo.CurrentInfo);
                    if (testResult > this.Maximum || testResult < this.Minimum)
                    {
                        return false;
                    }
                    else
                    {
                        result = (decimal)testResult;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                FormatOptions = NumberStyles.Currency;
                try
                {
                    decimal testResult = decimal.Parse(StringValue, FormatOptions, NumberFormatInfo.CurrentInfo);
                    if (testResult > this.Maximum || testResult < this.Minimum)
                    {
                        return false;
                    }
                    else
                    {
                        result = testResult;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }


        public NumberPicker()
        {
            InitializeComponent();

            int dpi;
            using (Graphics g = this.CreateGraphics())
            {
                dpi = (int)g.DpiX;
                myScale = g.DpiX / DesignDpi;
                myPadding100 = (int)myScale;
                myPadding50 = myPadding100 >> 1;
                myPadding150 = myPadding100 + myPadding50;
                myPadding200 = myPadding100 + myPadding100;
                myPadding300 = myPadding200 + myPadding100;
            }
            this._Height *= myPadding100;
            this.NumberTextBox.Location = new Point(myPadding300, myPadding300);

            if (dpi == DesignDpi)
            {
                // standard resolution, use small arrows. 
                if (SystemColors.Window == Color.White && SystemColors.WindowText == Color.Black) // default colours
                {
                    this._bmUpOut = Properties.Resources.UpArrowSmall;
                    this._bmDownOut = Properties.Resources.DownArrowSmall;
                    this._bmUpIn = Properties.Resources.UpArrowSmall;
                    this._bmDownIn = Properties.Resources.DownArrowSmall;
                    this._bmUpDisabled = Properties.Resources.UpArrowSmall;
                    this._bmDownDisabled = Properties.Resources.DownArrowSmall;
                }
                else
                {
                    this._bmUpOut = GraphicsUtilities.CopyBitmap(Properties.Resources.UpArrowSmall, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.WindowText });
                    this._bmDownOut = GraphicsUtilities.CopyBitmap(Properties.Resources.DownArrowSmall, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.WindowText });
                    this._bmUpIn = GraphicsUtilities.CopyBitmap(Properties.Resources.UpArrowSmall, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.WindowText, SystemColors.Window });
                    this._bmDownIn = GraphicsUtilities.CopyBitmap(Properties.Resources.DownArrowSmall, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.WindowText, SystemColors.Window });
                    this._bmUpDisabled = GraphicsUtilities.CopyBitmap(Properties.Resources.UpArrowSmall, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.GrayText });
                    this._bmDownDisabled = GraphicsUtilities.CopyBitmap(Properties.Resources.DownArrowSmall, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.GrayText });
                }
            }
            else
            {
                if (SystemColors.Window == Color.White && SystemColors.WindowText == Color.Black) // default colours
                {
                    this._bmUpOut = Properties.Resources.UpArrowBig;
                    this._bmDownOut = Properties.Resources.DownArrowBig;
                    this._bmUpIn = Properties.Resources.UpArrowBig;
                    this._bmDownIn = Properties.Resources.DownArrowBig;
                    this._bmUpDisabled = Properties.Resources.UpArrowBig;
                    this._bmDownDisabled = Properties.Resources.DownArrowBig;
                }
                else
                {
                    this._bmUpOut = GraphicsUtilities.CopyBitmap(Properties.Resources.UpArrowBig, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.WindowText });
                    this._bmDownOut = GraphicsUtilities.CopyBitmap(Properties.Resources.DownArrowBig, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.WindowText });
                    this._bmUpIn = GraphicsUtilities.CopyBitmap(Properties.Resources.UpArrowBig, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.WindowText, SystemColors.Window });
                    this._bmDownIn = GraphicsUtilities.CopyBitmap(Properties.Resources.DownArrowBig, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.WindowText, SystemColors.Window });
                    this._bmUpDisabled = GraphicsUtilities.CopyBitmap(Properties.Resources.UpArrowBig, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.GrayText });
                    this._bmDownDisabled = GraphicsUtilities.CopyBitmap(Properties.Resources.DownArrowBig, new Color[] { Color.White, Color.Black }, new Color[] { SystemColors.Window, SystemColors.GrayText });
                }
            }
            this.UpArrow.Size = this._bmUpOut.Size;
            this.DownArrow.Size = this._bmDownOut.Size;
            this.UpArrow.Image = this._bmUpOut;
            this.DownArrow.Image = this._bmDownOut;

            Maximum = 1000; // default maximum

            _initialized = true;
            resizeMe();
            ShowValue();
        }

        private bool _inResize;
        // note: this only works on devices - the normal Windows Forms won't show the correct behaviour in design time!
        private void resizeMe()
        {
            if (!_inResize)
            {
                _inResize = true;
                this.SuspendLayout();
                int w = this.Width;

                Size s = new Size(w, _Height);
                if (!this.Size.Equals(s)) this.Size = s;

                this.DownArrow.Left = w - this.DownArrow.Width;// -myPadding50;
                this.UpArrow.Left = this.DownArrow.Left - this.UpArrow.Width + myPadding100;
                this.NumberTextBox.Width = this.UpArrow.Left - this.NumberTextBox.Left;

                this.DownArrow.Height = _Height;
                this.UpArrow.Height = _Height;
                this.ResumeLayout(true);
                _inResize = false;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            resizeMe();
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);
        }

        protected override bool ScaleChildren
        {
            get
            {
                return false; // base.ScaleChildren;
            }
        }

        protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
        {
            return base.GetScaledBounds(bounds, factor, specified);
        }

        //hack to override default control size
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            //this.Size = new Size(100, 22);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(SystemColors.WindowFrame, myPadding100), myPadding50, myPadding50, this.Width - myPadding150, this.Height - myPadding150);
            //if (this.Enabled)
            //{
            //    e.Graphics.DrawRectangle(new Pen(SystemColors.WindowFrame, myPadding100), myPadding50, myPadding50, this.Width - myPadding150, this.Height - myPadding150);
            //}
            //else
            //{
            //    e.Graphics.DrawRectangle(new Pen(SystemColors.GrayText, myPadding100), myPadding50, myPadding50, this.Width - myPadding150, this.Height - myPadding150);
            //}
        }

        private void UpArrow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.WindowFrame, myPadding100), myPadding50, myPadding50, UpArrow.Width - myPadding150, UpArrow.Height - myPadding150);
            //if (this.Enabled)
            //{
            //    e.Graphics.DrawRectangle(new Pen(SystemColors.WindowFrame, myPadding100), myPadding50, myPadding50, UpArrow.Width - myPadding150, UpArrow.Height - myPadding150);
            //}
            //else
            //{
            //    e.Graphics.DrawRectangle(new Pen(SystemColors.GrayText, myPadding100), myPadding50, myPadding50, UpArrow.Width - myPadding150, UpArrow.Height - myPadding150);
            //}
        }

        private void DownArrow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.WindowFrame, myPadding100), myPadding50, myPadding50, UpArrow.Width - myPadding150, DownArrow.Height - myPadding150);
            //if (this.Enabled)
            //{
            //    e.Graphics.DrawRectangle(new Pen(SystemColors.WindowFrame, myPadding100), myPadding50, myPadding50, UpArrow.Width - myPadding150, DownArrow.Height - myPadding150);
            //}
            //else
            //{
            //    e.Graphics.DrawRectangle(new Pen(SystemColors.GrayText, myPadding100), myPadding50, myPadding50, UpArrow.Width - myPadding150, DownArrow.Height - myPadding150);
            //}
        }

        #region Data type converters

        public Int32 ToInt32()
        {
            return (int)this._value;
        }

        public Int16 ToInt16()
        {
            return (short)this._value;
        }

        public byte ToByte()
        {
            return (byte)this.Value;
        }

        public decimal ToDecimal()
        {
            return this._value;
        }

        public float ToSingle()
        {
            return (float)this._value;
        }

        public double ToDouble()
        {
            return (double)this._value;
        }

        public Int64 ToInt64()
        {
            return (long)this._value;
        }

        public override string ToString()
        {
            return this.NumberTextBox.Text;
        }

        #endregion

        #region "Button Event Handlers"

        private bool _downMouseDown;
        private bool _upMouseDown;

        private void DownArrow_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!this.Enabled) return;

            _downMouseDown = true;
            this.DownArrow.Image = this._bmDownIn;
            this.DownTimer.Interval = 250;
            this.DownTimer.Enabled = true;
            this.Refresh();
        }

        private void UpArrow_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!this.Enabled) return;

            _upMouseDown = true;
            this.UpArrow.Image = this._bmUpIn;
            this.UpTimer.Interval = 250;
            this.UpTimer.Enabled = true;
            this.Refresh();
        }

        private void DownArrow_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.DownTimer.Enabled = false;
            if (!this.Enabled) return;
            _downMouseDown = false;

            if (this.Value - this.Increment >= this.Minimum)
                this.Value -= this.Increment;

            this.DownArrow.Image = this._bmDownOut;
            this.Refresh();
        }

        private void UpArrow_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.UpTimer.Enabled = false;
            if (!this.Enabled) return;
            _upMouseDown = false;

            if (this.Value + this.Increment <= this.Maximum)
                this.Value += this.Increment;

            this.UpArrow.Image = this._bmUpOut;
            this.Refresh();
        }

        private void UpTimer_Tick(object sender, System.EventArgs e)
        {
            if (!_upMouseDown) // failsafe.
            {
                this.UpTimer.Enabled = false;
                return;
            }

            if (this.Value + this.Increment <= this.Maximum)
                this.Value += this.Increment;

            if (this.UpTimer.Interval == 250) this.UpTimer.Interval = 100;

            this.Refresh();
        }

        private void DownTimer_Tick(object sender, System.EventArgs e)
        {
            if (!_downMouseDown) // failsafe.
            {
                this.DownTimer.Enabled = false;
                return;
            }

            if (this.Value - this.Increment >= this.Minimum)
                this.Value -= this.Increment;

            if (this.DownTimer.Interval == 250) this.DownTimer.Interval = 100;

            this.Refresh();
        }
        #endregion

        public decimal DefaultValue { get; set; }

    }
}