namespace Fighters.Utilities.RandomService;

public interface IRandomService
{
    public int NextInt(int minValue, int maxValue);
    public float NextFloat(float minValue, float maxValue);
}