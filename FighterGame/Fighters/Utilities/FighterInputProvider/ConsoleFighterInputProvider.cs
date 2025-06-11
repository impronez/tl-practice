using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities.CommandLine;

namespace Fighters.Utilities.FighterInputProvider;

public class ConsoleFighterInputProvider : IFighterInputProvider
{
    private readonly ICommandLine _commandLine;

    public ConsoleFighterInputProvider(ICommandLine commandLine)
    {
        _commandLine = commandLine;
    }
    public string GetName()
    {
        while (true)
        {
            _commandLine.Write("Enter fighter's name: ");

            string? name = _commandLine.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) return name;

            _commandLine.WriteLine("Invalid value! Name can't be empty.");
        }
    }

    public int GetInitiative()
    {
        while (true)
        {
            _commandLine.Write($"Enter initiative [{IFighter.MinInitiative}..{IFighter.MaxInitiative}]: ");

            string? value = _commandLine.ReadLine();
            bool isSuccess = int.TryParse(value, out int initiative);
            if (!isSuccess || initiative < IFighter.MinInitiative || initiative > IFighter.MaxInitiative)
            {
                _commandLine.Write($"Invalid value: '{value}'! ");
                continue;
            }

            return initiative;
        }
    }

    public IRace GetRace()
    {
        var races = new List<IRace>
        {
            new Dwarf(), new Elf(), new Orc(), new Human()
        };

        return GetParameterFromCommandLine("race", races);
    }

    public IFighterType GetFighterType()
    {
        var races = new List<IFighterType>
        {
            new Knight(), new Mercenary()
        };

        return GetParameterFromCommandLine("fighter's type", races);
    }

    public IArmor GetArmor()
    {
        var armors = new List<IArmor>
        {
            new NoArmor(), new LightArmor(), new MediumArmor()
        };

        return GetParameterFromCommandLine("armor", armors);
    }

    public IWeapon GetWeapon()
    {
        var weapons = new List<IWeapon>
        {
            new Blade(), new Fists(), new Knife(), new Sword()
        };

        return GetParameterFromCommandLine("weapon", weapons);
    }

    private T GetParameterFromCommandLine<T>(string parameterName, List<T> acceptableValues)
    {
        _commandLine.WriteLine($"Acceptable values of {parameterName}:");
        for (int i = 0; i < acceptableValues.Count; i++)
        {
            _commandLine.WriteLine($"{i} - {acceptableValues[i].GetType().Name}");
        }

        while (true)
        {
            _commandLine.Write($"Enter {parameterName} index: ");

            bool isSuccess = int.TryParse(_commandLine.ReadLine(), out int index);
            if (!isSuccess || index < 0 || index >= acceptableValues.Count)
            {
                _commandLine.Write($"Invalid value: '{index}'! ");
                continue;
            }

            return acceptableValues[index];
        }
    }
}