namespace Fighters.Models.Races;

public struct Elf : IRace
{
    public int Damage => 8;
    public int Health => 80;
    public int Armor => 3;
    public string Name => "Elf";
}