namespace CarFactory.Transmissons;

public interface ITransmission
{
    public float Speed { get; }
    public int GearCount { get; }
    public string Name { get; }
}