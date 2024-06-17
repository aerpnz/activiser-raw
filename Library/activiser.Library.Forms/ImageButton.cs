using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace activiser.Library.Forms
{
    public class ImageButton : Control, System.ComponentModel.ISupportInitialize
    {
        private Bitmap _baseImage = null;
        private Bitmap _image;

        private Bitmap _baseDownImage;
        private Bitmap _downImage;
        private bool _bPushed;

        private Graphics _graphics;
        private Graphics _gDown;

        Color _transparentColor = Color.Transparent;
        Color _downTransparentColor = Color.Transparent;

        public ImageButton()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.disposeImages();
                if (this._baseImage != null)
                {
                    this._baseImage.Dispose();
                }
            }
        }

        #region "Event handlers"
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (_updating != 0) return;
            _bPushed = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (_updating != 0) return;
            _bPushed = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        private int _updating = 0;
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (_initializing != 0) return;
            drawMe(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            if (_initializing != 0) return;
            base.OnPaintBackground(e);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            if (_inEndInit) return;
            if (_updating != 0) return;
            if (_inResize)
            {
#if DEBUG
                // simple hack to get stack trace.
                try
                {
                    throw new ApplicationException("Already in resize");
                }
                catch (ApplicationException ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
#endif
                return;
            } 
            if (AutoSize) ResizeMe();
        }


        private bool _inResize;
        private void ResizeMe()
        {
            if (_initializing != 0) return;
            if (_updating != 0) return;
            if (_inResize) return;
            if (!AutoSize || this.Dock == DockStyle.Fill)
            {
                setupImages(); // all calls to ResizeMe also setup the images.
                this.Invalidate();
                return;
            }
           
            _inResize = true;
            bool resized = false;
            //Debug.WriteLine("Resizing Image Button...");
            try
            {
                if (!this.TextVisible)
                {
                    if (Image != null)
                    {
                        Size s = new Size(this.Image.Width + Padding.Horizontal, this.Image.Height + this.Padding.Vertical);
                        if (this.Dock != DockStyle.None)
                        {
                            if ((this.Dock & DockStyle.Left | DockStyle.Right) != 0)
                            {
                                s.Height = this.Height;
                            }
                            else if ((this.Dock & DockStyle.Top | DockStyle.Bottom) != 0)
                            {
                                s.Width = this.Width;
                            }
                        }
                        if (this.Size != s)
                        {
                            this.Size = s;
                            resized = true;
                        }
                    }
                }
                else
                {
                    Graphics g = this.CreateGraphics();
                    Size s = g.MeasureString(base.Text, base.Font).ToSize();
                    //Debug.WriteLine(string.Format("String size: {0} x {1}", s.Width, s.Height));
                    //Debug.WriteLine("Padding: " + Padding.ToString());
                    s.Width += Padding.Horizontal;
                    s.Height += Padding.Vertical;
                    //Debug.WriteLine(string.Format("With padding: {0} x {1}", s.Width, s.Height));
                    if (Image != null)
                    {
                        //Debug.WriteLine(string.Format("Image size: {0} x {1}",  Image.Size.Width, Image.Size.Height));

                        ContentAlignment ta = this.TextAlignment;
                        if (ta == ContentAlignment.MiddleCenter)
                        {
                            //Debug.WriteLine("No padding - text centered");
                            // smack bang in the middle, we're overwriting the image and don't care about avoiding it.
                        }
                        else if (ta == ContentAlignment.MiddleLeft || ta == ContentAlignment.MiddleRight)
                        {
                            //Debug.WriteLine("Horizontal padding");
                            // left or right of the image, we need to add horizontal padding...
                            s.Width += this.Image.Width + this.Padding.Horizontal;
                        }
                        else 
                        {
                            //Debug.WriteLine("Vertical padding");
                            // above or below the image, we need to add vertical padding...
                            s.Height += this.Image.Height + Padding.Top;
                        }

                        // whatever padding and text size, we need to make sure we include the whole image...
                        s.Width = Math.Max(s.Width, this.Image.Width + Padding.Horizontal);
                        s.Height = Math.Max(s.Height, this.Image.Height + Padding.Vertical);

                    }
   
                    //Debug.WriteLine(string.Format("Result: {0} x {1}", s.Width, s.Height));
                    if (this.Dock != DockStyle.None)
                    {
                        if ((this.Dock & DockStyle.Left | DockStyle.Right) != 0)
                        {
                            s.Height = this.Height;
                        }
                        else if ((this.Dock & DockStyle.Top | DockStyle.Bottom) != 0)
                        {
                            s.Width = this.Width;
                        }
                    }
                    if (this.Size != s)
                    {
                        this.Size = s;
                        resized = true;
                    }
                }
            }
            finally
            {
                _inResize = false;
                if (resized)
                {
                    setupImages();
                    this.Invalidate();
                }
            }
        }

        
        private Rectangle LocateText(Graphics g, ref int textLeft, ref int textTop)
        {
            Size s = g.MeasureString(base.Text, base.Font).ToSize();
            int left = Padding.Left;
            int right = this.ClientSize.Width - s.Width - 2;
            int center = this.ClientSize.Width / 2 - s.Width / 2;
            int middle = this.ClientSize.Height / 2 - s.Height / 2;
            int top = Padding.Top;
            int bottom = this.ClientSize.Height - s.Height - 2;
            switch (this.TextAlignment)
            {
                case ContentAlignment.BottomCenter:
                    textLeft = center;
                    textTop = bottom;
                    textTop = this.ImageAlignment == ContentAlignment.BottomCenter ? bottom - this.Image.Height : bottom;
                    break;
                case ContentAlignment.BottomLeft:
                    textLeft = left;
                    textTop = bottom;
                    break;
                case ContentAlignment.BottomRight:
                    textLeft = right;
                    textTop = bottom;
                    break;
                case ContentAlignment.MiddleCenter:
                    textLeft = center;
                    textTop = middle;
                    break;
                case ContentAlignment.MiddleLeft:
                    textLeft = this.ImageAlignment == ContentAlignment.MiddleLeft ? left + this.Image.Width : left;
                    textTop = middle;
                    break;
                case ContentAlignment.MiddleRight:
                    textLeft = this.ImageAlignment == ContentAlignment.MiddleRight ? right - this.Image.Width : right;
                    textTop = middle;
                    break;
                case ContentAlignment.TopCenter:
                    textLeft = center;
                    textTop = this.ImageAlignment == ContentAlignment.TopCenter ? top + this.Image.Height : top;
                    break;
                case ContentAlignment.TopLeft:
                    textLeft = left;
                    textTop = top;
                    break;
                case ContentAlignment.TopRight:
                    textLeft = right;
                    textTop = top;
                    break;
            }
            return new Rectangle(textLeft, textTop, s.Width + 1, s.Height + 1);
        }

        private void drawMe(Graphics g)
        {

            int textLeft = Padding.Left;
            int textTop = Padding.Top;
            Rectangle textRect;
            if (this.TextVisible && !string.IsNullOrEmpty(this.Text))
                textRect = LocateText(g, ref textLeft, ref textTop);
            else
                textRect = new Rectangle(0, 0, 0, 0);

            ImageAttributes ia = new ImageAttributes();
            if (TransparentBackground)
            {
                ia.SetColorKey(_transparentColor, _transparentColor);
            }
            if (this._backColor == Color.Transparent)
            {
                if (Parent != null)
                {
                    g.Clear(this.Parent.BackColor);
                    g.FillRectangle(new SolidBrush(this.Parent.BackColor), this.ClientRectangle);
                }
                else
                {
                    g.Clear(SystemColors.Control);
                }
            }
            else
            {
                g.Clear(this._backColor);
                g.FillRectangle(new SolidBrush(this._backColor), this.ClientRectangle);
            }

            if (_image != null)
            {
                if (_bPushed)
                {
                    g.DrawImage(_downImage, base.ClientRectangle, 0, 0, _downImage.Width, _downImage.Height, GraphicsUnit.Pixel, ia);
                }
                else
                {
                    g.DrawImage(_image, base.ClientRectangle, 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, ia);
                }
            }

            if (this.TextVisible && !string.IsNullOrEmpty(this.Text))
            {
                StringFormat sf = new StringFormat();
 
                if ((this.TextAlignment & ContentAlignment.Left) != 0) sf.LineAlignment = StringAlignment.Near;
                else if ((this.TextAlignment & ContentAlignment.Center) != 0) sf.LineAlignment = StringAlignment.Center;
                else sf.LineAlignment = StringAlignment.Far;

                if ((this.TextAlignment & ContentAlignment.Left) != 0) sf.Alignment = StringAlignment.Near;
                else if ((this.TextAlignment & ContentAlignment.Center) != 0) sf.Alignment = StringAlignment.Center;
                else sf.Alignment = StringAlignment.Far;

                sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
                g.DrawString(this.Text + ' ', this.Font, new SolidBrush(this.ForeColor), textRect, sf);
            }

            switch (this.BorderStyle)
            {
                case BorderStyle.Fixed3D:
                    int topX = this.ClientSize.Width - 2;
                    int topY = this.ClientSize.Height - 2;
                    if (this._bPushed)
                    {
                        g.DrawLines(new Pen(SystemColors.ControlLightLight, 2), new Point[] { new Point(0, topY), new Point(0, 0), new Point(topX, 0) });
                        g.DrawLines(new Pen(SystemColors.ControlDarkDark, 2), new Point[] { new Point(0, topY), new Point(topX, topY), new Point(topX, 0) });
                    }
                    else
                    {
                        g.DrawLines(new Pen(SystemColors.ControlDarkDark, 2), new Point[] { new Point(0, topY), new Point(topX, topY), new Point(topX, 0) });
                        g.DrawLines(new Pen(SystemColors.ControlLightLight, 2), new Point[] { new Point(0, topY), new Point(0, 0), new Point(topX, 0) });
                    }

                    break;
                case BorderStyle.FixedSingle:
                    g.DrawRectangle(new Pen(SystemColors.ControlDarkDark, 1), 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
                    break;
                case BorderStyle.None:
                    break;
            }
        }
        #endregion

        #region "Image Setup"

        private void SetPlainImage()
        {
            if ((_baseImage != null))
            {
                _baseDownImage = GraphicsUtilities.CopyBitmap(_baseImage, null, null);
                //clear backgrounds. 
                _transparentColor = _baseImage.GetPixel(0, 0);
                _downTransparentColor = _transparentColor;

                if (this.TransparentBackground)
                {
                    _graphics.Clear(this._transparentColor);
                    _gDown.Clear(this._downTransparentColor);
                }
                else
                {
                    if (this.BackColor == Color.Transparent)
                    {
                        if (this.Parent != null)
                        {
                            _graphics.Clear(this.Parent.BackColor);
                            _gDown.Clear(this.Parent.BackColor);
                        }
                        else
                        {
                            _graphics.Clear(SystemColors.Window);
                            _gDown.Clear(SystemColors.Window);
                        }
                    }
                    else
                    {
                        _graphics.Clear(this._backColor);
                        _gDown.Clear(this._backColor);
                    }
                }

                if ((this.ImageAlignment & ContentAlignment.Fill )!= ContentAlignment.Fill)
                {
                    GraphicsUtilities.AlignBitmap( this._baseImage,  this._image, ImageAlignment, Padding);
                    GraphicsUtilities.AlignBitmap( this._baseDownImage,  this._downImage, ImageAlignment, Padding);
                }
                else
                { // Stretch Image {
                    if (this.TransparentBackground)
                    {
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image, _transparentColor, _backColor);
                        GraphicsUtilities.StretchBitmap(this._baseDownImage, this._downImage, _transparentColor, _backColor);
                    }
                    else
                    {
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image);
                        GraphicsUtilities.StretchBitmap(this._baseDownImage, this._downImage);
                    }
                }
            }

            else
            {
                _graphics.Clear(this._backColor);
                _gDown.Clear(this._backColor);
            }
        }

        private void setupImages()
        {
            if (!(this.ClientSize.Width > 0 && this.ClientSize.Height > 0)) return;

            this.disposeImages();
            _image = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format32bppRgb);
            _downImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format32bppRgb);

            _graphics = Graphics.FromImage(_image);
            _gDown = Graphics.FromImage(_downImage);

            if (this.InvertOnClick)
            {
                this.SetInvertedImage();
            }
            else if (this.ClickMaskColor == Color.Empty)
            {
                this.SetPlainImage();
            }
            else
            {
                this.SetMaskImage();
            }
            _bPushed = false;
            _graphics.Dispose();
            _gDown.Dispose();

            if (_initializing != 0) return;
            this.Invalidate();
        }
        #endregion

        #region "Properties"

        /// <summary>
        /// The image displayed on the button
        /// </summary>
        public System.Drawing.Image Image
        {
            get { return this._baseImage; }
            set
            {
                if (object.ReferenceEquals(value, this._baseImage))
                    return;

                if (this._baseImage != null) this._baseImage.Dispose();
                if (this._baseDownImage != null) this._baseDownImage.Dispose();

                if (value != null)
                {
                    this._baseImage = value as Bitmap;
                }
                ResizeMe();
            }
        }

        public void ResetImage()
        {
            if (this._baseImage != null) this._baseImage.Dispose();
            if (this._baseDownImage != null) this._baseDownImage.Dispose();
            this._baseImage = null;
            this._baseDownImage = null;
            ResizeMe();
        }

       private bool _transparentBackground = true;
        public bool TransparentBackground
        {
            get { return _transparentBackground; }
            set
            {
                if (value != this._transparentBackground)
                {
                    _transparentBackground = value;
                    if (_initializing != 0) return;
                    setupImages();
                }
            }
        }

        public override System.Drawing.Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        private Color _backColor = SystemColors.Control;
        public new System.Drawing.Color BackColor
        {
            get { return _backColor; }
            set
            {
                if (value != _backColor)
                {
                    _backColor = value;
                    if (_initializing != 0) return;
                    setupImages();
                }
            }
        }

        private BorderStyle _borderStyle = BorderStyle.None;
        public BorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                if (value != this._borderStyle)
                {
                    _borderStyle = value;
                    if (_initializing != 0) return;
                    setupImages();
                }
            }
        }

        private ContentAlignment _textAlignment = ContentAlignment.BottomCenter;
        public ContentAlignment TextAlignment
        {
            get { return _textAlignment; }
            set
            {
                _textAlignment = value;
                if (_autoSize)
                {
                    ContentAlignment newImageAlignment;

                    // switch left & right
                    newImageAlignment = (ContentAlignment)((int)value ^ 0x505);

                    // switch top & bottom
                    if (((int)value & 0x00F) != 0) // top
                    {
                        newImageAlignment = (ContentAlignment)((int)newImageAlignment << 8);
                    }
                    else if (((int)value & 0xF00) != 0) // bottom
                    {
                        newImageAlignment = (ContentAlignment)((int)newImageAlignment >> 8);
                    }
                    _imageAlignment = newImageAlignment;
                }
                ResizeMe();
            }
        }

        private ContentAlignment _imageAlignment = ContentAlignment.MiddleCenter;
        public ContentAlignment ImageAlignment
        {
            get { return _imageAlignment; }
            set
            {
                _imageAlignment = value;
                ResizeMe();
            }
        }

        internal void ResetImageAlignment()
        {
            _imageAlignment = ContentAlignment.MiddleCenter;
            ResizeMe();
        }

        #endregion

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            ResizeMe();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            ResizeMe();
        }

        private void disposeImages()
        {
            if (this._image != null) this._image.Dispose();
            if (this._graphics != null) this._graphics.Dispose();
            if (this._downImage != null) this._downImage.Dispose();
            if (this._baseDownImage != null) this._baseDownImage.Dispose();
            if (this._gDown != null) this._gDown.Dispose();
        }

        private bool _textVisible = true;
        public bool TextVisible
        {
            get
            {
                return _textVisible;
            }
            set
            {
                _textVisible = value;
                ResizeMe();
            }
        }

        public void BeginUpdate() { _updating++; }

        public void EndUpdate() { 
            if (_updating > 0) _updating--;
            ResizeMe();
        }


        #region ISupportInitialize Members

        private int _initializing = 0;
        void System.ComponentModel.ISupportInitialize.BeginInit()
        {
            _initializing++;
        }

        private bool _inEndInit;
        void System.ComponentModel.ISupportInitialize.EndInit()
        {
            _inEndInit = true;
            if (_initializing > 0) _initializing--;
            ResizeMe();
            _inEndInit = false;
        }

        #endregion

        private bool _autoSize = false;
        public bool AutoSize
        {
            get
            {
                return _autoSize;
            }
            set
            {
                _autoSize = value;
                ResizeMe();
            }
        }


        private activiser.Library.Forms.Padding _padding = new Padding(2,2,2,2);
        public activiser.Library.Forms.Padding Padding
        {
            get
            {
                return _padding;
            }
            set
            {
                _padding = value;
                ResizeMe();
            }
        }
        
        private bool _invertOnClick;
        public bool InvertOnClick
        {
            get { return _invertOnClick; }
            set
            {
                if (value != _invertOnClick)
                {
                    _invertOnClick = value;
                    if (_initializing != 0) return;
                    setupImages();
                }
            }
        }

        private Color _maskedClickColor = SystemColors.Control;
        public Color ClickMaskColor
        {
            get { return _maskedClickColor; }
            set
            {
                if (value != this._maskedClickColor)
                {
                    _maskedClickColor = value;
                    if (_initializing != 0) return;
                    setupImages();
                }
            }
        }

        private void SetMaskImage()
        {
            //step one, create an inverted base image. 
            if ((_baseImage != null))
            {

                _baseDownImage = new Bitmap(_baseImage); // GraphicsUtilities.CopyBitmap(_baseImage, null, null);
                _transparentColor = _baseImage.GetPixel(0, 0);
                _downTransparentColor = _transparentColor;

                GraphicsUtilities.RecolorBitmap(this._baseDownImage, _transparentColor, _maskedClickColor);

                if (this.TransparentBackground)
                {
                    _graphics.Clear(this._transparentColor);
                    _gDown.Clear(this._maskedClickColor);
                }
                else
                {
                    if (this.BackColor == Color.Transparent)
                    {
                        if (this.Parent != null)
                        {
                            _graphics.Clear(this.Parent.BackColor);
                        }
                        else
                        {
                            _graphics.Clear(SystemColors.Control);
                        }
                        _gDown.Clear(this._maskedClickColor);
                    }
                    else
                    {
                        _graphics.Clear(this._backColor);
                        _gDown.Clear(this._maskedClickColor);
                    }
                }

                if ((this.ImageAlignment & ContentAlignment.Fill) != ContentAlignment.Fill)
                {
                    GraphicsUtilities.AlignBitmap(this._baseImage, this._image, ImageAlignment, Padding);
                    GraphicsUtilities.AlignBitmap(this._baseDownImage, this._downImage, ImageAlignment, Padding);
                    //if (this.TransparentBackground)
                    //{
                    //    GraphicsUtilities.RecolorBitmap(this._baseImage, _backColor, _transparentColor);
                    //}
                }
                else
                {
                    if (this.TransparentBackground)
                    {
                        // need to recolor before stretching, otherwise the 'mask color' will get dithered. 
                        // for 'mask'-style button, the transparency has already been replaced by the 'mask' color.
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image, this._transparentColor, this._backColor);
                        GraphicsUtilities.StretchBitmap(this._baseDownImage, this._downImage);
                    }
                    else
                    {
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image);
                        GraphicsUtilities.StretchBitmap(this._baseDownImage, this._downImage);
                    }
                }
            }
            else
            {
                _graphics.Clear(this._backColor);
                _gDown.Clear(this._maskedClickColor);
            }
        }
        private void SetInvertedImage()
        {
            //step one, create an inverted base image. 
            if ((_baseImage != null))
            {
                _baseDownImage = GraphicsUtilities.NegateBitmap(_baseImage);
                _transparentColor = _baseImage.GetPixel(0, 0);
                _downTransparentColor = _baseDownImage.GetPixel(0, 0);
                Color backgroundNegative = GraphicsUtilities.NegateColor(this.Parent.BackColor);

                if (this.TransparentBackground)
                {
                    _graphics.Clear(this._transparentColor);
                    _gDown.Clear(this._downTransparentColor);
                }
                else
                {
                    if (this.BackColor == Color.Transparent)
                    {
                        if (this.Parent != null)
                        {
                            _graphics.Clear(this.Parent.BackColor);
                            
                            _gDown.Clear(backgroundNegative);
                        }
                        else
                        {
                            _graphics.Clear(SystemColors.Control);
                            _gDown.Clear(SystemColors.ScrollBar);
                        }
                    }
                    else
                    {
                        _graphics.Clear(this._backColor);
                        _gDown.Clear(GraphicsUtilities.NegateColor(this._backColor));
                    }
                }

                if ((this.ImageAlignment & ContentAlignment.Fill) != ContentAlignment.Fill)
                {
                    GraphicsUtilities.AlignBitmap(this._baseImage, this._image, ImageAlignment, Padding);
                    GraphicsUtilities.AlignBitmap(this._baseDownImage, this._downImage, ImageAlignment, Padding);
                    if (this.TransparentBackground)
                    {
                        GraphicsUtilities.RecolorBitmap(this._image, _backColor, _transparentColor);
                        GraphicsUtilities.RecolorBitmap(this._downImage, backgroundNegative, _downTransparentColor);
                    }
                }
                else
                {
                    if (this.TransparentBackground)
                    {
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image, this._transparentColor, this._backColor);
                        GraphicsUtilities.StretchBitmap(this._baseDownImage, this._downImage, this._transparentColor, this._backColor);
                    }
                    else
                    {
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image);
                        GraphicsUtilities.StretchBitmap(this._baseDownImage, this._downImage);
                    }
                }
            }
            else
            {
                _graphics.Clear(this._backColor);
                _gDown.Clear(GraphicsUtilities.NegateColor(this._backColor));
            }
        }

    } 
}
