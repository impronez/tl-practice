namespace CarFactory.Engines;

public struct AtmosphericEngine : IEngine
{
    public float Speed => 150f;
    public string Name => "Atmospheric Engine";
}