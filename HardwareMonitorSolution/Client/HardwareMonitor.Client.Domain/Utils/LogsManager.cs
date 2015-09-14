using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HardwareMonitor.Client.Domain.Utils
{
    public static class LogsManager
    {
        [Flags]
        public enum LogLevel
        {
            None = 0,
            Verbose = 1 << 0,
            Debug =   1 << 1,
            Warning = 1 << 2,
            Error =   1 << 3,
            Temp =    1 << 4
        };

        public abstract class LogObserver
        {
            public LogLevel LogLevelsObserved { get; private set; } = LogLevel.None;

            public abstract void OnLog(string message, LogLevel level);

            protected void Subscribe() => _observers.AddLast(this);

            protected void Unsubscribe() => _observers.Remove(this);
        }

        private const string _SEPARATOR = "--------------------------------";
        private static readonly string _LOGS_FOLDER = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\HardwareMonitor Logs\\";
        private static readonly string _TEMP_LOGS_FOLDER = $"{_LOGS_FOLDER}\\_TEMP_LOGS_FOLDER\\";

        private static LinkedList<LogObserver> _observers = new LinkedList<LogObserver>();

        public static void Log(object message, LogLevel level = LogLevel.Debug)
        {
            Log(new object[] { message }, level);
        }

        public static void Log(object[] messages, LogLevel level = LogLevel.Debug)
        {
            if (!Directory.Exists(_LOGS_FOLDER)) Directory.CreateDirectory(_LOGS_FOLDER);

            StringBuilder sb = new StringBuilder();

            foreach (var message in messages)
                sb.AppendLine(message.ToString());

            var buildedMessage = sb.ToString();

            foreach (var path in LogFilesPath(level))
                File.AppendAllText(path, $"{DateTime.UtcNow}\n{buildedMessage}\n{_SEPARATOR}\n");

            try
            {
                lock (_observers)
                {
                    foreach (var observer in _observers)
                    {
                        lock (observer)
                        {
                            if (observer.LogLevelsObserved.HasFlag(level)) observer.OnLog(buildedMessage, level);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(LogFilesPath(LogLevel.Error)[0], $"Log to observers (level {level}): {ex}");
            }
        }

        public static string GetTempLogsFolderPath()
        {
            if (!Directory.Exists(_TEMP_LOGS_FOLDER)) Directory.CreateDirectory(_TEMP_LOGS_FOLDER);
            return _TEMP_LOGS_FOLDER;
        }

        public static string[] LogFilesPath(LogLevel lv = LogLevel.Debug)
        {
            var levels = lv.ToString().ToLower().Split(',');
            var paths = new string[levels.Length];

            for (int i = 0; i < levels.Length; i++)
                paths[i] = $"{_LOGS_FOLDER}{levels[i].Trim()}.log";

            return paths;
        }
    }
}
