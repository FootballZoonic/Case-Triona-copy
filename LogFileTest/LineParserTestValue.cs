using LogFileBatchUploader.Models;
using LogFileBatchUploader.Services;

namespace LineParserTestValue

{
    [TestClass]
    public class LineParserTestValue
    {
        [TestMethod]
        public void ParseLine_ShouldReturnLogFileItem_WithCorrectValues()
        {
            string line = "2021-01-01|Case1|User1|Type1|Endpoint1|ExtraPart";
            var expected = new LogFileItem
            {
                Timestamp = DateTime.Parse("2021-01-01"),
                Case = "Case1",
                UserName = "User1",
                Type = "Type1",
                Endpoint = "Endpoint1"
            };

            var result = LineParser.ParseLine(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Timestamp, result.Timestamp);
            Assert.AreEqual(expected.Case, result.Case);
            Assert.AreEqual(expected.UserName.ToUpper(), result.UserName);
            Assert.AreEqual(expected.Type, result.Type);
            Assert.AreEqual(expected.Endpoint, result.Endpoint);
        }
    }
}