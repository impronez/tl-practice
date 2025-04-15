namespace CarFactory.Transmissons;

public struct ContinuouslyVariableTransmission : ITransmission
{
    public float Speed => 250f;
    public int GearCount => 0;
    public string Name => "Continuously Variable Transmission";
}