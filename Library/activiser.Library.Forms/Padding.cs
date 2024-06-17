using System;

using System.Collections.Generic;
using System.Text;


namespace activiser.Library.Forms
{
    [Serializable()]
    public struct Padding
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        //public Padding()
        //{
        //    Left = 0;
        //    Top = 0;
        //    Right = 0;
        //    Bottom = 0;
        //}

        public Padding(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Padding(int padding)
        {
            Left = padding;
            Top = padding;
            Right = padding;
            Bottom = padding;
        }

        public Padding(string padding)
        {
            string[] parts = padding.Split(',');
            if (parts.Length != 4)
            {
                throw new FormatException();
            }

            Left = int.Parse(parts[0]);
            Top = int.Parse(parts[1]);
            Right = int.Parse(parts[2]);
            Bottom = int.Parse(parts[3]);
        }

        public int All
        {
            get {
                if ((Left == Right) && (Left == Top) && (Left == Bottom)) return Left;
                return -1;
            }
            set {
                Left = value;
                Top = value;
                Right = value;
                Bottom = value;
            }
        }

        public  int Horizontal
        {
            get { return Left + Right; }
        }

        public  int Vertical
        {
            get { return Top + Bottom; }
        }

        public  System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.Horizontal, this.Vertical);
            }
        }

        private static readonly Padding _EmptyPadding= new Padding(0, 0, 0, 0);
        public static  Padding Empty
        {
            get
            {
                return _EmptyPadding;
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Left, Top, Right, Bottom); 
        }

        public static explicit operator Padding(int padding)
        {
            return new Padding(padding);
        }

        public static explicit operator string(Padding padding)
        {
            return padding.ToString();
        }

        public static explicit operator Padding(string padding)
        {
            if (padding.IndexOf(',') != -1)
            {
                string[] parts = padding.Split(',');
                if (parts.Length != 4)
                {
                    throw new FormatException();
                }
                return new Padding(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
            }
            else
            {
                int p = int.Parse(padding, null);
                return new Padding(p);                
            }
        }
    }
}
