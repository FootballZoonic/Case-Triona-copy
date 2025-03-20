using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LogFileBatchUploader.Services
{
    internal class LogFileTitleUpdater
    {
        private readonly HttpClient _httpClient;

        public LogFileTitleUpdater()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5268") }; // Adjust the base address as needed
        }

        internal async Task UpdateLogfile(string logFileTitle, int readLines)
        {
            var updateData = new { Title = logFileTitle, NumOfRows = readLines };
            var response = await _httpClient.PutAsJsonAsync("/api/LogFileTitles", updateData);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update log file title.");
            }
        }
    }
}