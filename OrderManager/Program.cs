PrintWelcomeMessage();

string productName;
uint productQuantity;
string userName;
string deliveryAddress;

while (true)
{
    productName = GetStringParameterFromConsole("название товара");
    productQuantity = GetUintParameterFromConsole("количество товара");
    userName = GetStringParameterFromConsole("имя");
    deliveryAddress = GetStringParameterFromConsole("адрес доставки");

    if (IsCorrectOrderData(productName, productQuantity, userName, deliveryAddress))
        break;
}

PrintSuccessfulOrderConfirmationMessage(productName, productQuantity, userName, deliveryAddress);

static void PrintSuccessfulOrderConfirmationMessage(string productName, uint productQuantity, string userName,
    string deliveryAddress)
{
    Console.WriteLine(
        $"{userName}! Ваш заказ '{productName}' в количестве {productQuantity} шт. оформлен! Доставка по адресу: {deliveryAddress}");
}

static bool IsCorrectOrderData(string productName, uint productQuantity, string userName, string deliveryAddress)
{
    Console.WriteLine(
        $"{userName}, вы заказали '{productName}' в количестве {productQuantity} шт. на адрес '{deliveryAddress}'.");

    Console.Write("Если всё верно, нажмите 'Y', если нет, нажмите любую клавишу: ");

    var isConfirmed = Console.ReadKey().Key == ConsoleKey.Y;
    Console.WriteLine();

    return isConfirmed;
}

static uint GetUintParameterFromConsole(string parameterName)
{
    uint value;

    Console.Write($"Введите {parameterName}: ");

    while (true)
    {
        var isParsed = uint.TryParse(Console.ReadLine(), out value);
        if (!isParsed || value == 0)
            Console.Write($"Невалидное значение! Введите {parameterName} еще раз: ");
        else
            break;
    }

    return value;
}

static string GetStringParameterFromConsole(string parameterName)
{
    string value;

    Console.Write($"Введите {parameterName}: ");

    while (true)
    {
        value = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(value))
            break;

        Console.Write($"Введенное значение не может быть пустым. Введите {parameterName} еще раз: ");
    }

    return value;
}

static void PrintWelcomeMessage()
{
    Console.WriteLine("Добро пожаловать в центр заказов!");
}