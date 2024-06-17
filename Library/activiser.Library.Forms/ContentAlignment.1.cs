using System.ComponentModel;
using System;

namespace activiser.Library.Forms
{
    //[Flags()]   
    public enum ContentAlignment // values copied from full framework System.Drawing
    {
        None = 0,
        TopLeft = 1,        //Content is vertically aligned at the top, and horizontally aligned on the left. 
        TopCenter = 2,      //Content is vertically aligned at the top, and horizontally aligned at the center. 
        TopRight = 4,        //Content is vertically aligned at the top, and horizontally aligned on the right. 
        Top = TopLeft | TopCenter | TopRight,
        MiddleLeft = 0x10,     //Content is vertically aligned in the middle, and horizontally aligned on the left. 
        MiddleCenter = 0x20,   //Content is vertically aligned in the middle, and horizontally aligned at the center. 
        MiddleRight = 0x40,    //Content is vertically aligned in the middle, and horizontally aligned on the right. 
        Middle = MiddleLeft | MiddleCenter | MiddleRight,
        BottomLeft = 0x100,     //Content is vertically aligned at the bottom, and horizontally aligned on the left. 
        BottomCenter = 0x200,   //Content is vertically aligned at the bottom, and horizontally aligned at the center. 
        BottomRight = 0x400,    //Content is vertically aligned at the bottom, and horizontally aligned on the right. 
        Bottom = BottomLeft | BottomCenter | BottomRight,

        Left = TopLeft | MiddleLeft | BottomLeft,
        Center = TopCenter | MiddleCenter | BottomCenter,
        Right = TopRight | MiddleRight | BottomRight,

        Fill = 0x777,
    }
}