using System;
using System.IO;
using System.Threading.Tasks;

namespace GolddiggerGmbh.Model
{
    public static class Logger
    {
        //private static readonly object _lock = new object();
        private static readonly string LogFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        private static string GetLogFilePath()
        {
            var fileName = $"log_{DateTime.Now:yyyy-MM-dd}.txt";
            return Path.Combine(LogFolderPath, fileName);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                if (!Directory.Exists(LogFolderPath))
                {
                    Directory.CreateDirectory(LogFolderPath);
                }

                string logEntry = $"[{level}] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";

                
                File.AppendAllText(GetLogFilePath(), logEntry);
            }
            catch
            {
                
            }
        }

        public static void LogInfo(string message) => WriteLog("INFO", message);
        public static void LogWarning(string message) => WriteLog("WARN", message);
        public static void LogError(string message) => WriteLog("ERROR", message);


        public static void LogException(Exception ex)
        {
            var msg = $"{ex.Message}{Environment.NewLine}{ex.StackTrace}";
            WriteLog("EXCEPTION", msg);
        }
    }
}
