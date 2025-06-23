using Fighters.Utilities.CommandLine;
using Fighters.Utilities.FighterInputProvider;
using Fighters.Utilities.RandomService;

namespace Fighters;

public class Program
{
    public static void Main()
    {
        ICommandLine commandLine = new ConsoleCommandLine();
        IFighterInputProvider fighterInputProvider = new ConsoleFighterInputProvider(commandLine);
        IRandomService randomService = new RandomService();
        GameManager gameManager = new GameManager(commandLine, fighterInputProvider, randomService);
        
        gameManager.Run();
    }
}