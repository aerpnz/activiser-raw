using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.ComponentModel;

namespace activiser.library
{
    public class DecodeEncryptedSignature
    {
        public static Image LoadSignature(byte[] b)
        {
            Color borderColor = Color.Black;
            Graphics graphics = null;
            Pen linePen = new Pen(Color.Black);
            Bitmap bmp;
            ArrayList currentLine = new ArrayList();
            ArrayList totalLines = new ArrayList();
            bool done = false;
            int pointCount = 0;
            Point previousPoint, currentPoint;

            bmp = new Bitmap(224, 72);
            graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 224, 72));
            graphics.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Black), new Rectangle(0, 0, 224 - 1, 72 - 1));

            MemoryStream ms = new MemoryStream(b);
            BinaryReader reader = new BinaryReader(ms);

            int width = reader.ReadInt32();
            int height = reader.ReadInt32();

            totalLines.Clear();
            while (!done)
            {
                currentLine.Clear();
                try
                {
                    pointCount = reader.ReadInt32();
                    previousPoint = new Point(reader.ReadInt32(), reader.ReadInt32());
                    currentLine.Add(previousPoint);
                    for (int x = 1; x < pointCount; x++)
                    {
                        currentPoint = new Point(reader.ReadInt32(), reader.ReadInt32());
                        currentLine.Add(currentPoint);
                        previousPoint = currentPoint;
                    }
                }
                catch
                {
                    if (currentLine.Count > 0)
                        totalLines.Add(currentLine.Clone());
                    break;
                }
                totalLines.Add(currentLine.Clone());
            }

            foreach (ArrayList line in totalLines)
            {
                if (line.Count == 0)
                    continue;

                previousPoint = (Point)line[0];
                for (int x = 1; x < line.Count; x++)
                {
                    currentPoint = (Point)line[x];
                    graphics.DrawLine(linePen, previousPoint.X, previousPoint.Y, currentPoint.X, currentPoint.Y);
                    previousPoint = currentPoint;
                }
            }

            graphics.DrawRectangle(linePen, 0, 0, 224 - 1, 72 - 1);
            return bmp;
        }
    }
}