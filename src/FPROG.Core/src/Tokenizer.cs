namespace FPROG.Core;

    public class Tokenizer
    {
        // TODO: alle chars wie ',' '.' ignorieren
        public static Dictionary<string, List<string>>? Tokenize (Dictionary<string, string> fileReaderResponse)
        {
            var tokenizedDictionary = new Dictionary<string, List<string>>();

            foreach (var entry in fileReaderResponse)
            {
                var tokenizedValues = entry.Value.Split(new[] { '\n', '\r', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                tokenizedDictionary.Add(entry.Key, tokenizedValues.ToList());
            }

            return tokenizedDictionary;
        }
    }

