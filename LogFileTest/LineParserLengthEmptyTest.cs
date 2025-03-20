using LogFileBatchUploader.Services;

namespace LogFileTest;

[TestClass]
public class LineParserLengthEmptyTest
{
    [TestMethod]
    public void ParseLineInvalidPartsLength()
    {
        string lineEmpty = " ";

        var resultEmpty = LineParser.ParseLine(lineEmpty);

        Assert.IsNull(resultEmpty);
    }
}
