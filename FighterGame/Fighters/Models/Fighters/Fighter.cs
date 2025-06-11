using Fighters.Models.Armors;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities.RandomService;

namespace Fighters.Models.Fighters;

public class Fighter : IFighter
{
    public const float MinDamageRatio = -0.2f;
    public const float MaxDamageRatio = 0.1f;

    private IRandomService _randomService;

    public Fighter(string name,
        int initiative,
        IRace race,
        IFighterType fighterType,
        IArmor armor,
        IWeapon weapon,
        IRandomService randomService)
    {
        _randomService = randomService;

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
        int damage = Race.Damage + FighterType.Damage + Weapon.Damage;
        if (damage <= 0)
        {
            return 0;
        }

        float ratio = 1f + _randomService.NextFloat(MinDamageRatio, MaxDamageRatio);

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

    public string GetStats()
    {
        var stats = $"Name: {Name}\n";
        stats += $"Fighter type: {FighterType.Name}";
        stats += $"Health: {GetMaxHealth()}\n";
        stats += $"Armor: {CalculateArmor()}\n";
        stats += $"Race: {Race.Name}\n";
        stats += $"Weapon: {Weapon.Name}\n";

        return stats;
    }

    private bool IsCriticalDamage()
    {
        int value = _randomService.NextInt(1, 40);

        return value is >= 30 and <= 40;
    }
}