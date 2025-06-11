using Fighters.Models.Fighters;

namespace FightersTests.Models.FighterTests;

public class FighterHealthTests : FighterTestsBase
{
    [Fact]
    public void GetMaxHealth_ValidRaceAndFighterType_ReturnsSum()
    {
        // Arrange
        SetupMocks(raceHealth: 100, fighterTypeHealth: 100);
        IFighter fighter = CreateFighter();

        // Act
        int maxHealth = fighter.GetMaxHealth();

        // Assert
        Assert.Equal(200, maxHealth);
    }
}