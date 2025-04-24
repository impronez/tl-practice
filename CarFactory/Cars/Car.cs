using CarFactory.Bodyworks;
using CarFactory.Colors;
using CarFactory.Engines;
using CarFactory.Transmissons;

namespace CarFactory.Cars;

public class Car : ICar
{
    public IEngine Engine { get; }
    public ITransmission Transmission { get; }
    public IBodywork Bodywork { get; }
    public Color Color { get; }

    public Car(IEngine engine,
        ITransmission transmission,
        IBodywork bodywork,
        Color color)
    {
        Engine = engine;
        Transmission = transmission;
        Bodywork = bodywork;
        Color = color;
    }

    public float MaxSpeed => Math.Min(Engine.Speed, Math.Min(Transmission.Speed, Bodywork.Speed));
    public int GearCount => Transmission.GearCount;

    public string GetStringConfiguration()
    {
        return $"Engine: {Engine.Name}\n" +
               $"Transmission: {Transmission.Name}\n" +
               $"Bodywork: {Bodywork.Name}\n" +
               $"Color: {Color.GetType().Name}\n" +
               $"Max speed: {MaxSpeed}\n" +
               $"Gear count: {GearCount}";
    }
}