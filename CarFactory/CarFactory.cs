using CarFactory.Bodyworks;
using CarFactory.Cars;
using CarFactory.Colors;
using CarFactory.Engines;
using CarFactory.Transmissons;

namespace CarFactory;

public static class CarFactory
{
    public static ICar Create(IEngine engine,
        ITransmission transmission,
        IBodywork bodywork,
        Color color)
    {
        return new Car(engine, transmission, bodywork, color);
    }
}