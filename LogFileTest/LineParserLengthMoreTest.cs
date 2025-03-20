using LogFileBatchUploader.Services;

namespace LogFileTest
{
    [TestClass]
    public sealed class LineParserLengthMoreTest
    {
        [TestMethod]
        public void ParseLineInvalidPartsLength()
        {
            
            string line = "2021-01-01 | Case1 | User1 | Type1 | test | test | test  "; // Mer än 6 delar
            
            var result = LineParser.ParseLine(line);
            
            Assert.IsNull(result);
  
        }
    }
}