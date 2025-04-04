namespace Dictionary;

public class Dictionary
{
    private const string DictionaryFileName = "dict.txt";
    private const char PairSeparator = ':';
    private const char TranslationSeparator = '|';

    private readonly Dictionary<string, List<string>> _dictionary;

    public Dictionary()
    {
        _dictionary = new Dictionary<string, List<string>>();
        LoadDictionaryFromFile();
    }

    public void AddTranslation(string word, string translation)
    {
        if (string.IsNullOrEmpty(word))
            throw new ArgumentNullException(nameof(word), "Word cannot be empty");
        
        if (string.IsNullOrEmpty(translation))
            throw new ArgumentNullException(nameof(word), "Translation cannot be empty");
        
        if (_dictionary.ContainsKey(word) && !_dictionary[word].Contains(translation))
            _dictionary[word].Add(translation);
        else if (!_dictionary.ContainsKey(word))
            _dictionary.Add(word, [translation]);
    }

    public string[] GetTranslation(string word)
    {
        if (string.IsNullOrEmpty(word))
            throw new ArgumentNullException(nameof(word), "Word cannot be empty");
        
        return !_dictionary.TryGetValue(word, out var value) ? [] : value.ToArray();
    }

    public void SaveToFile()
    {
        if (_dictionary.Count == 0) return;

        using var writer = new StreamWriter(DictionaryFileName);

        foreach (var pair in _dictionary)
        {
            var translations = pair.Value.Count > 1
                ? string.Join(TranslationSeparator, pair.Value)
                : pair.Value[0];

            var result = $"{pair.Key}{PairSeparator}{translations}";

            writer.WriteLine(result);
        }

        writer.Close();
    }

    private void LoadDictionaryFromFile()
    {
        if (!File.Exists(DictionaryFileName)) return;

        using var reader = new StreamReader(DictionaryFileName);

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line!.Split(PairSeparator);
            if (parts.Length != 2)
                throw new FormatException(
                    "Invalid file format. Use: 'word:translate' or 'word:translate|translate|...'");

            var translations = parts[1].Split(TranslationSeparator);

            _dictionary.Add(parts[0], translations.ToList());
        }

        reader.Close();
    }
}