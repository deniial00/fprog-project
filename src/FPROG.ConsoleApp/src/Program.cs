using System.Collections.Concurrent;
using System.Numerics;

namespace FPROG.ConsoleApp;

class Program {
    static void Main(string[] args)
    {
        const string warAndPeaceFilePath = @"war-and-peace.txt";
        const string warTermsFilePath = @"war-terms.txt";
        const string peaceTermsFilePath = @"peace-terms.txt";

        Func<string, List<string>> FileReader = (string filePath) =>
        {
            try
            {
                List<string> result = new List<string>();
                string[] fileLines = File.ReadAllLines(filePath);
                result.AddRange(fileLines);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Could not initialize files", e);
            }
        };

        Func<List<string>, List<string>> Tokenizer = (List<string> fileContent) =>
        {
            try
            {
                List<string> fileContentAsWords = new List<string>();
                fileContent.ForEach(line => {
                    string[] words = line.Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    fileContentAsWords.AddRange(words);
                });

                return fileContentAsWords;
            }
            catch (Exception e)
            {
                throw new Exception("Could not initialize files", e);
            }
        };

        Func<List<string>, List<string>, List<string>> FilterWords = (List<string> listToFilter, List<string> filterList) =>
        {
            try
            {
                List<string> filteredWords = listToFilter
                    .Where(word => filterList.Contains(word))
                    .ToList();


                return filteredWords;
            }
            catch (Exception e)
            {
                throw new Exception("Could not initialize files", e);
            }
        };

        static Dictionary<string, int> CountWordOccurrences(List<string> wordList)
        {
            var wordOccurrences = new ConcurrentDictionary<string, int>();

            // Use parallel processing to count occurrences efficiently
            Parallel.ForEach(wordList, word =>
            {
                // Use AddOrUpdate to safely update the dictionary in parallel
                wordOccurrences.AddOrUpdate(word, 1, (_, count) => count + 1);
            });

            // Convert ConcurrentDictionary to regular Dictionary
            return wordOccurrences.ToDictionary(pair => pair.Key, pair => pair.Value);
        }









        List<string> warAndPieceContent = FileReader(warAndPeaceFilePath);
        List<string> warTermsContent = FileReader(warTermsFilePath);
        List<string> peaceTermsContent = FileReader(peaceTermsFilePath);

        List<string> warAndPieceContentAsWords = Tokenizer(warAndPieceContent);

        List<string> filteredWarTerms = FilterWords(warAndPieceContentAsWords, warTermsContent);
        List<string> filteredPeaceTerms = FilterWords(warAndPieceContentAsWords, peaceTermsContent);

        Dictionary<string, int> warTermsOccurences = CountWordOccurrences(filteredWarTerms);
        Dictionary<string, int> peaceTermsOccurences = CountWordOccurrences(filteredPeaceTerms);

        // warTermsContent.ForEach(line => { Console.WriteLine(line); });
        // warAndPieceContentAsWords.ForEach(line => { Console.WriteLine(line); });
         filteredWarTerms.ForEach(line => { Console.WriteLine(line); });
        // filteredPeaceTerms.ForEach(line => { Console.WriteLine(line); });
        /*foreach (var entry in warTermsOccurences)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} times");
        }

        foreach (var entry in peaceTermsOccurences)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} times");
        }*/






        // annahme man bekommt vom reader ein dictionary mit 3 strings zurück
        /*Dictionary<string, string>? fileReaderResponse = new Dictionary<string, string>();
        Dictionary<string, List<string>>? tokenizerResponse = Tokenizer.Tokenize(fileReaderResponse);
        Dictionary<string, List<string>>? wordsFilterResponse = WordsFilter.FilterWords(tokenizerResponse);
        // TODO: research mas mit map-reduce philosophy gmeint ist
        OccurencesCounter.CountOccurrences();
        TermDensityCalculator.CalculateTermDensity();
        Console.WriteLine("Hello, World!");*/
    }
}
