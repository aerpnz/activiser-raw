using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using activiser.WebService.Gateway.Crm.DataAccessLayer.EventLogTableAdapters;

namespace activiser.WebService.CrmOutputGateway
{
    public partial class Gateway
    {
        private static RegistryKey _Key = Registry.LocalMachine.OpenSubKey("Software\\activiser\\Microsoft CRM 4.0", false);

        public static string FullName
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().FullName;
            }
        }

        public enum LoggingLevels
        {
            None = 0,
            Error = 1,
            Warning = 2,
            Failure = 3,
            Success = 4,
            Informational = 5,
            Debug = 9
        }

        public const LoggingLevels DefaultLoggingLevel = LoggingLevels.Error | LoggingLevels.Warning;

        public static LoggingLevels DebugLevel
        {
            get
            {
                return (LoggingLevels)(GetRegistryInt("Debug Level", (int)DefaultLoggingLevel));
            }
        }


        private static string GetRegistryString(string key, string defaultValue)
        {
            try
            {
                return (string)_Key.GetValue(key, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static int GetRegistryInt(string key, int defaultValue)
        {
            try
            {
                return (int)_Key.GetValue(key, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }
        protected void logEvent(string message, string xml)
        {
            logEvent(message, xml, EventLogEntryType.Information, 0);
        }

        protected void logEvent(string message, string xml, EventLogEntryType entryType, EventId eventId)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection elc = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
                {
                    using (EventLogTableAdapter elta = new EventLogTableAdapter())
                    {
                        elta.Connection = elc;
                        elta.Insert(Guid.NewGuid(), DateTime.UtcNow, message, xml);
                    }
                }
            }
            catch
            {
                // TODO: some thing here.
                Debug.WriteLine("error logging to event table for message: " + message);
            }
            try
            {
                EventLog errorLogger = new EventLog(string.Empty, ".", Properties.Resources.EventLogSource);
                if (errorLogger != null)
                {
                    errorLogger.WriteEntry(message, entryType, (int)eventId, 0);
                }
            }
            catch
            {
                // TODO: some thing here.
                Debug.WriteLine("error logging to event log for message: " + message);
            }

        }

        protected void LogErrorMessage(EventId eventId, string message, params object[] args)
        {
            LogErrorMessage(eventId, string.Format(message, args));
        }

        protected void LogErrorMessage(EventId eventId, string message, Exception ex, params object[] args)
        {
            LogErrorMessage(eventId, string.Format(message, args), ex);
        }

        protected void LogErrorMessage(EventId eventId, string message)
        {
            logEvent(message, string.Empty, EventLogEntryType.Error, eventId);
        }

        protected void LogErrorMessage(EventId eventId, string message, Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    try
                    {
                        ExceptionParser eventParser = new ExceptionParser(ex);
                        message += "\n\nException details: \n" + eventParser.ToString();
                    }
                    catch (Exception ex2)
                    {
                        message += "\nInternal error in exception parser\n" + ex2.StackTrace;
                    }
                }
                logEvent(message, string.Empty, EventLogEntryType.Error, eventId);
                // logEvent(message, string.Empty); // TODO : Serialize exception data.
            }
            catch
            {
            }
        }

        protected void LogWarningMessage(EventId eventId, string message, params object[] args)
        {
            LogWarningMessage(eventId, string.Format(message, args));
        }

        protected void LogWarningMessage(EventId eventId, string message)
        {
            if (DebugLevel >= LoggingLevels.Warning)
            {
                logEvent(message, string.Empty, EventLogEntryType.Warning, eventId); // TODO : Serialize exception data.
            }
        }

        protected void LogInfoMessage(EventId eventId, string message, params object[] args)
        {
            LogInfoMessage(eventId, string.Format(message, args));
        }

        protected void LogInfoMessage(EventId eventId, string message)
        {
            if (DebugLevel >= LoggingLevels.Informational)
            {
                logEvent(message, string.Empty);
            }
        }

        protected void LogSuccess(EventId eventId, string message, params object[] args)
        {
            LogSuccess(eventId, string.Format(message, args));
        }

        protected void LogSuccess(EventId eventId, string message)
        {
            if (DebugLevel >= LoggingLevels.Debug)
            {
                logEvent(message, string.Empty, EventLogEntryType.SuccessAudit, eventId);
            }
        }

        protected void LogFailure(EventId eventId, string message, params object[] args)
        {
            LogFailure(eventId, string.Format(message, args));
        }

        protected void LogFailure(EventId eventId, string message)
        {
            if (DebugLevel >= LoggingLevels.Debug)
            {
                logEvent(message, string.Empty, EventLogEntryType.FailureAudit, eventId);
            }
        }
    }
}
