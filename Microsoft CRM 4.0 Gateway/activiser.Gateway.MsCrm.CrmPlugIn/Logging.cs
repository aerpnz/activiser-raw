using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Globalization;

namespace activiser.InputGateway.CrmPlugIn
{
    sealed class Logging
    {
        #region Logging
        private Logging() { }

        static private void LogMessage(ErrorCode eventId, string message, EventLogEntryType entryType, object[] args)
        {
            if (args != null) message = string.Format(CultureInfo.InvariantCulture, message, args);
            EventLog errorLogger = new EventLog(string.Empty, ".", Properties.Resources.EventLogSource);
            if (errorLogger != null)
            {
                errorLogger.WriteEntry(message, entryType, (int)eventId, 0);
            }
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
