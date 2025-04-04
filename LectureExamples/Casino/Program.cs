namespace Casino;

class Program
{
    private const uint MaxBalance = 10000;

    private static Random _random = new();

    private static List<uint> _winNums = [18, 19, 20];

    private static double _multiplicator = 2d;

    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в казино #####!");

        var balance = GetBalanceFromConsole();

        StartMenu(balance);
    }

    static void StartMenu(uint balance)
    {
        PrintMenu();

        string line;

        while (true)
        {
            Console.Write("Введите число: ");

            line = Console.ReadLine();

            if (line == "1")
                Play(ref balance);
            else if (line == "2")
                Console.WriteLine($"Ваш баланс: {balance}");
            else if (line == "3")
            {
                Console.WriteLine("До скорых встреч!");
                break;
            }
            else
                Console.Write("Неверное значение. ");
        }
    }

    static void Play(ref uint balance)
    {
        if (balance == 0)
        {
            Console.WriteLine("Ваш баланс равен 0. Игра невозможна.");
            return;
        }

        var bet = GetBet(balance);

        var randomNum = (uint)_random.Next(1, 20);

        if (_winNums.Contains(randomNum))
        {
            var winning = GetWinning(bet, randomNum);
            balance += winning;
            Console.WriteLine($"Вы выиграли! Ваш выигрыш: {winning}");
        }
        else
        {
            balance -= bet;
            Console.WriteLine($"Вы проиграли! Ваш баланс составляет {balance}");
        }
    }

    static uint GetWinning(uint bet, uint randomNum)
    {
        return bet * (1 + ((uint)_multiplicator * randomNum % 17));
    }

    static uint GetBet(uint balance)
    {
        uint bet;

        Console.Write("Введите ставку: ");

        while (true)
        {
            var isValid = uint.TryParse(Console.ReadLine(), out bet);
            if (isValid && bet <= balance)
            {
                break;
            }

            Console.Write($"Невалидное значение. Введите ставку в диапазоне (1; {balance}]: ");
        }

        return bet;
    }

    static void PrintMenu()
    {
        Console.WriteLine("Меню: ");
        Console.WriteLine("1 - начать игру");
        Console.WriteLine("2 - вывести баланс");
        Console.WriteLine("3 - выйти");
    }

    static uint GetBalanceFromConsole()
    {
        uint balance;

        Console.Write("Введите баланс: ");

        while (true)
        {
            var isValid = uint.TryParse(Console.ReadLine(), out balance);
            if (!isValid || balance == 0 || balance > MaxBalance)
            {
                Console.Write($"Невалидное значение! Введите значение в диапазоне (0; {MaxBalance}]: ");
            }
            else
                break;
        }

        return balance;
    }
}