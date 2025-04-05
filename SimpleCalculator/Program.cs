using SimpleCalculator;

string? line;
double result = 0;

while (true)
{
    Console.Write("Enter expression: ");
    line = Console.ReadLine();
    if (string.IsNullOrEmpty(line))
        break;

    try
    {
        var split = line.Split(" ");

        var a = double.Parse(split[0]);
        var b = double.Parse(split[2]);

        var operation = GetOperationByStringOperator(split[1]);

        result = Calculator.Calculate(a, b, operation);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
    finally
    {
        Console.WriteLine($"Result: {result}");
    }
}

return;

Operation GetOperationByStringOperator(string op)
{
    return op switch
    {
        "+" => Operation.Addition,
        "-" => Operation.Subtraction,
        "*" => Operation.Multiplication,
        "/" => Operation.Division,
        _ => throw new InvalidDataException("Unknown operator")
    };
}