namespace FPROG.Tests;

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
        Assert.IsTrue(!string.IsNullOrEmpty(reader.InputText), "Search text is empty or null.");
        Assert.IsTrue(reader.SearchTerms != null, "Search terms is null.");
    }

    [Test]
    public void CheckSearchTermsFormatted()
    {
        var reader = new FileReader(inputFilePath, searchTermPaths);
        var searchTerms = reader.SearchTerms;

        foreach (var list in searchTerms)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(list.Key), "Search terms key is empty or null.");
            
            foreach (var term in list.Value)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(term), "Search term value is empty or null.");
            }
        }
    }
}