using LogFileBatchUploader.Models;
using LogFileBatchUploader.Services;

namespace LogFileTest
{
    [TestClass]
    public sealed class LogFileItemTestFilter
    {
        [TestMethod]
        public void LogFileItemInvalidUser()
        {
            // Arrange

            var logFileBackgroundJob = new LogFileItem
            {
                Timestamp = DateTime.Parse("2021-01-01"),
                Case = "Case1",
                UserName = "BKG_JOB",
                Type = "CALL",
                Endpoint = "/endpoint"
            };

            var logFileIntegration = new LogFileItem
            {
                Timestamp = DateTime.Parse("2021-01-01"),
                Case = "Case1",
                UserName = "User1",
                Type = "CALL",
                Endpoint = "api/integration/loaduser-info"
            };

            var logFileAnonymous = new LogFileItem
            {
                Timestamp = DateTime.Parse("2021-01-01"),
                Case = "Case2",
                UserName = "anonymous",
                Type = "RES",
                Endpoint = "api/loading-slots/calendars"
            };

            // Act
            var resultBackground = LogFileItemFilter.Include(logFileBackgroundJob);
            var resultIntegration = LogFileItemFilter.Include(logFileIntegration);
            var resultAnonymous = LogFileItemFilter.Include(logFileAnonymous);


            // Assert
            Assert.IsFalse(resultBackground);
            Assert.IsFalse(resultIntegration);
            Assert.IsFalse(resultAnonymous);
        }
    }
}