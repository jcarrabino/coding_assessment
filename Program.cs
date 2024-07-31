using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static readonly string filePath = "/log/out.txt";
    private static readonly object fileLock = new object();
    private static int lineCount = 0;

    static async Task Main()
    {
        try
        {
            // Initialize the file with the first line
            InitializeLogFile(filePath);

            // Create 10 tasks to write to the file
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Task.Run(() => LogToFile());
            }

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Wait for a character press before exiting
            Console.WriteLine("All threads have completed. Press any key to exit.");
            Console.Read();
        }
        catch (Exception ex)
        {
            ErrorLogger.LogException(ex);
        }

    }

    private static async Task LogToFile()
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                string currentTime = DateTime.Now.ToString("HH:mm:ss.fff");
                int threadId = Thread.CurrentThread.ManagedThreadId;
                lock (fileLock)
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine($"{lineCount}, {threadId}, {currentTime}");
                        lineCount++;
                    }
                }
                await Task.Yield();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.LogException(ex);
        }
    }

    private static void InitializeLogFile(string filePath)
    {
        try
        {
            // Ensure the directory exists, creates it if not
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            lock (fileLock)
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    string currentTime = DateTime.Now.ToString("HH:mm:ss.fff");
                    writer.WriteLine($"0, 0, {currentTime}");
                    lineCount++;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.LogException(ex);
        }
    }

}
