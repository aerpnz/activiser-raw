using System;
using System.Drawing;

namespace activiser.Library.Forms
{
    public static class GraphicsUtilities
    {
        public static Color NegateColor(Color value)
        {
            return Color.FromArgb(~value.ToArgb());
        }

        public static Bitmap NegateBitmap(Bitmap image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            Bitmap result = new Bitmap(image.Width, image.Height);

            int maxX = image.Width - 1;
            int maxY = image.Height - 1;

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    result.SetPixel(x, y, GraphicsUtilities.NegateColor(image.GetPixel(x, y)));
                }
            }
            return result;
        }

        public static Bitmap CopyBitmap(Bitmap image, Color[] inputColors, Color[] outputColors)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (inputColors == null && outputColors == null)
            {
                return new Bitmap(image); // why you might do this this way is beyond the author at the time of writing.... RCP 20/9/2006 
            }

            if (inputColors.Length != outputColors.Length)
            {
                throw new ArgumentException("Input and Output color maps must be the same length.");
            }

            Bitmap result = new Bitmap(image.Width, image.Height);
            int maxX = image.Width - 1;
            int maxY = image.Height - 1;
            int maxC = inputColors.Length - 1;
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    Color inputColor = image.GetPixel(x, y);
                    Color outputColor = inputColor;
                    for (int c = 0; c <= maxC; c++)
                    {
                        if (inputColor.Equals(inputColors[c]))
                        {
                            outputColor = outputColors[c];
                            break;
                        }
                    }
                    result.SetPixel(x, y, outputColor);
                }
            }
            return result;
        }

        public static void RecolorBitmap(Bitmap image, Color[] inputColors, Color[] outputColors)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (inputColors.Length != outputColors.Length)
            {
                throw new ArgumentException("ReColorArrayExceptionMessage");
            }
            //Dim result As New Bitmap(image.Width, image.Height) 

            int maxX = image.Width - 1;
            int maxY = image.Height - 1;
            int maxC = inputColors.Length - 1;

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    Color inputColor = image.GetPixel(x, y);
                    Color outputColor = inputColor;
                    for (int c = 0; c <= maxC; c++)
                    {
                        if (inputColor.Equals(inputColors[c]))
                        {
                            outputColor = outputColors[c];
                            break; // TODO: might not be correct. Was : Exit For 
                        }
                    }
                    if (!(inputColor == outputColor))
                    {
                        image.SetPixel(x, y, outputColor);
                    }
                }
            }
        }

        public static void RecolorBitmap(Bitmap image, Color inputColor, Color outputColor)
        {
            if (image == null)
        {
                throw new ArgumentNullException("image");
            }

            if (inputColor == outputColor)
            {
                return; // doh!
            }

            int maxX = image.Width - 1;
            int maxY = image.Height - 1;

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    if (!(image.GetPixel(x, y) == inputColor))
                    {
                        image.SetPixel(x, y, outputColor);
                    }
                }
            }
        }

        public static void StretchBitmap( Bitmap sourceImage,  Bitmap targetImage)
        {
            if (sourceImage == null)
            {
                throw new ArgumentNullException("sourceImage");
            }
            if (targetImage == null)
            {
                throw new ArgumentNullException("targetImage");
            }
            Graphics g = Graphics.FromImage(targetImage);
            Rectangle srcRect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            Rectangle destRect = new Rectangle(0, 0, targetImage.Width, targetImage.Height);
            g.DrawImage(sourceImage, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public static void StretchBitmap(Bitmap sourceImage, Bitmap targetImage, Color transparentColor, Color backgroundColor)
        {
            if (sourceImage == null)
        {
                throw new ArgumentNullException("sourceImage");
            }
            if (targetImage == null)
            {
                throw new ArgumentNullException("targetImage");
            }

            GraphicsUtilities.RecolorBitmap(sourceImage, transparentColor, backgroundColor);
            Graphics g = Graphics.FromImage(targetImage);
            Rectangle srcRect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            Rectangle destRect = new Rectangle(0, 0, targetImage.Width, targetImage.Height);
            g.DrawImage(sourceImage, destRect, srcRect, GraphicsUnit.Pixel);
            GraphicsUtilities.RecolorBitmap(targetImage, backgroundColor, transparentColor);
        }

        public static void CropBitmap(Bitmap sourceImage, Bitmap targetImage)
        {
            if (sourceImage == null)
            {
                throw new ArgumentNullException("sourceImage");
            }
            if (targetImage == null)
            {
                throw new ArgumentNullException("targetImage");
            }
            Graphics g = Graphics.FromImage(targetImage);
            Rectangle srcRect = new Rectangle(0, 0, Math.Min(sourceImage.Width, targetImage.Width), Math.Min(sourceImage.Height, targetImage.Height));
            g.DrawImage(sourceImage, srcRect, srcRect, GraphicsUnit.Pixel);
        }

        public static void CenterBitmap(Bitmap sourceImage, Bitmap targetImage)
        {
            if (sourceImage == null)
            {
                throw new ArgumentNullException("sourceImage");
            }
            if (targetImage == null)
            {
                throw new ArgumentNullException("targetImage");
            }

            int offsetX = 0 ;
            int offsetY = 0 ;
            int offsetXOut = 0;
            int offsetYOut = 0;
            int width = Math.Min(sourceImage.Width, targetImage.Width);
            int height = Math.Min(sourceImage.Height, targetImage.Height);

            if (sourceImage.Width > targetImage.Width)
            {
                offsetX = (sourceImage.Width - targetImage.Width) / 2;
            }
            else if (targetImage.Width > sourceImage.Width)
            {
                offsetXOut = (targetImage.Width - sourceImage.Width) / 2;
            }

            if (sourceImage.Height > targetImage.Height)
            {
                offsetY = (sourceImage.Height - targetImage.Height) / 2;
            }
            else if (targetImage.Height > sourceImage.Height)
            {
                offsetYOut = (targetImage.Height - sourceImage.Height) / 2;
            }

            Rectangle srcRect = new Rectangle(offsetX, offsetY, width, height);
            Rectangle dstRect = new Rectangle(offsetXOut, offsetYOut, width, height);

            Graphics g = Graphics.FromImage(targetImage);
            g.DrawImage(sourceImage, dstRect, srcRect, GraphicsUnit.Pixel);
        } 

        public static void AlignBitmap(Bitmap sourceImage, Bitmap targetImage, activiser.Library.Forms.ContentAlignment alignment, activiser.Library.Forms.Padding padding)
        {
            if (sourceImage == null)
            {
                throw new ArgumentNullException("sourceImage");
            }
            if (targetImage == null)
            {
                throw new ArgumentNullException("targetImage");
            }

            int width = Math.Min(sourceImage.Width, targetImage.Width);
            int height = Math.Min(sourceImage.Height, targetImage.Height);

            int leftIn = 0;
            int leftOut = padding.Left;
            int rightIn = 0;
            int rightOut = padding.Right;
            int centreIn = 0;
            int centreOut = 0;

            int topIn = 0;
            int topOut = padding.Top;
            int bottomIn = 0;
            int bottomOut = padding.Bottom;
            int middleIn = 0;
            int middleOut = 0;

            if (sourceImage.Width != targetImage.Width)
            {
                if (sourceImage.Width > targetImage.Width)
                {
                    rightIn = (sourceImage.Width - targetImage.Width);
                    centreIn = rightIn / 2;
                }
                else // (targetImage.Width > sourceImage.Width)
                {
                    rightOut = (targetImage.Width - sourceImage.Width);
                    centreOut = rightOut / 2;
                    rightOut -= padding.Right;
                }
            }

            if (sourceImage.Height != targetImage.Height)
            {
                if (sourceImage.Height > targetImage.Height)
                {
                    bottomIn = sourceImage.Height - targetImage.Height;
                    middleIn = bottomIn / 2;
                }
                else // (targetImage.Height > sourceImage.Height)
                {
                    bottomOut = (targetImage.Height - sourceImage.Height);
                    middleOut = bottomOut / 2;
                    bottomOut += padding.Bottom;
                }
            }

            // image is centered, now move as necessary !

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    leftIn = 0;
                    leftOut = padding.Left;
                    topIn = 0;
                    topOut = 2;
                    break;
                case ContentAlignment.TopCenter:
                    leftOut = centreOut;
                    leftIn = centreIn;
                    topIn = 0;
                    topOut = padding.Top;
                    break;
                case ContentAlignment.TopRight:
                    leftOut = rightOut;
                    leftIn = rightIn;
                    topIn = 0;
                    topOut = padding.Top;
                    break;
                case ContentAlignment.MiddleLeft:
                    leftIn = 0;
                    leftOut = padding.Left;
                    topIn = middleIn;
                    topOut = middleOut;
                    break;
                case ContentAlignment.MiddleCenter:
                    leftOut = centreOut;
                    leftIn = centreIn;
                    topIn = middleIn;
                    topOut = middleOut;
                    break;
                case ContentAlignment.MiddleRight:
                    leftOut = rightOut;
                    leftIn = rightIn;
                    topIn = middleIn;
                    topOut = middleOut;
                    break;
                case ContentAlignment.BottomLeft:
                    leftIn = 0;
                    leftOut = padding.Left;
                    topIn = bottomIn;
                    topOut = bottomOut;
                    break;
                case ContentAlignment.BottomCenter:
                    leftOut = centreOut;
                    leftIn = centreIn;
                    topIn = bottomIn;
                    topOut = bottomOut;
                    break;
                case ContentAlignment.BottomRight:
                    leftOut = rightOut;
                    leftIn = rightIn;
                    topIn = bottomIn;
                    topOut = bottomOut ;
                    break;
            }

            Rectangle srcRect = new Rectangle(leftIn, topIn, width, height);
            Rectangle dstRect = new Rectangle(leftOut, topOut, width, height);

            Graphics g = Graphics.FromImage(targetImage);
            g.DrawImage(sourceImage, dstRect, srcRect, GraphicsUnit.Pixel);
        } 
    }
}
