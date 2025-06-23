using Fighters.Utilities.RandomService;

namespace FightersTests.Utilities.RandomServiceTests;

public class RandomServiceTests
{
    private readonly RandomService _randomService;

    public RandomServiceTests()
    {
        _randomService = new RandomService();
    }
    
    [Fact]
    public void NextInt_ValidRange_ReturnsValueWithinRange()
    {
        // Arrange
        int minValue = 1;
        int exclusiveMaxValue = 4;
        
        // Act
        int result = _randomService.NextInt(minValue, exclusiveMaxValue + 1);

        // Assert
        Assert.InRange(result, minValue, exclusiveMaxValue);
    }
    
    [Fact]
    public void NextInt_SameMinAndMax_ReturnsMinValue()
    {
        // Assert
        int value = 3;
        
        // Act
        int result = _randomService.NextInt(value, value);
        
        // Act
        Assert.Equal(value, result);
    }

    [Fact]
    public void NextInt_MinGreaterThanMax_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _randomService.NextInt(10, 5));
    }

    [Fact]
    public void NextFloat_ValidRange_ReturnsValueWithinRange()
    {
        // Act
        float result = _randomService.NextFloat(0.5f, 1.0f);

        // Assert
        Assert.InRange(result, 0.5f, 1.0f);
    }

    [Fact]
    public void NextFloat_MinGreaterThanMax_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _randomService.NextFloat(1.0f, 0.5f));
    }

    [Fact]
    public void NextFloat_SameMinAndMax_ReturnsSameValue()
    {
        // Act
        float result = _randomService.NextFloat(2.5f, 2.5f);

        // Assert
        Assert.Equal(2.5f, result);
    }
}