namespace FPROG.Tokenizer.Test;

public class FileReaderTests
{
    string inputFilePath;
    List<string> searchTermPaths;

    [SetUp]
    public void Setup()
    {
        inputFilePath = @"war-and-peace.txt";
        searchTermPaths = new List<string>(){
            @"peace-terms.txt",
            @"war-terms.txt"
        };
    }

    [Test]
    public void CheckEmptyInputThrowsError()
    {
        Assert.Throws<Exception>(() => {
            var reader = new FileReader("", new List<string>());
        });
    }

    [Test]
    public void CheckInitFileReader()
    {
        var reader = new FileReader(inputFilePath, searchTermPaths);
        Assert.IsTrue(!string.IsNullOrEmpty(reader.InputText), "String is empty or null.");
        Assert.IsTrue(reader.SearchTerms != null, "Search terms is null.");
    }
}