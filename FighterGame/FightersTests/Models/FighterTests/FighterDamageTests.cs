using Fighters.Models.Fighters;

namespace FightersTests.Models.FighterTests;

public class FighterDamageTests : FighterTestsBase
{
    [Fact]
    public void CalculateDamage_DamageIsZero_ReturnsZero()
    {
        // Arrange
        SetupMocks();
        IFighter fighter = CreateFighter();

        // Act
        int damage = fighter.CalculateDamage();

        // Assert
        Assert.Equal(0, damage);
    }

    [Fact]
    public void CalculateDamage_DamageIsNotZero_ReturnsSumOfDamage()
    {
        // Arrange
        SetupMocks(raceDamage: 1, fighterTypeDamage: 3, weaponDamage: 5,
            damageRatio: Fighter.MaxDamageRatio, critRoll: 1);

        // Act
        IFighter fighter = CreateFighter();

        // Assert
        Assert.Equal(9, fighter.CalculateDamage());
    }

    [Fact]
    public void CalculateDamage_WithCriticalDamage_ReturnsDamageWithCriticalDamage()
    {
        // Arrange
        SetupMocks(raceDamage: 1, fighterTypeDamage: 3, weaponDamage: 5,
            damageRatio: Fighter.MaxDamageRatio, critRoll: 40);

        // Act
        IFighter fighter = CreateFighter();

        // Assert
        Assert.Equal(19, fighter.CalculateDamage());
    }
}