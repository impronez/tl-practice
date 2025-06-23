using Fighters.Models.Fighters;

namespace FightersTests.Models.FighterTests;

public class FighterArmorTests : FighterTestsBase
{
    [Fact]
    public void CalculateArmor_ValidArmor_ReturnsSum()
    {
        // Arrange
        SetupMocks(raceArmor: 10, armorArmor: 20);
        IFighter fighter = CreateFighter();

        // Act
        int armor = fighter.CalculateArmor();

        // Assert
        Assert.Equal(30, armor);
    }
}