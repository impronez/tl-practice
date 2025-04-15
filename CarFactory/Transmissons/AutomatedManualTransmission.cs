namespace CarFactory.Transmissons;

public struct AutomatedManualTransmission : ITransmission
{
    public float Speed => 250f;
    public int GearCount => 6;
    public string Name => "Automated Manual Transmission";
}