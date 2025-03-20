using LogFileBatchUploader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogFileBatchUploader
{
    public class Program
    {
        static async Task Main(string[] args)
        {
       
            Console.WriteLine("Fetch log files from drive? [y/n]");
            string input = Console.ReadLine() ?? string.Empty;
            if (input == "y")
            {
                var logs = await LogFileFetcher.FetchRawLogFiles(); // Call the static method directly

                Console.WriteLine("Post to LogFileDb? [y/n]");
                input = Console.ReadLine() ?? string.Empty;
                if (input == "y")
                {
                    LogfileTitleSearcher logFileTitleSearcher = new LogfileTitleSearcher();
                    LogFileTitlePoster logFileTitlePoster = new LogFileTitlePoster();
                    LogFileTitleLogLineSearcher logFileTitleLogLineSearcher = new LogFileTitleLogLineSearcher();
                    LogFileTitleUpdater logFileTitleUpdater = new LogFileTitleUpdater();

                    foreach (var logFile in logs)
                    {
                        var logFileTitle = logFile.Key;
                        var logLines = logFile.Value;

                        var exists = await logFileTitleSearcher.DoesLogFileTitleExist(logFileTitle);

                        if (!exists)
                        {
                            Console.WriteLine("read");
                            Console.WriteLine(logFileTitle);
                            await logFileTitlePoster.PostLogFileTitle(logFileTitle);

                            int batchSize = 1000; // Define the batch size
                            int maxNumberOfLogFiles = logLines.Count; // Use Count property
                            for (int i = 0; i < maxNumberOfLogFiles; i += batchSize)
                            {
                                var batch = logLines.Skip(i).Take(batchSize).ToList();
                                await LogFilePoster.PostLogFileItemsAsync(batch);
                                await logFileTitleUpdater.UpdateLogfile(logFileTitle, i);
                                Console.WriteLine($"Posted logs {i + 1} to {i + batch.Count()} of {maxNumberOfLogFiles}"); // Use Count() method
                            }
                            await logFileTitleUpdater.UpdateLogfile(logFileTitle, maxNumberOfLogFiles);
                        }
                        else
                        {
                            int numOfRows = await logFileTitleLogLineSearcher.GetLogLineNumber(logFileTitle);
                            if (numOfRows != logLines.Count)
                            {
                                Console.WriteLine("Log file resuming");
                                int batchSize = 1000; // Define the batch size
                                int maxNumberOfLogFiles = logLines.Count; // Use Count property
                                for (int i = numOfRows; i < maxNumberOfLogFiles; i += batchSize)
                                {
                                    var batch = logLines.Skip(i).Take(batchSize).ToList();
                                    await LogFilePoster.PostLogFileItemsAsync(batch);
                                    await logFileTitleUpdater.UpdateLogfile(logFileTitle, i);
                                    Console.WriteLine($"Posted logs {i + 1} to {i + batch.Count()} of {maxNumberOfLogFiles}"); // Use Count() method
                                }
                                await logFileTitleUpdater.UpdateLogfile(logFileTitle, maxNumberOfLogFiles);
                            }
                            else
                            {
                                Console.WriteLine("skipped");
                                Console.WriteLine(logFileTitle);
                            }
                        }
                    }
                }
            }
        }
    }
}