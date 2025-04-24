using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities;
using Fighters.Utilities.FighterInputProvider;

namespace Fighters;

public static class GameManager
{
    private static readonly IFighterInputProvider FighterInputProvider = new ConsoleFighterInputProvider();

    public static void Run()
    {
        Console.WriteLine("Welcome to the Gladiators Game!");
        Console.WriteLine("Commands:\n'add' - add a fighter\n'battle' - start a battle\n'exit' - exit the game");

        var fighters = new List<IFighter>();

        while (true)
        {
            Console.Write("Enter command: ");

            var command = Console.ReadLine();
            switch (command?.ToLower())
            {
                case "add":
                    fighters.Add(GetFighter());
                    break;
                case "battle":
                    if (fighters.Count < 2)
                    {
                        Console.WriteLine("Count of fighters must be more than 2!");
                        break;
                    }

                    StartBattle(fighters);
                    Console.WriteLine($"Battle finished! Winner: {fighters[0].Name}");
                    return;
                case "exit":
                    return;
                default:
                    Console.WriteLine($"Command '{command}' is not a valid!");
                    break;
            }
        }
    }

    private static void StartBattle(List<IFighter> fighters)
    {
        fighters = fighters.OrderByDescending(f => f.Initiative).ToList();

        while (fighters.Count != 1)
        {
            var firstFighter = fighters[0];
            var secondFighter = fighters[1];

            Fight(ref fighters, firstFighter, secondFighter);
        }
    }

    private static void Fight(ref List<IFighter> fighters, IFighter firstFighter, IFighter secondFighter)
    {
        Console.WriteLine($"Battle: {firstFighter.Name} VS {secondFighter.Name}");
        Console.WriteLine($"First fighter stats:\n{firstFighter.GetStats()}");
        Console.WriteLine($"Second fighter stats:\n{secondFighter.GetStats()}");

        while (true)
        {
            if (!firstFighter.IsAlive())
            {
                Console.WriteLine($"{firstFighter.Name} is dead!");
                fighters.Remove(firstFighter);
                break;
            }

            Attack(firstFighter, secondFighter);

            if (!secondFighter.IsAlive())
            {
                Console.WriteLine($"{secondFighter.Name} is dead!");
                fighters.Remove(secondFighter);
                break;
            }

            Attack(secondFighter, firstFighter);
        }
    }

    private static void Attack(IFighter attacker, IFighter defender)
    {
        int attackerDamage = attacker.CalculateDamage();
        defender.TakeDamage(attackerDamage);
        Console.WriteLine($"{attacker.Name} deals {attackerDamage} damage to {defender.Name}");
    }

    private static IFighter GetFighter()
    {
        string name = FighterInputProvider.GetName();
        int initiative = FighterInputProvider.GetInitiative();
        IFighterType fighterType = FighterInputProvider.GetFighterType();
        IRace race = FighterInputProvider.GetRace();
        IArmor armor = FighterInputProvider.GetArmor();
        IWeapon weapon = FighterInputProvider.GetWeapon();

        return FighterFactory.CreateFighter(name,
            initiative,
            race,
            fighterType,
            armor,
            weapon);
    }
}