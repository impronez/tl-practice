using Fighters.Models.Armors;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Fighter : IFighter
{
    private const int MinDamageRatioInPercent = -20;
    private const int MaxDamageRatioInPercent = 10;

    public Fighter(string name,
        int initiative,
        IRace race,
        IFighterType fighterType,
        IArmor armor,
        IWeapon weapon)
    {
        Name = name;
        Initiative = initiative;
        Race = race;
        FighterType = fighterType;
        Armor = armor;
        Weapon = weapon;
        CurrentHealth = GetMaxHealth();
    }

    public string Name { get; }
    public int Initiative { get; }
    public int CurrentHealth { get; private set; }
    public IRace Race { get; }
    public IArmor Armor { get; }
    public IWeapon Weapon { get; }
    public IFighterType FighterType { get; }

    public int GetMaxHealth() => Race.Health + FighterType.Health;

    public int CalculateDamage()
    {
        var damage = Race.Damage + FighterType.Damage + Weapon.Damage;
        var ratio = 1f + Random.Shared.Next(MinDamageRatioInPercent, MaxDamageRatioInPercent) / 100f;
        if (IsCriticalDamage())
        {
            damage *= 2;
        }

        return (int)(damage * ratio);
    }

    public int CalculateArmor() => Race.Armor + Armor.Armor;

    public void TakeDamage(int damage)
    {
        CurrentHealth -= Math.Max(damage - CalculateArmor(), 0);
    }

    public bool IsAlive() => CurrentHealth > 0;

    private static bool IsCriticalDamage()
    {
        var random = Random.Shared.Next(1, 50);
        return random is > 30 and < 40;
    }
}