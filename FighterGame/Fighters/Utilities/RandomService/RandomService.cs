namespace Fighters.Utilities.RandomService;

public class RandomService : IRandomService
{
    public int NextInt(int minValue, int maxValue)
    {
        if (minValue > maxValue)
        {
            throw new ArgumentException($"Min value [{minValue}] must be less than max value [{maxValue}]]");
        }

        return Random.Shared.Next(minValue, maxValue);
    }

    public float NextFloat(float minValue, float maxValue)
    {
        if (minValue > maxValue)
        {
            throw new ArgumentException($"Min value [{minValue}] must be less than max value [{maxValue}]]");
        }

        float range = maxValue - minValue;

        return minValue + (float)Random.Shared.NextDouble() * range;
    }
}