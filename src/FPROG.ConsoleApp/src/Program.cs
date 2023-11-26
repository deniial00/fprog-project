using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static Func<string, List<string>> FileReader = (string filePath) =>
    {
        try
        {
            return File.ReadAllLines(filePath).ToList();
        }
        catch (Exception e)
        {
            throw new Exception("Could not initialize files", e);
        }
    };

    static Func<List<string>, List<string>> Tokenizer = (List<string> fileContent) =>
        fileContent
            .SelectMany(line => line.Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries))
            .ToList();

    static Func<List<string>, List<string>, List<string>> FilterWords = (List<string> listToFilter, List<string> filterList) =>
        listToFilter
            .Where(word => filterList.Contains(word))
            .ToList();

    static Func<List<string>, List<List<string>>> SplitIntoChapters = (List<string> inputText) =>
    {
        List<List<string>> chapters = new List<List<string>>();
        List<string> currentChapter = new List<string>();

        foreach (string word in inputText)
        {
            if (word.Contains("CHAPTER"))
            {
                if (currentChapter.Count > 0)
                {
                    chapters.Add(new List<string>(currentChapter));
                    currentChapter = new List<string>();
                }
            }

            currentChapter.Add(word);
        }

        if (currentChapter.Count > 0)
        {
            chapters.Add(new List<string>(currentChapter));
        }

        return chapters.Skip(1).ToList();
    };

    static Func<List<string>, List<string>, string> CategorizeChapter = (List<string> warTerms, List<string> peaceTerms) =>
    {
        string FormatTermResult(string termType, List<string> terms)
        {
            if (terms.Count == 0) return "";

            string termResult = $"{termType} term count: {terms.Count}\n{termType} terms: ";
            termResult += string.Join(", ", terms) + "\n";
            return termResult;
        }

        string warTermResult = FormatTermResult("war", warTerms);
        string peaceTermResult = FormatTermResult("peace", peaceTerms);

        string result = warTerms.Count > peaceTerms.Count
            ? "war-related\n" + warTermResult + peaceTermResult
            : "peace-related\n" + warTermResult + peaceTermResult;

        return result;
    };


    /*static Func<List<string>, List<string>, Dictionary<string, double>> CalculateTermDensity = (List<string> termList, List<string> filterList) =>
    {
        var distances = CalculateDistances(termList);

        return distances.ToDictionary(
            entry => entry.Key,
            entry => entry.Value.Select((_, index) => index).Average()
        );

        Dictionary<string, List<int>> CalculateDistances(List<string> termOccurrences)
        {
            return termOccurrences
                .GroupBy(word => word)
                .ToDictionary(
                    group => group.Key,
                    group => Enumerable.Range(0, group.Count()).ToList()
                );
        }
    };*/

    static void Main(string[] args)
    {
        const string warAndPeaceFilePath = @"war-and-peace.txt";
        const string warTermsFilePath = @"war-terms.txt";
        const string peaceTermsFilePath = @"peace-terms.txt";

        List<string> warAndPieceContentTokenized = Tokenizer(FileReader(warAndPeaceFilePath));
        List<string> warTermsTokenized = Tokenizer(FileReader(warTermsFilePath));
        List<string> peaceTermsTokenized = Tokenizer(FileReader(peaceTermsFilePath));

        List<List<string>> chapters = SplitIntoChapters(warAndPieceContentTokenized);

        int cnt = 0;
        chapters.ForEach(chapter =>
        {
            List<string> filteredWarTerms = FilterWords(chapter, warTermsTokenized);
            List<string> filteredPeaceTerms = FilterWords(chapter, peaceTermsTokenized);

            /*Dictionary<string, double> warTermsDensity = CalculateTermDensity(filteredWarTerms, warTermsTokenized);
            Dictionary<string, double> peaceTermsDensity = CalculateTermDensity(filteredPeaceTerms, peaceTermsTokenized);*/

            string chapterCategory = CategorizeChapter(filteredWarTerms, filteredPeaceTerms);

            /*Console.WriteLine($"Chapter {cnt + 1} - War Terms Density: {string.Join(", ", warTermsDensity)}");
            Console.WriteLine($"Chapter {cnt + 1} - Peace Terms Density: {string.Join(", ", peaceTermsDensity)}");*/
            Console.WriteLine($"Chapter {cnt + 1}: {chapterCategory}");
            cnt++;
        });
    }
}
