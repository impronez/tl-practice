namespace Fighters.Models.FighterTypes;

public interface IFighterType
{
    public string Name { get; }
    public int Health { get; }
    public int Damage { get; }
}