namespace Fighters.Models.FighterTypes;

public struct Knight : IFighterType
{
    public string Name => "Knight";
    public int Health => 90;
    public int Damage => 5;
}