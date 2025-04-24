using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public static class FighterFactory
{
    public static IFighter CreateFighter(string name,
        int initiative,
        IRace race,
        IFighterType fighterType,
        IArmor armor,
        IWeapon weapon)
    {
        return new Fighter(name, initiative, race, fighterType, armor, weapon);
    }
}