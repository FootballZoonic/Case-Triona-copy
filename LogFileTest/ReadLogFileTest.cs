using LogFileBatchUploader.Models;

namespace ReadLogFileTest
{
    [TestClass]
    public class LogFileRetrievalTests
    {
        [TestMethod]
        public void GetLogFiles_ShouldReturnLogFiles_FromSpecifiedDirectory()
        {
            var testDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestLogs");
            Directory.CreateDirectory(testDirectory);

            var logFile1 = Path.Combine(testDirectory, "CLD_API_prod.20241001.log");
            var logFile2 = Path.Combine(testDirectory, "CLD_API_prod.20241002.log");
            File.WriteAllText(logFile1, "Sample log content 1");
            File.WriteAllText(logFile2, "Sample log content 2");

            var logs = new List<LogFileItem>();
            var logDirectory = testDirectory;

            var logFiles = Directory.GetFiles(logDirectory, "*.log");

            Assert.AreEqual(2, logFiles.Length);
            Assert.IsTrue(logFiles.Contains(logFile1));
            Assert.IsTrue(logFiles.Contains(logFile2));

            Directory.Delete(testDirectory, true);
        }
    }
}
