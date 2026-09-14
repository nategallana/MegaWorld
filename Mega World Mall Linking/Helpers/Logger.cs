using System;
using System.IO;
using System.Windows.Forms;

namespace Mega_World_Mall_Linking.Helpers
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static string _logDirectory;

        private static string GetLogDirectory()
        {
            if (string.IsNullOrEmpty(_logDirectory))
            {
                try
                {
                    string asmLoc = typeof(Logger).Assembly.Location;
                    if (!string.IsNullOrEmpty(asmLoc))
                    {
                        _logDirectory = Path.Combine(Path.GetDirectoryName(asmLoc), "Logs");
                    }
                }
                catch { }

                if (string.IsNullOrEmpty(_logDirectory))
                {
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    if (string.IsNullOrEmpty(baseDir))
                    {
                        baseDir = Application.StartupPath;
                    }
                    _logDirectory = Path.Combine(baseDir, "Logs");
                }
            }
            return _logDirectory;
        }

        static Logger()
        {
            try
            {
                string dir = GetLogDirectory();
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch { }
        }

        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            string fullMessage = message;
            if (ex != null)
            {
                fullMessage += $" | Exception: {ex.Message} | StackTrace: {ex.StackTrace}";
            }
            WriteLog("ERROR", fullMessage);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                lock (_lock)
                {
                    string dir = GetLogDirectory();
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string logFile = Path.Combine(dir, $"{DateTime.Now:yyyy-MM-dd}.log");
                    string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

                    File.AppendAllText(logFile, logLine + Environment.NewLine);
                }
            }
            catch
            {
                // Silently avoid crashing if disk write fails
            }
        }
    }
}
