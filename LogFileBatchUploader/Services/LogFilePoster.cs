using System.Text;
using LogFileBatchUploader.Models;
using Newtonsoft.Json;

namespace LogFileBatchUploader.Services
{
    public static class LogFilePoster
    {
        public static async Task PostLogFileItemsAsync(List<LogFileItem> logs)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5268/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(logs);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/LogFileItems", content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("LogFileItems posted successfully.");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
    }
}
