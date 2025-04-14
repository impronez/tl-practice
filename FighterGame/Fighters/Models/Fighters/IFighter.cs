using Fighters.Models.Armors;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public interface IFighter
    {
        string Name { get; }
        int Initiative { get; }
        int CurrentHealth { get; }
        IRace Race { get; }
        IArmor Armor { get; }
        IWeapon Weapon { get; }
        IFighterType FighterType { get; }

        public int GetMaxHealth();
        public int CalculateDamage();
        public int CalculateArmor();
        public void TakeDamage(int damage);
        public bool IsAlive();
    }
}