using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace TestWinForm
{
    internal class EventLogger
    {
        private const string LogName = "Console";
        private const string SourceName = "MainForm";


        public static void LogInformation(string message)
        {
            LogEntry(message, EventLogEntryType.Information);
        }

        public static void LogWarning(string message)
        {
            LogEntry(message, EventLogEntryType.Warning);
        }

        public static void LogError(string message, Exception exception)
        {
            LogEntry($"{message}\n\nException Details:\n{exception}", EventLogEntryType.Error);
        }

        public static void LogEntry(string message, EventLogEntryType entryType)
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }

            EventLog eventLog = new EventLog(LogName);
            eventLog.Source = SourceName;

            string formattedMessage = $"[{entryType}] {DateTime.Now:yyyy-MMM-dd HH:mm:ss}: {message}";

            eventLog.WriteEntry(formattedMessage, entryType);

            eventLog.Close();
        }
    }
}