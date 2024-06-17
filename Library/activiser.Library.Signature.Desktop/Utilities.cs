using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace activiser.Library
{
    public static class Utilities
    {
        public static DateTime FixTime(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, value.Kind);
        }

        public static string GetShortTimeFormat(string LongFormat)
        {
            int i = LongFormat.LastIndexOf(':');
            if ((i == -1))
            {
                i = LongFormat.IndexOf('s');
            }
            if ((i == -1))
            {
                return LongFormat;
            }
            string result = LongFormat.Substring(0, i);
            if (LongFormat.IndexOf('t') != -1)
            {
                string remainder = LongFormat.Substring(i);
                int j = remainder.LastIndexOfAny("sS.fF".ToCharArray());
                if (((j != -1)
                            && (j < remainder.Length)))
                {
                    string tail = remainder.Substring((j + 1));
                    result += tail;
                }
            }
            return result;
        }

        public static void LocateDropDown(Control target, Control anchor, bool preferLeft)
        {
            Rectangle myScreen = Screen.FromControl(anchor).WorkingArea;
            Rectangle anchorRect = anchor.ClientRectangle;
            Rectangle targetRect = anchor.RectangleToScreen(anchorRect);
            Point newLocation;
            if (preferLeft)
            {
                //  try anchoring to bottom left
                newLocation = new Point(targetRect.Left, targetRect.Bottom);
            }
            else
            {
                // try anchoring to bottom right
                newLocation = new Point((targetRect.Right - target.Width), targetRect.Bottom);
            }
            //  if lost off the bottom, drop it up instead of down!
            if ((newLocation.Y + target.Height) > myScreen.Bottom)
            {
                if (preferLeft)
                {
                    newLocation = new Point(targetRect.Left, (targetRect.Top - target.Height));
                }
                else
                {
                    newLocation = new Point((targetRect.Right - target.Width), (targetRect.Top - target.Height));
                }
            }
            //  if off the edge of the screen, move it over
            if ((newLocation.X + target.Width ) > myScreen.Right)
            {
                newLocation = new Point((myScreen.Right - target.Width), newLocation.Y);
            }
            //  if off the edge of the screen, move it over
            if ((newLocation.X < myScreen.Left))
            {
                newLocation = new Point(myScreen.X, newLocation.Y);
            }
            // Debug.WriteLine(String.Format("relocating '{0}' to {1}", target.Name, newLocation.ToString))
            target.SuspendLayout();
            target.Location = newLocation;
            target.ResumeLayout();
        }
    }
}
