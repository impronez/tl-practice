using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities.RandomService;
using Moq;

namespace FightersTests.Models.FighterTests;

public abstract class FighterTestsBase
{
    private readonly Mock<IRace> _raceMock = new();
    private readonly Mock<IFighterType> _fighterTypeMock = new();
    private readonly Mock<IArmor> _armorMock = new();
    private readonly Mock<IWeapon> _weaponMock = new();
    private readonly Mock<IRandomService> _randomServiceMock = new();

    protected void SetupMocks(
        int raceHealth = 0, int fighterTypeHealth = 0,
        int raceArmor = 0, int armorArmor = 0,
        int raceDamage = 0, int fighterTypeDamage = 0, int weaponDamage = 0,
        float? damageRatio = null, int? critRoll = null)
    {
        _raceMock.Setup(x => x.Health).Returns(raceHealth);
        _fighterTypeMock.Setup(x => x.Health).Returns(fighterTypeHealth);

        _raceMock.Setup(x => x.Armor).Returns(raceArmor);
        _armorMock.Setup(x => x.Armor).Returns(armorArmor);

        _raceMock.Setup(x => x.Damage).Returns(raceDamage);
        _fighterTypeMock.Setup(x => x.Damage).Returns(fighterTypeDamage);
        _weaponMock.Setup(x => x.Damage).Returns(weaponDamage);

        if (damageRatio.HasValue)
        {
            _randomServiceMock
                .Setup(x => x.NextFloat(Fighter.MinDamageRatio, Fighter.MaxDamageRatio))
                .Returns(damageRatio.Value);
        }

        if (critRoll.HasValue)
        {
            _randomServiceMock
                .Setup(x => x.NextInt(1, 40))
                .Returns(critRoll.Value);
        }
    }

    protected Fighter CreateFighter(string name = "Test", int initiative = 1)
    {
        return new Fighter(
            name,
            initiative,
            _raceMock.Object,
            _fighterTypeMock.Object,
            _armorMock.Object,
            _weaponMock.Object,
            _randomServiceMock.Object);
    }
}