using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace activiser.Library.Forms
{
    public class ImageLabel : Control, System.ComponentModel.ISupportInitialize
    {
        private Bitmap _baseImage = null;
        private Bitmap _image;
        private Graphics _graphics;

        Color _transparentColor = Color.Transparent;

        public ImageLabel()
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
                return;
            }
           
            _inResize = true;
            bool resized = false;
            //Debug.WriteLine("Resizing Image Label...");
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

        
        private void locateText(Graphics g, ref int textLeft, ref int textTop)
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
                    textLeft = left;
                    textTop = middle;
                    break;
                case ContentAlignment.MiddleRight:
                    textLeft = right;
                    textTop = middle;
                    break;
                case ContentAlignment.TopCenter:
                    textLeft = center;
                    textTop = top;
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
        }

        private void drawMe(Graphics g)
        {

            int textLeft = Padding.Left;
            int textTop = Padding.Top;
            if (this.TextVisible && !string.IsNullOrEmpty(this.Text))
            {
                locateText(g, ref textLeft, ref textTop);
            }

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
                g.DrawImage(_image, base.ClientRectangle, 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, ia);

            
            if (this.TextVisible && !string.IsNullOrEmpty(this.Text))
            {
                g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textLeft, textTop);
            }

            switch (this.BorderStyle)
            {
                case BorderStyle.Fixed3D:
                    int topX = this.ClientSize.Width - 2;
                    int topY = this.ClientSize.Height - 2;
                    g.DrawLines(new Pen(SystemColors.ControlDarkDark, 2), new Point[] { new Point(0, topY), new Point(topX, topY), new Point(topX, 0) });
                    g.DrawLines(new Pen(SystemColors.ControlLightLight, 2), new Point[] { new Point(0, topY), new Point(0, 0), new Point(topX, 0) });
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
                //clear backgrounds. 
                _transparentColor = _baseImage.GetPixel(0, 0);

                if (this.TransparentBackground)
                {
                    _graphics.Clear(this._transparentColor);
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
                            _graphics.Clear(SystemColors.Window);
                        }
                    }
                    else
                    {
                        _graphics.Clear(this._backColor);
                    }
                }

                if ((this.ImageAlignment & ContentAlignment.Fill) != ContentAlignment.Fill)
                {
                    GraphicsUtilities.AlignBitmap(this._baseImage, this._image, ImageAlignment, Padding);
                }
                else
                {
                    if (this.TransparentBackground)
                    {
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image, this._transparentColor, this._backColor);
                    }
                    else
                    {
                        GraphicsUtilities.StretchBitmap(this._baseImage, this._image);
                    }
                }
            }

            else
            {
                _graphics.Clear(this._backColor);
            }
        }

        private void setupImages()
        {
            if (!(this.ClientSize.Width > 0 && this.ClientSize.Height > 0)) return;

            this.disposeImages();
            _image = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format32bppRgb);

            _graphics = Graphics.FromImage(_image);

            this.SetPlainImage();
            _graphics.Dispose();
            
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
                if (value != null)
                {
                    this._baseImage = value as Bitmap;
                }
                if (_initializing != 0) return;
                ResizeMe();
                setupImages();
            }
        }

        public void ResetImage()
        {
            if (this._baseImage != null) this._baseImage.Dispose();
            this._baseImage = null;
            if (_initializing != 0) return;
            ResizeMe();
        }

        private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.Normal;
        public PictureBoxSizeMode SizeMode
        {
            get { return _sizeMode; }
            set
            {
                _sizeMode = value;
                ResizeMe();
            }
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
    } 
}
