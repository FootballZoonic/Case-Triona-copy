using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LogFileBatchUploader.Models;
using Newtonsoft.Json;

namespace LogFileBatchUploader.Services
{
    internal class LogFileTitlePoster
    {
        private readonly HttpClient _httpClient;

        public LogFileTitlePoster()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5268") };
        }

        internal async Task PostLogFileTitle(string logFile)
        {
            var logFileTitle = new LogFileTitle
            {
                Title = logFile,
                NumOfRows = 0
            };

            var jsonContent = JsonConvert.SerializeObject(logFileTitle);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/LogFileTitles", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Log file title posted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to post log file title. Status code: {response.StatusCode}");
            }
        }
    }
}