namespace Dictionary;

class Program
{
    private const string GetTranslationCommandString = "1";
    private const string AddTranslationCommandString = "2";
    private const string PrintMenuCommandString = "3";
    private const string ExitCommandString = "4";

    static void Main()
    {
        var dictionary = new Dictionary();

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

            var input = Console.ReadLine();

            if (input == GetTranslationCommandString)
                RunGetTranslationMenu(dictionary);
            else if (input == AddTranslationCommandString)
                RunAddTranslationMenu(dictionary);
            else if (input == PrintMenuCommandString)
                PrintMenu();
            else if (input == ExitCommandString)
                break;
            else
                Console.WriteLine($"Invalid command: {input}. Please try again.");
        }
    }

    private static void RunGetTranslationMenu(Dictionary dictionary)
    {
        var word = GetStringFromConsole("word");

        var translationResult = dictionary.GetTranslation(word);
        if (translationResult.Length > 0)
        {
            Console.WriteLine($"Translation: {string.Join(", ", translationResult)}");
            return;
        }

        Console.WriteLine($"Translation of [{word}] is not found.");
        Console.Write("If you want add translation to a dictionary, press any symbol or press 'enter' to exit: ");

        if (Console.ReadKey().Key == ConsoleKey.Enter) return;
        Console.WriteLine();

        RunAddTranslationMenu(dictionary, word);
    }

    private static void RunAddTranslationMenu(Dictionary dictionary, string? word = null)
    {
        if (string.IsNullOrEmpty(word))
        {
            word = GetStringFromConsole("word");
        }

        var translation = GetStringFromConsole("translation");

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

            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;

            Console.WriteLine($"Invalid value: '{input}'. Please try again.");
        }
    }
}