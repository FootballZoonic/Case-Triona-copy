using System.Net.Http;
using System.Threading.Tasks;

namespace LogFileBatchUploader.Services
{
    internal class LogFileTitleLogLineSearcher
    {
        private readonly HttpClient _httpClient;

        public LogFileTitleLogLineSearcher()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5268") };
        }

        internal async Task<int> GetLogLineNumber(string logFileTitle)
        {
            var response = await _httpClient.GetAsync($"/api/LogFileTitles/rows?titleParam={logFileTitle}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            int numOfRows = int.Parse(responseContent);

            return numOfRows;
        }
    }
}