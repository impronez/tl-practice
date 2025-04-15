using CarFactory.Bodyworks;
using CarFactory.Colors;
using CarFactory.Engines;
using CarFactory.Transmissons;

namespace CarFactory.Cars;

public interface ICar
{
    public IEngine Engine { get; }
    public ITransmission Transmission { get; }
    public IBodywork Bodywork { get; }
    public Color Color { get; }
    public float MaxSpeed { get; }
    public int GearCount { get; }

    public string GetStringConfiguration();
}