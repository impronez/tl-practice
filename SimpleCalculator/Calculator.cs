namespace SimpleCalculator;

public enum Operation
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

public static class Calculator
{
    public static double Calculate(double a, double b, Operation operation)
    {
        var result =  operation switch
        {
            Operation.Addition => Add(a, b),
            Operation.Subtraction => Subtract(a, b),
            Operation.Multiplication => Multiply(a, b),
            Operation.Division => Divide(a, b),
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation")
        };

        if (double.IsInfinity(result))
            throw new OverflowException();

        return result;
    }

    private static double Add(double a, double b)
    {
        return a + b;
    }

    private static double Subtract(double a, double b)
    {
        return a - b;
    }

    private static double Multiply(double a, double b)
    {
        return a * b;
    }

    private static double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException();
        
        return a / b;
    }
}