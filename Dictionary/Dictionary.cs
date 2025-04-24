namespace Dictionary;

public class Dictionary
{
    private const char PairSeparator = ':';
    private const char TranslationSeparator = '|';

    private readonly string _dictionaryFileName;
    private readonly Dictionary<string, List<string>> _dictionary;

    public Dictionary(string filePath)
    {
        _dictionaryFileName = filePath;
        _dictionary = new Dictionary<string, List<string>>();
        LoadDictionaryFromFile(filePath);
    }

    public void AddTranslation(string word, string translation)
    {
        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentNullException(nameof(word), "Word cannot be empty");
        }

        if (string.IsNullOrEmpty(translation))
        {
            throw new ArgumentNullException(nameof(word), "Translation cannot be empty");
        }

        if (_dictionary.ContainsKey(word) && !_dictionary[word].Contains(translation))
        {
            _dictionary[word].Add(translation);
        }
        else if (!_dictionary.ContainsKey(word))
        {
            _dictionary.Add(word, [translation]);
        }
    }

    public string[] GetTranslations(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentNullException(nameof(word), "Word cannot be empty");
        }

        return !_dictionary.TryGetValue(word, out var value) ? [] : value.ToArray();
    }

    public void SaveToFile()
    {
        if (_dictionary.Count == 0) return;

        try
        {
            using var writer = new StreamWriter(_dictionaryFileName);

            foreach (var pair in _dictionary)
            {
                var translations = pair.Value.Count > 1
                    ? string.Join(TranslationSeparator, pair.Value)
                    : pair.Value[0];

                var result = $"{pair.Key}{PairSeparator}{translations}";

                writer.WriteLine(result);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"There is no write access to the file: {_dictionaryFileName}");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Path not found: {_dictionaryFileName}");
        }
    }

    private void LoadDictionaryFromFile(string filePath)
    {
        if (!File.Exists(filePath)) return;

        using var reader = new StreamReader(filePath);
        int lineNumber = 0;

        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split(PairSeparator);
            if (parts.Length != 2)
            {
                throw new FormatException(
                    $"Invalid file format at line {lineNumber}. Use: 'word:translate' or 'word:translate|translate|...'");
            }

            string[] translations = parts[1].Split(TranslationSeparator);

            _dictionary.Add(parts[0], translations.ToList());
        }
    }
}