using Fighters.Models.Armors;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Utilities.FighterInputProvider;

public interface IFighterInputProvider
{
    public string GetName();
    public int GetInitiative();
    public IRace GetRace();
    public IFighterType GetFighterType();
    public IArmor GetArmor();
    public IWeapon GetWeapon();
}