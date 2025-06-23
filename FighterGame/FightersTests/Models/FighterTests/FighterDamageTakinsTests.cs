using Fighters.Models.Fighters;

namespace FightersTests.Models.FighterTests;

public class FighterDamageTakingTests : FighterTestsBase
{
    [Fact]
    public void TakeDamage_DamageGreaterThanArmor_HealthReduced()
    {
        // Arrange
        SetupMocks(raceHealth: 100, raceArmor: 5, armorArmor: 3);
        IFighter fighter = CreateFighter();

        // Act
        fighter.TakeDamage(20);

        // Assert
        Assert.Equal(88, fighter.CurrentHealth);
    }

    [Fact]
    public void TakeDamage_DamageLessThanArmor_NoHealthChange()
    {
        // Arrange
        SetupMocks(raceHealth: 50, raceArmor: 6, armorArmor: 5);
        IFighter fighter = CreateFighter();

        // Act
        fighter.TakeDamage(5);

        // Assert
        Assert.Equal(50, fighter.CurrentHealth);
    }

    [Fact]
    public void TakeDamage_DamageEqualsArmor_NoHealthChange()
    {
        // Arrange
        SetupMocks(raceHealth: 30, raceArmor: 3, armorArmor: 2);
        IFighter fighter = CreateFighter();

        // Act
        fighter.TakeDamage(5);

        // Assert
        Assert.Equal(30, fighter.CurrentHealth);
    }
}