namespace Fighters.Models.FighterTypes;

public struct Mercenary : IFighterType
{
    public string Name => "Mercenary";
    public int Health => 100;
    public int Damage => 3;
}