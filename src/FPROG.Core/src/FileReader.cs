namespace FPROG.Core;

public class FileReader
{
    public string? InputText {get; private set;}
    public Dictionary<string,List<string>> SearchTerms {get; private set;}

    public FileReader(string searchFilePath, List<string> searchTermPaths)
    {
        SearchTerms = new Dictionary<string, List<string>>();
        InitSearchFileAndTerms(searchFilePath, searchTermPaths);
    }

    /// <summary>
    /// Non-pure function to initialize InputText and SearchTerms
    /// </summary>
    /// <param name="searchFilePath"></param>
    /// <param name="searchTermPaths"></param>
    /// <returns>int</returns>
    private void InitSearchFileAndTerms(string searchFilePath, List<string> searchTermPaths)
    {
        try {
            InputText = File.ReadAllText(searchFilePath);
            foreach (var path in searchTermPaths)
            {
                string text = File.ReadAllText(path);
                string fileName = Path.GetFileName(path);
                // man könnt sich überlegen, statt 'StringSplitOptions.None' 'StringSplitOptions.RemoveEmptyEntries' zu verwenden.
                // macht von dem her keinen unterschied würd aber leere zeilen ignorieren.
                // außerdem würd i das splitten generell noch weglassen und dafür eine eigene funktion machen, weil das prinzipiell auch gefordert ist.
                List<string> terms = new(text.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
                SearchTerms.Add(fileName, terms);
            }
        } catch (Exception e) {
            throw new Exception("Could not initialize files",e);
        }
    }
}