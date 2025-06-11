using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities.RandomService;

namespace Fighters;

public static class FighterFactory
{
    public static IFighter CreateFighter(string name,
        int initiative,
        IRace race,
        IFighterType fighterType,
        IArmor armor,
        IWeapon weapon,
        IRandomService randomService)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace", nameof(name));
        }

        if (initiative < IFighter.MinInitiative || initiative > IFighter.MaxInitiative)
        {
            throw new ArgumentOutOfRangeException(nameof(initiative), initiative,
                $"Initiative must be between {IFighter.MinInitiative} and {IFighter.MaxInitiative}");
        }

        return new Fighter(name,
            initiative,
            race,
            fighterType,
            armor,
            weapon,
            randomService);
    }
}