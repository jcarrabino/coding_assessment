using System;
using System.IO;

public static class ErrorLogger
{
    private static readonly string ErrorLogFilePath = "/log/error_log.txt";
    private static readonly object ErrorLogLock = new object();

    public static void LogError(string message)
    {
        lock (ErrorLogLock)
        {
            using (StreamWriter writer = new StreamWriter(ErrorLogFilePath, true))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                writer.WriteLine($"{timestamp} - {message}");
            }
        }
    }

    public static void LogException(Exception ex)
    {
        LogError($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
    }
}