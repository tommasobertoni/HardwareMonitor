using System;
using System.IO;

namespace HardwareMonitor.Client.Domain.Utils
{
    public static class LogsManager
    {
        private const string _SEPARATOR = "--------------------------------";
        private static readonly string _LOGS_FOLDER = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}HardwareMonitor Logs\\";

        public enum LogLevel
        {
            VERBOSE, DEBUG, WARNING, ERROR
        };

        static LogsManager()
        {
            if (!Directory.Exists(_LOGS_FOLDER))
                Directory.CreateDirectory(_LOGS_FOLDER);
        }

        public static void Log(string message, LogLevel level = LogLevel.DEBUG)
        {
            File.AppendAllText(
                LogFilePath(level),
                $"{DateTime.UtcNow}\n{message}\n{_SEPARATOR}\n");
        }

        public static string LogFilePath(LogLevel level = LogLevel.DEBUG)
        {
            return $"{_LOGS_FOLDER}{level.ToString().ToLower()}.log";
        }
    }
}
