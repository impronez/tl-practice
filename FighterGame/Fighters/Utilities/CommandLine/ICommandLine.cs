namespace Fighters.Utilities.CommandLine;

public interface ICommandLine
{
    void Write(string value);
    void WriteLine(string value);
    string? ReadLine();
}