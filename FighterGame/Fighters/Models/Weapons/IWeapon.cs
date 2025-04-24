namespace Fighters.Models.Weapons;

public interface IWeapon : IHaveName
{
    public int Damage { get; }
}