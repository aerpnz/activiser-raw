using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;

namespace activiser.Library.Forms
{
    /// <summary>
    /// Capture a signature as an array of vectors.
    /// </summary>
    [DesignerCategory("Form")]
    public class Signature : Control
	{
        public const int XAxisBasis = 768;
        public const int YAxisBasis = 256;

        private const int maxLines = 63;            // more than this is just plain silly. even this many is just plain silly.
        private const int maxPointsPerLine= 63;    // 63 * 63 = 3969 points. on a typical 72 x 216 capture pad on QVGA screen,
                                                    // this would have to pretty much fill the pad.

        // version number
        private const int versionLength = 6;
        private const string signaturePrefix = "#V";
        private static string currentVersion = "#V040#";
        private static byte[] currentVersionBytes = { 
            (byte)currentVersion[0], 
            (byte)currentVersion[1],  
            (byte)currentVersion[2],  
            (byte)currentVersion[3],  
            (byte)currentVersion[4], 
            (byte)currentVersion[5]
        };

        private static List<string> supportedVersions = new List<string>(new string[] { currentVersion, "#V35#:", "#V33#:" });
        private bool maxedOut;

        private BorderStyle _borderStyle = BorderStyle.FixedSingle;
		private Color _borderColor; // = Color.Black;
        private Point _lastPoint;
        private Point _previousPoint;
        private Point _nextPoint;
		private int _lastX;
		private int _lastY;
		private bool _hasCapture ; // = false;
        private Pen _linePen = new Pen(Color.Black);
        private string _text = "";
        private long _timestamp;

        private string _sourceSignature;
        private List<byte> _sourceArray = new List<byte>();

        //double scaleX = 1;
        //double scaleY = 1;

		List<Point> currentLine = new List<Point>();
        List<Point[]> totalLines = new List<Point[]>();

        //public delegate void OnSignatureChangedEventHandler(Object sender, EventArgs e);
        public event EventHandler<EventArgs> SignatureChanged;

		static char[] lookupTable=new char[64]
	      {
			  'A','B','C','D','E','F','G','H','I','J','K','L','M',
			  'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
			  'a','b','c','d','e','f','g','h','i','j','k','l','m',
			  'n','o','p','q','r','s','t','u','v','w','x','y','z',
			  '0','1','2','3','4','5','6','7','8','9','+','/'
          };

        private static char sixbit2char(byte b)
        {
            if ((b >= 0) && (b <= 63))
            {
                return lookupTable[(int)b];
            }
            if (b > 63) return lookupTable[63];
            return lookupTable[0];
        }

        private static byte char2sixbit(char c)
        {
            if (c == '=')
                return 0;
            else
            {
                for (int x = 0; x < 64; x++)
                {
                    if (lookupTable[x] == c)
                        return (byte)x;
                }
                return 0; //should not reach here
            }
        }

        private static char[] int18ToBase64(int b)
        {
            byte b1, b2, b3; // , b4;
            b1 = (byte) ((b & 0x3F));
            b2 = (byte) ((b & (0x3F << 6)) >> 6);
            b3 = (byte) ((b & (0x3F << 12)) >> 12);
            // b4 = (byte) ((b & (0x3F << 18)) >> 18);
            char[] result = { sixbit2char(b1), sixbit2char(b2), sixbit2char(b3)}; // , sixbit2char(b4) };
            return result;
        }

        private static int int18FromBase64(char[] c)
        {
            if (c.Length != 3) { throw new ArgumentException(Properties.Resources.int18DecodeNeeds3Characters,"c"); }
            int b1, b2, b3; //, b4;
            b1 = char2sixbit(c[0]);
            b2 = char2sixbit(c[1]);
            b3 = char2sixbit(c[2]);
            // b4 = char2sixbit(c[3]);
            int result = b1 | (b2<<6) | (b3<<12); // | (b4<<18);
            return result;
        }

        public static string Prefix
        {
            get
            {
                return signaturePrefix;
            }
        }

