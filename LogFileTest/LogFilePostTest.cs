using System.Net;
using System.Text;
using LogFileBatchUploader.Models;
using Newtonsoft.Json;

namespace LogFilePostTests
{
    public class CustomHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("LogFileItem posted successfully.")
            });
        }
    }

    [TestClass]
    public class LogFileItemPostTests
    {
        [TestMethod]
        public async Task PostLogFileItemAsync_ShouldPostLogFileItemSuccessfully()
        {
            // Arrange
            var logFileItem = new LogFileItem
            {
                Timestamp = DateTime.Now,
                Case = "Case1",
                UserName = "User1",
                Type = "Type1",
                Endpoint = "Endpoint1"
            };

            var handler = new CustomHttpMessageHandler();
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:5268/")
            };

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                await PostLogFileItemAsync(logFileItem, httpClient);

                // Assert
                Assert.AreEqual("LogFileItem posted successfully.", sw.ToString().Trim());
            }
        }

        private static async Task PostLogFileItemAsync(LogFileItem log, HttpClient client)
        {
            var json = JsonConvert.SerializeObject(log);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/LogFileItems", content);
            Console.WriteLine(response.IsSuccessStatusCode ? "LogFileItem posted successfully." : $"Error: {response.StatusCode}");
        }
    }
}