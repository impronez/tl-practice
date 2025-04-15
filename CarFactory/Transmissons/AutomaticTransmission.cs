namespace CarFactory.Transmissons;

public struct AutomaticTransmission : ITransmission
{
    public float Speed => 300f;
    public int GearCount => 8;
    public string Name => "Automatic Transmission";
}