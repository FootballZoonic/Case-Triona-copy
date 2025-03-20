using LogFileBatchUploader.Services;

namespace LogFileTest;

[TestClass]
public class LineParserLengthLessTest
{
    [TestMethod]
    public void ParseLineInvalidPartsLength()
    {
        string line = "2021-01-01 | Case1 | User1 | Type1  "; // Mindre än 6 delar

        var result = LineParser.ParseLine(line);

        Assert.IsNull(result);
    }
}
