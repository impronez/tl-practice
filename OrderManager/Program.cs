PrintWelcomeMessage();

while (true)
{
    string productName = GetRequiredStringParameterFromConsole("название товара");
    uint productQuantity = GetRequiredPositiveUintParameterFromConsole("количество товара");
    string userName = GetRequiredStringParameterFromConsole("имя");
    string deliveryAddress = GetRequiredStringParameterFromConsole("адрес доставки");

    if (IsOrderConfirmed(productName, productQuantity, userName, deliveryAddress))
    {
        PrintSuccessfulOrderConfirmationMessage(productName, productQuantity, userName, deliveryAddress);
        break;
    }
}

static void PrintSuccessfulOrderConfirmationMessage(string productName,
    uint productQuantity,
    string userName,
    string deliveryAddress)
{
    Console.WriteLine(
        $"{userName}! Ваш заказ '{productName}' в количестве {productQuantity} шт. оформлен! Доставка по адресу: {deliveryAddress}");
}

static bool IsOrderConfirmed(string productName,
    uint productQuantity,
    string userName,
    string deliveryAddress)
{
    Console.WriteLine(
        $"{userName}, вы заказали '{productName}' в количестве {productQuantity} шт. на адрес '{deliveryAddress}'.");

    Console.Write("Если всё верно, нажмите 'Y', если нет, нажмите любую клавишу: ");

    var isConfirmed = Console.ReadKey().Key == ConsoleKey.Y;
    Console.WriteLine();

    return isConfirmed;
}

static uint GetRequiredPositiveUintParameterFromConsole(string parameterName)
{
    uint value;

    do
    {
        value = GetUintParameterFromConsole(parameterName);
        if (value == 0)
        {
            Console.WriteLine($"Значение '{parameterName}' не может быть равно 0!");
        }
    } while (value == 0);

    return value;
}

static uint GetUintParameterFromConsole(string parameterName)
{
    uint value;

    Console.Write($"Введите {parameterName}: ");

    while (true)
    {
        var isParsed = uint.TryParse(Console.ReadLine(), out value);
        if (!isParsed)
        {
            Console.Write($"Невалидное значение! Введите {parameterName} еще раз: ");
        }
        else
        {
            break;
        }
    }

    return value;
}

static string GetRequiredStringParameterFromConsole(string parameterName)
{
    while (true)
    {
        string? value = GetStringParameterFromConsole(parameterName);
        if (!string.IsNullOrEmpty(value))
        {
            return value;
        }

        Console.WriteLine("Введенное значение не может быть пустым!");
    }
}

static string? GetStringParameterFromConsole(string parameterName)
{
    Console.Write($"Введите {parameterName}: ");

    return Console.ReadLine();
}

static void PrintWelcomeMessage()
{
    Console.WriteLine("Добро пожаловать в центр заказов!");
}