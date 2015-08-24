using System;
using static System.IO.File;

namespace HardwareMonitor.Client.Controller.Utils
{
    static class LogsManager
    {
        private const string _SEPARATOR = "--------------------------------";

        public enum LogLevel
        {
            VERBOSE, DEBUG, WARNING, ERROR
        };

        public static void Log(string message, LogLevel level = LogLevel.DEBUG)
        {
            AppendAllText(
                $"{AppDomain.CurrentDomain.BaseDirectory}{level.ToString().ToLower()}.log",
                $"{DateTime.UtcNow}\n{message}\n{_SEPARATOR}\n");
        }
    }
}