        public string SignatureVersion
        {
            get
            {
                return this._sourceSignature.Substring(0, versionLength);
            }
        }

        public static int VersionLength
        {
            get
            {
                return versionLength;
            }
        }

        public Signature()
            : base()
		{
		}

        //~Signature()
        //{
        //}
        
		public Color BorderColor
		{
			get
			{
				return _borderColor;
			}
			set
			{
				_borderColor = value;
				Invalidate();
			}
		}

        public override string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                this.Invalidate();
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (base.Font != value)
                {
                    base.Font = value;
                    this.Invalidate();
                }
            }
        }

        private void drawSignature(Graphics g) {
            Point previousPoint = new Point(0,0), currentPoint;

            g.Clear(this.BackColor);
            foreach (Point[] line in totalLines)
            {
                if (line.Length == 0 || line.Length == 1)
                    continue;

                // if (previousPoint.X == 0 && previousPoint.Y == 0) { previousPoint = (Point)line[0]; }
                previousPoint = (Point)line[0]; 
                for (int x = 1; x < line.Length; x++)
                {
                    currentPoint = line[x];
                    g.DrawLine(_linePen, previousPoint.X, previousPoint.Y, currentPoint.X, currentPoint.Y);
                    previousPoint = currentPoint;
                }
//#if DEBUG
//                linePen.Color = linePen.Color == Color.Black ? Color.DarkBlue : Color.Black;
//#endif
            }

            string textValue = this.Text + ' '; // add a buffer, since we seem to be truncating.
            Font fontValue = this.Font;
            SizeF textSize = SizeF.Empty;
            int topOffset ;
            int leftOffset ;
            Rectangle textRect = Rectangle.Empty;

            if ((textValue != null) && (fontValue != null))
            {
                if (textValue.Length > 0)
                {
                    textSize = g.MeasureString(textValue, fontValue);
                    topOffset = this.Height - Convert.ToInt32(textSize.Height * 1.5);
                    leftOffset = this.Width - Convert.ToInt32(textSize.Width + (textSize.Height / 2));
                    textRect = new Rectangle(leftOffset, topOffset, Convert.ToInt32(textSize.Width) + 10, Convert.ToInt32(textSize.Height));
                    SolidBrush textBrush;

                    textBrush = new SolidBrush(SystemColors.GrayText);

                    g.DrawString(textValue, fontValue, textBrush, textRect);

                    textBrush.Dispose();
                }
            }
        }

		public Bitmap ToBitmap()
		{
            Bitmap result = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(result);
            drawSignature(g);
            return result;
        }

        private void GetSignatureByteArray()
        {
            if (totalLines.Count == 0) { return; }

            double scaleX = (double)XAxisBasis / (double)this.Width;
            double scaleY = (double)YAxisBasis / (double)this.Height;

            System.Collections.Generic.List<byte> Ls = new System.Collections.Generic.List<byte>();
            System.Collections.Generic.List<int> Ps = new System.Collections.Generic.List<int>();
            foreach (Point[] line in totalLines)
            {
                if (line.Length < 64)
                {
                    Ls.Add((byte)line.Length);
                    foreach (Point p in line)
                    {
                        int pc = ((int)(p.X * scaleX) * YAxisBasis) + (int)(p.Y * scaleY);
                        Ps.Add(pc);
                    }
                }
            }
            System.Collections.Generic.List<byte> resultList = new System.Collections.Generic.List<byte>(10 + Ls.Count + Ps.Count * 3); // + Xs.Count  * 2);
            resultList.AddRange(currentVersionBytes);
            resultList.Add((byte)(Ls.Count & 0xFF)); // don't support more than 256 'lines' of =< 64 points !
            resultList.AddRange(BitConverter.GetBytes(_timestamp));
            resultList.AddRange(Ls);
            foreach (int p in Ps)
            {
                byte b1, b2, b3;
                b1 = (byte)((p & 0x3F));
                b2 = (byte)((p & (0x3F << 6)) >> 6);
                b3 = (byte)((p & (0x3F << 12)) >> 12);
                resultList.Add(b1);
                resultList.Add(b2);
                resultList.Add(b3);
            }
            _sourceArray = resultList; // .ToArray();
            // return _sourceArray;
        }

        private void SetSignatureByteArray(byte[] signature)
        {
            double scaleX = (double)this.Width / (double)XAxisBasis;
            double scaleY = (double)this.Height / (double)YAxisBasis;
            int i, j, x, y, p, xy;

            char[] version = new char[6];

            for (j = 0; j < versionLength; j++)
            {
                version[j] = (char) signature[j];
            }

            bool versionOk = supportedVersions.Contains(new string(version));
            if (!versionOk)
                {
                throw new ArgumentException("Supplied string is not a valid signature");
            }


            try
                {
                    i = versionLength;
                    byte noOfLines = signature[i++]; // number of lines represented.
                    _timestamp = BitConverter.ToInt64(signature, j);
                    j += 8;
                    byte[] Ls = new byte[noOfLines];

                    int totalPoints = 0;
                    for (i = 0; i < noOfLines; i++)
                    {
                        Ls[i] = signature[j++];
                        totalPoints += Ls[i];
                    }

                    int[] Ps = new int[totalPoints];
                    for (i = 0; i < totalPoints; i++, j += 3)
                    {
                        byte[] b = new byte[3];
                        Array.Copy(signature, j, b, 0, 3);
                        p = (int)b[0] | ((int)b[1] << 6) | ((int)b[2] << 12);
                        Ps[i] = p;
                    }

                    for (p = 0, xy = 0; p < noOfLines; p++)
                    {
                        for (j = 0; j < Ls[p]; j++, xy++)
                        {
                            int cp = Ps[xy];
                            y = (int)((cp % YAxisBasis) * scaleY);
                            x = (int)(((cp - y) / YAxisBasis) * scaleX);
                            currentLine.Add(new Point(x, y));
                        }
                        totalLines.Add(currentLine.ToArray());
                        currentLine.Clear();
                    }
                    Invalidate();
                }
                catch
                {
                    totalLines.Clear();
                    currentLine.Clear();
                    throw new ArgumentException("Supplied string is not a valid signature");
                }

            }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<byte> SignatureByteArray
        {
            get {
                if (_sourceArray.Count == 0) { this.GetSignatureByteArray(); }
                return _sourceArray;
            }
            //set
            //{
            //    _sourceArray.Clear();
            //    _sourceArray.AddRange(value);
            //    this.SetSignatureByteArray(value.ToArray());
            //}
        }

        public void LoadSignatureBytes(byte[] value)
        {
            _sourceArray.Clear();
            _sourceArray.AddRange(value);
            this.SetSignatureByteArray(value);
        }
        public string SignatureString
        {
            get { return this.GetSignature(); }
            set { _sourceSignature = value; this.SetSignature(value); }
        }

        private string GetSignature()
        {
            try
            {
                if (totalLines.Count == 0)
                {
                    return string.Empty;
                }

                if (!string.IsNullOrEmpty(_sourceSignature))
                {
                    return _sourceSignature;
                }
                
                double scaleX = (double)XAxisBasis / (double)this.Width;
                double scaleY = (double)YAxisBasis / (double)this.Height;
                System.Text.StringBuilder sb;
                System.Collections.Generic.List<byte> Ls = new System.Collections.Generic.List<byte>();
                System.Collections.Generic.List<int> Ps = new System.Collections.Generic.List<int>();
                int top = 0;
                foreach (Point[] line in totalLines)
                {
                    if (line.Length < 64)
                    {
                        Ls.Add((byte)line.Length);
                        foreach (Point p in line)
                        {
                            int pc = ((int)(p.X * scaleX) * YAxisBasis) + (int)(p.Y * scaleY);
                            if (pc > top) top = pc;
                            Ps.Add(pc);
                        }
                    }
                }
                Debug.WriteLine(top);
                Debug.WriteLine(new string(int18ToBase64(top)));
                byte pl = (byte)(Ls.Count & 0x3F); // don't support more than 63 'lines' of =< 63 points !
                sb = new System.Text.StringBuilder(currentVersion, Ls.Count + Ps.Count * 3);
                sb.Append(sixbit2char(pl)); 
                
                sb.Append(Convert.ToBase64String(BitConverter.GetBytes(_timestamp)));
                foreach (byte l in Ls) { sb.Append(sixbit2char(l)); } // limited in code to 1 x b64 digit - Max points per line = 63
                sb.Append('=');
                foreach (int p in Ps) { sb.Append(int18ToBase64(p)); }
                sb.Append('=');
                _sourceSignature = sb.ToString();
                return _sourceSignature;
            }
            catch //(ObjectDisposedException ex) // hopper special
            {
                return string.Empty;
            }
        }

        private void SetSignature(string signature)
        {
            if (string.IsNullOrEmpty(signature))
            {
                totalLines.Clear();
                currentLine.Clear();
                return;
            }

            double scaleX = (double)this.Width / (double) XAxisBasis;
            double scaleY = (double)this.Height / (double) YAxisBasis;
            bool versionOk = supportedVersions.Contains(signature.Substring(0, versionLength));
            if (versionOk)
            {
                try
                {
                    char[] sigArray = signature.ToCharArray();
                    // char[] noOfLinesInHex = new char[2];
                    // Array.Copy(sigArray, magic.Length, noOfLinesInHex, 0, 2);
                    int noOfLines = char2sixbit(sigArray[versionLength]); // int.Parse(new string(noOfLinesInHex), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                    byte[] Ls = new byte[noOfLines];

                    int i, j, x, y, p, xy;
                    int totalPoints = 0;
                    j = versionLength + 1; // get past linecount
                    _timestamp = BitConverter.ToInt64(Convert.FromBase64String(signature.Substring(j, 12)), 0);
                    j += 12; //length of B64 
                    // calculate total number of points.
                    for (i = 0; i < noOfLines; i++)
                    {
                        Ls[i] = char2sixbit(sigArray[j++]);
                        totalPoints += Ls[i];
                    }
                    j++;
                    int[] Ps = new int[totalPoints];
                    for (i = 0; i < totalPoints; i++, j+=3)
                    {
                        char[] tmp = new char[3];
                        Array.Copy(sigArray, j, tmp, 0, 3); 
                        Ps[i] = int18FromBase64(tmp);
                    }

                    totalLines.Clear();
                    currentLine.Clear();
                    for (p = 0, xy = 0; p < noOfLines; p++)
                    {
                        for (j = 0; j < Ls[p]; j++, xy++)
                        {
                            int cp = Ps[xy];
                            y = (int) ((cp % YAxisBasis) * scaleY);
                            x = (int) (((cp - y) / YAxisBasis) * scaleX);
                            currentLine.Add(new Point(x, y));
                        }
                        totalLines.Add(currentLine.ToArray());
                        currentLine.Clear();
                    }
                    Invalidate();
                }
                catch {
                    totalLines.Clear();
                    currentLine.Clear();
                    throw new ArgumentException("Supplied string is not a valid signature");
                }
            }
        }

        public BorderStyle BorderStyle
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                _borderStyle = value;
                Invalidate();
            }
        }

		private void drawBorder(Graphics g)
		{
			if(_borderStyle == BorderStyle.None)
				return;
            
			using(Pen p = new Pen(BorderColor))
			{
                switch (_borderStyle)
				{
					case BorderStyle.FixedSingle:
						g.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
						break;

					case BorderStyle.Fixed3D:
						g.DrawRectangle(p, this.ClientRectangle);
						break;	
				}
			}
		}

        private bool haveGoneBad ; //= false;

        private void openLine(int x, int y)
        {
            if (!maxedOut)
            {
                _lastPoint = new Point(x, y);
                //previousPoint = lastPoint;
                Graphics g = this.CreateGraphics();
                g.DrawLine(_linePen, x, y, x, y);
                haveGoneBad = false;

                _lastX = x;
                _lastY = y;

                currentLine.Clear();
                currentLine.Add(_lastPoint);
                if (this.SignatureChanged != null) this.SignatureChanged(this, new EventArgs());
                if (_sourceSignature != null) _sourceSignature = null;
                if (_sourceArray.Count != 0) _sourceArray.Clear();
                this._timestamp = DateTime.UtcNow.Ticks;
            }
        }

        private void addPoint(int x, int y)
        {
            if (!maxedOut)
            {
                Graphics g = this.CreateGraphics();
                g.DrawLine(_linePen, _lastX, _lastY, x, y);

                if ((x != _lastPoint.X || y!= _previousPoint.Y)) // || x != previousPoint.X) && (y != lastPoint.Y || y!= previousPoint.Y)) 
                {
                    _nextPoint = new Point(x, y);
                    currentLine.Add(_nextPoint);
                    if (currentLine.Count == maxPointsPerLine)
                    {
                        totalLines.Add(currentLine.ToArray());
                        currentLine.Clear();
                        if (totalLines.Count == maxLines) maxedOut = true;
                        else openLine(x, y);
                    }
                    _lastPoint = _nextPoint;
                    //previousPoint = lastPoint;
                    //if (_sourceSignature != null) _sourceSignature = null;
                    //if (_sourceArray.Count != 0) _sourceArray.Clear();

                    if (this.SignatureChanged != null) this.SignatureChanged(this, new EventArgs());
                }
                _lastX = x;
                _lastY = y;

                this._timestamp = DateTime.UtcNow.Ticks;
            }
        }

        private void closeLine()
        {
            if (currentLine.Count != 0)
            {
                totalLines.Add(currentLine.ToArray());
                currentLine.Clear();
                if (totalLines.Count == maxLines) maxedOut = true;
                this._timestamp = DateTime.UtcNow.Ticks;
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
            if (maxedOut) return;
            _hasCapture = true;
            openLine(e.X, e.Y);
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
            if (maxedOut) return;
            if (e.Button == MouseButtons.None)
            {
                if (_hasCapture)
                {
                    closeLine();
                    _hasCapture = false;
                }
                return;
            }
			if(_hasCapture)
			{
                bool badPoint = false;
				int x = e.X;
				int y = e.Y;
                if (x > Width)
                {
                    badPoint = true;
                    x = Width - 1;
                }
                if (x < 0)
                {
                    badPoint = true;
                    x = 0;
                }
                if (y > Height)
                {
                    badPoint = true;
                    y = Height - 1;
                }
                if (y < 0)
                {
                    badPoint = true;
                    y = 0;
                }
                if (!badPoint)
                {
                    if (haveGoneBad)
                    {
                        openLine(_lastX, _lastY);
                        addPoint(x, y);
                    }
                    else
                    {
                        addPoint(x, y);
                    }
                }
                else // close the current line, but record where we are for when we come back into the control.
                {
                    if (!haveGoneBad)
                    {
                        addPoint(x, y);
                        //Graphics g = this.CreateGraphics();
                        //g.DrawLine(linePen, lastX, lastY, x, y);
                        haveGoneBad = true;
                        closeLine();
                    }
                    _lastX = x;
                    _lastY = y;
                }
                if (this.SignatureChanged != null) this.SignatureChanged(this, new EventArgs());
			}			
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
            addPoint(e.X, e.Y);
            closeLine();
            _hasCapture = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Refresh();
        }

		/// <summary>
		/// Clears the signature area.
		/// </summary>
		public void Clear()
		{
			currentLine.Clear();
			totalLines.Clear();
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            this.maxedOut = false;
            Invalidate();
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
            this.drawSignature(e.Graphics);
			this.drawBorder(e.Graphics);
    	}		
	
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				_linePen.Dispose();
				_linePen = new Pen(value);
			}
		}
	
        private object _tag;
        public new object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                if (_tag != value)
                {
                    _tag = value;
                }
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return new DateTime(_timestamp);
            }
        }
    }
}
