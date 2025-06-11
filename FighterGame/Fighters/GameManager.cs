using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities.CommandLine;
using Fighters.Utilities.FighterInputProvider;
using Fighters.Utilities.RandomService;

namespace Fighters;

public class GameManager
{
    private readonly ICommandLine _commandLine;
    private readonly IFighterInputProvider _fighterInputProvider;
    private readonly IRandomService _randomService;

    private List<IFighter> _fighters;

    public GameManager(ICommandLine commandLine,
        IFighterInputProvider fighterInputProvider,
        IRandomService randomService)
    {
        _commandLine = commandLine;
        _fighterInputProvider = fighterInputProvider;
        _randomService = randomService;

        _fighters = new List<IFighter>();
    }

    public void Run()
    {
        _commandLine.WriteLine("Welcome to the Gladiators Game!");
        _commandLine.WriteLine("Commands:\n'add' - add a fighter\n'battle' - start a battle\n'exit' - exit the game");

        var fighters = new List<IFighter>();

        while (true)
        {
            _commandLine.Write("Enter command: ");

            string? command = _commandLine.ReadLine();
            switch (command?.ToLower())
            {
                case "add":
                    fighters.Add(GetFighter());
                    break;
                case "battle":
                    if (fighters.Count < 2)
                    {
                        _commandLine.WriteLine("Count of fighters must be more than 2!");
                        break;
                    }

                    StartBattle();
                    _commandLine.WriteLine($"Battle finished! Winner: {fighters[0].Name}");
                    return;
                case "exit":
                    _commandLine.WriteLine("Goodbye!");
                    return;
                default:
                    _commandLine.WriteLine($"Command '{command}' is not a valid!");
                    break;
            }
        }
    }

    private void StartBattle()
    {
        _fighters = _fighters.OrderByDescending(f => f.Initiative).ToList();

        while (_fighters.Count > 1)
        {
            IFighter firstFighter = _fighters[0];
            IFighter secondFighter = _fighters[1];

            Fight(firstFighter, secondFighter);
        }
    }

    private void Fight(IFighter firstFighter, IFighter secondFighter)
    {
        _commandLine.WriteLine($"Battle: {firstFighter.Name} VS {secondFighter.Name}");
        _commandLine.WriteLine($"First fighter stats:\n{firstFighter.GetStats()}");
        _commandLine.WriteLine($"Second fighter stats:\n{secondFighter.GetStats()}");

        while (true)
        {
            if (!firstFighter.IsAlive())
            {
                _commandLine.WriteLine($"{firstFighter.Name} is dead!");
                _fighters.Remove(firstFighter);
                break;
            }

            Attack(firstFighter, secondFighter);

            if (!secondFighter.IsAlive())
            {
                _commandLine.WriteLine($"{secondFighter.Name} is dead!");
                _fighters.Remove(secondFighter);
                break;
            }

            Attack(secondFighter, firstFighter);
        }
    }

    private void Attack(IFighter attacker, IFighter defender)
    {
        int attackerDamage = attacker.CalculateDamage();
        defender.TakeDamage(attackerDamage);
        _commandLine.WriteLine($"{attacker.Name} deals {attackerDamage} damage to {defender.Name}");
    }

    private IFighter GetFighter()
    {
        string name = _fighterInputProvider.GetName();
        int initiative = _fighterInputProvider.GetInitiative();
        IFighterType fighterType = _fighterInputProvider.GetFighterType();
        IRace race = _fighterInputProvider.GetRace();
        IArmor armor = _fighterInputProvider.GetArmor();
        IWeapon weapon = _fighterInputProvider.GetWeapon();

        return FighterFactory.CreateFighter(name,
            initiative,
            race,
            fighterType,
            armor,
            weapon,
            _randomService);
    }
}