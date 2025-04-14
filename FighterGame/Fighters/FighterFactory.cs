using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public static class FighterFactory
{
    public static IFighter CreateFighter()
    {
        string name = GetNameFromConsole();
        int initiative = GetInitiativeFromConsole();
        IRace race = GetRaceFromConsole();
        IFighterType fighterType = GetFighterTypeFromConsole();
        IArmor armor = GetArmorFromConsole();
        IWeapon weapon = GetWeaponsFromConsole();

        return new Fighter(name, initiative, race, fighterType, armor, weapon);
    }

    private static string GetNameFromConsole()
    {
        while (true)
        {
            Console.Write("Enter fighter's name: ");

            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) return name;

            Console.WriteLine("Invalid value! Name can't be empty.");
        }
    }

    private static int GetInitiativeFromConsole()
    {
        while (true)
        {
            Console.Write("Enter initiative (1 or 2): ");

            var value = Console.ReadLine();
            var isSuccess = int.TryParse(value, out int initiative);
            if (!isSuccess || (initiative != 1 && initiative != 2))
            {
                Console.Write($"Invalid value: [{value}]! ");
                continue;
            }

            return initiative;
        }
    }

    private static IRace GetRaceFromConsole()
    {
        var races = new List<IRace>
        {
            new Dwarf(), new Elf(), new Orc(), new Human()
        };

        return GetParameterFromConsole("race", races);
    }

    private static IFighterType GetFighterTypeFromConsole()
    {
        var races = new List<IFighterType>
        {
            new Knight(), new Mercenary()
        };

        return GetParameterFromConsole("fighter's type", races);
    }

    private static IArmor GetArmorFromConsole()
    {
        var armors = new List<IArmor>
        {
            new NoArmor(), new LightArmor(), new MediumArmor()
        };

        return GetParameterFromConsole("armor", armors);
    }

    private static IWeapon GetWeaponsFromConsole()
    {
        var weapons = new List<IWeapon>
        {
            new Blade(), new Firsts(), new Knife(), new Sword()
        };

        return GetParameterFromConsole("weapon", weapons);
    }

    private static T GetParameterFromConsole<T>(string parameterName, List<T> acceptableValues)
    {
        Console.WriteLine($"Acceptable values of {parameterName}:");
        for (int i = 0; i < acceptableValues.Count; i++)
        {
            Console.WriteLine($"{i} - {acceptableValues[i].GetType().Name}");
        }

        while (true)
        {
            Console.Write($"Enter {parameterName} index: ");

            var isSuccess = int.TryParse(Console.ReadLine(), out int index);
            if (!isSuccess || index < 0 || index >= acceptableValues.Count)
            {
                Console.Write($"Invalid value: [{index}]! ");
                continue;
            }

            return acceptableValues[index];
        }
    }
}