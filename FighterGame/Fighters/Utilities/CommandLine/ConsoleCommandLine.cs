namespace Fighters.Utilities.CommandLine;

public class ConsoleCommandLine : ICommandLine
{
    public void Write(string? value)
    {
        Console.Write(value);
    }

    public void WriteLine(string? value)
    {
        Console.WriteLine(value);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();        
    }
}