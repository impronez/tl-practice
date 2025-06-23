using Fighters.Models.Fighters;

namespace FightersTests.Models.FighterTests;

public class FighterAliveTests : FighterTestsBase
{
    [Fact]
    public void IsAlive_HealthAboveZero_ReturnsTrue()
    {
        // Arrange
        SetupMocks(raceHealth: 100, fighterTypeHealth: 100);
        
        // Act
        IFighter fighter = CreateFighter();

        // Assert
        Assert.True(fighter.IsAlive());
    }

    [Fact]
    public void IsAlive_HealthZero_ReturnsFalse()
    {
        // Arrange
        SetupMocks(raceHealth: 0, fighterTypeHealth: 0);
        
        // Act
        IFighter fighter = CreateFighter();

        // Assert
        Assert.False(fighter.IsAlive());
    }
}