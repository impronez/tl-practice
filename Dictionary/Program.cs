namespace Dictionary;

class Program
{
    private const string GetTranslationCommandString = "1";
    private const string AddTranslationCommandString = "2";
    private const string PrintMenuCommandString = "3";
    private const string ExitCommandString = "4";

    private static readonly string FilePath = "dict.txt";

    static void Main()
    {
        Dictionary dictionary = new Dictionary(FilePath);

        RunMenu(dictionary);

        dictionary.SaveToFile();
    }

    private static void RunMenu(Dictionary dictionary)
    {
        Console.WriteLine("Welcome to a dictionary app!");
        PrintMenu();

        while (true)
        {
            Console.Write("Enter a command: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case GetTranslationCommandString:
                    RunGetTranslationMenu(dictionary);
                    break;
                case AddTranslationCommandString:
                    RunAddTranslationMenu(dictionary);
                    break;
                case PrintMenuCommandString:
                    PrintMenu();
                    break;
                case ExitCommandString:
                    return;
                default:
                    Console.WriteLine($"Invalid command: {input}. Please try again.");
                    break;
            }
        }
    }

    private static void RunGetTranslationMenu(Dictionary dictionary)
    {
        string word = GetStringFromConsole("word");

        string[] translationResult = dictionary.GetTranslations(word);
        if (translationResult.Length > 0)
        {
            Console.WriteLine($"Translation: {string.Join(", ", translationResult)}");
            return;
        }

        Console.WriteLine($"Translation of [{word}] is not found.");
        Console.Write("If you want add translation to a dictionary, press any symbol or press 'enter' to exit: ");

        if (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            return;
        }
        
        Console.WriteLine();

        RunAddTranslationMenu(dictionary, word);
    }

    private static void RunAddTranslationMenu(Dictionary dictionary, string? word = null)
    {
        if (string.IsNullOrEmpty(word))
        {
            word = GetStringFromConsole("word");
        }

        string translation = GetStringFromConsole("translation");

        dictionary.AddTranslation(word, translation);
    }

    private static void PrintMenu()
    {
        var menuString =
            $"Commands:\n{GetTranslationCommandString} - get translation;" +
            $"\n{AddTranslationCommandString} - add translation;" +
            $"\n{PrintMenuCommandString} - print menu;" +
            $"\n{ExitCommandString} - exit.";

        Console.WriteLine(menuString);
    }

    private static string GetStringFromConsole(string parameterName)
    {
        while (true)
        {
            Console.Write($"Enter a {parameterName}: ");

            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Console.WriteLine($"Invalid value: '{input}'. Please try again.");
        }
    }
}