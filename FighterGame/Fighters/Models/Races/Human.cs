namespace Fighters.Models.Races;

public struct Human : IRace
{
    public int Damage => 15;
    public int Health => 100;
    public int Armor => 0;
}