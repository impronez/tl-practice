namespace Fighters.Models.Races
{
    public interface IRace : IHaveName
    {
        public int Damage { get; }
        public int Health { get; }
        public int Armor { get; }
    }
}