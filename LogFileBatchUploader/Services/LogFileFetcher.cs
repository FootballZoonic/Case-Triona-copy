using LogFileBatchUploader.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LogFileBatchUploader.Services
{
    public static class LogFileFetcher
    {
        public static async Task<Dictionary<string, List<LogFileItem>>> FetchRawLogFiles()
        {
            var logs = new Dictionary<string, List<LogFileItem>>();
            var logDirectory = @"..\Logs";

            var logFiles = Directory.GetFiles(logDirectory, "*.log");

            foreach (var logFile in logFiles)
            {
                var logFileTitle = Path.GetFileName(logFile);
                var logLines = new List<LogFileItem>();

                using (StreamReader reader = new StreamReader(logFile))
                {
                    string logLine;
                    while ((logLine = reader.ReadLine() ?? string.Empty) != string.Empty)
                    {
                        var parsedLine = LineParser.ParseLine(logLine);

                        if (parsedLine != null && LogFileItemFilter.Include(parsedLine))
                        {
                            logLines.Add(parsedLine);
                        }
                    }
                }

                logs[logFileTitle] = logLines;
            }
            return logs;
        }
    }
}