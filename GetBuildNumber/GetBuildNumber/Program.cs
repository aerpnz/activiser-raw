using System;
using System.Collections.Generic;
using System.Text;

namespace GetBuildNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            string format = args[0];
            DateTime now = DateTime.Now;
            DateTime since;
            if (DateTime.TryParse(format, out since))
            {
                int newVersionNumberInt = (int)new TimeSpan(now.Ticks - since.Ticks).TotalDays;
                string newVersionNumber = newVersionNumberInt.ToString();
                // Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber);
                Console.WriteLine (newVersionNumber);
            }
            else
            {
                throw new ArgumentException("Format string does not specify a valid date");
            }
        }
    }
}
