namespace Fighters.Models.FighterTypes;

public interface IFighterType : IHaveName
{
    public int Health { get; }
    public int Damage { get; }
}