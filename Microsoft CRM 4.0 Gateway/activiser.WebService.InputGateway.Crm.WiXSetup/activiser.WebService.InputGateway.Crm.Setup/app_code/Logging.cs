using System;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace activiser.WebService
{
    sealed class Logging
    {
        private Logging() { }
        #region Logging

        static private void LogMessage(ErrorCode eventId, string message, EventLogEntryType entryType, object[] args)
        {
            if (args != null) message = string.Format(CultureInfo.InvariantCulture, message, args);

            using (StreamWriter log = new StreamWriter(Path.Combine(Path.GetTempPath(),
                   string.Format(CultureInfo.InvariantCulture, "ActiviserInputGatewayCrm-{0:yyyy-MM-dd}.Log", DateTime.Today)), true))
            {
                log.WriteLine(message);
            }
            //EventLog errorLogger = new System.Diagnostics.EventLog("Application", Environment.MachineName, "Activiser CRM Input Gateway");
            //if (errorLogger != null)
            //{
            //    errorLogger.WriteEntry(message, entryType, (int) eventId, 0);
            //}
        }

        static public void LogMessage(ErrorCode eventId, string message)
        {
            LogMessage(eventId, message, EventLogEntryType.Information, null);
        }

        static public void LogMessage(ErrorCode eventId, string format, params object[] args)
        {
            LogMessage(eventId, format, EventLogEntryType.Information, args);
        }

        static public void LogWarning(ErrorCode eventId, string message)
        {
            LogMessage(eventId, message, EventLogEntryType.Warning, null);
        }

        static public void LogWarning(ErrorCode eventId, string format, params object[] args)
        {
            LogMessage(eventId, format, EventLogEntryType.Warning, args);
        }

        static public void LogError(ErrorCode eventId, string message)
        {
            LogMessage(eventId, message, EventLogEntryType.Error, null);
        }

        static public void LogError(ErrorCode eventId, string format, params object[] args)
        {
            LogMessage(eventId, format, EventLogEntryType.Error, args);
        }

        #endregion
    }
}
