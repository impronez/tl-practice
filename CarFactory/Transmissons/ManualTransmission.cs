namespace CarFactory.Transmissons;

public struct ManualTransmission : ITransmission
{
    public float Speed => 180f;
    public int GearCount => 5;
    public string Name => "Manual Transmission";
}