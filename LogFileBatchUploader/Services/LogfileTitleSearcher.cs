using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LogFileBatchUploader.Services
{
    internal class LogfileTitleSearcher
    {
        private readonly HttpClient _httpClient;

        public LogfileTitleSearcher()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5268") };
        }

        internal async Task<bool> DoesLogFileTitleExist(string logFileTitle)
        {
            var response = await _httpClient.GetAsync($"/api/LogFileTitles/search?titleParam={logFileTitle}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return bool.Parse(result);
            }
            return false;
        }
    }
}