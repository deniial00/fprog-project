using FPROG.Core;

namespace FPROG.ConsoleApp;

class Program {
    static void Main(string[] args)
    {
        var reader = new FileReader(
            @"war-and-peace.txt",
            new(){
                @"peace-terms.txt",
                @"war-terms.txt"
            }
        );

        // annahme man bekommt vom reader ein dictionary mit 3 strings zurück
        Dictionary<string, string>? fileReaderResponse = new Dictionary<string, string>();
        Dictionary<string, List<string>>? tokenizerResponse = Tokenizer.Tokenize(fileReaderResponse);
        Dictionary<string, List<string>>? wordsFilterResponse = WordsFilter.FilterWords(tokenizerResponse);
        OccurencesCounter.CountOccurrences();
        TermDensityCalculator.CalculateTermDensity();
        Console.WriteLine("Hello, World!");
    }
}
