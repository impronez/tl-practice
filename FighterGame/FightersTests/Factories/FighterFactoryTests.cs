using Fighters;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities.RandomService;
using Moq;

namespace FightersTests.Factories;

public class FighterFactoryTests
{
    private readonly string _name;
    private readonly int _initiative;
    private readonly Mock<IRace> _raceMock;
    private readonly Mock<IFighterType> _fighterTypeMock;
    private readonly Mock<IArmor> _armorMock;
    private readonly Mock<IWeapon> _weaponMock;
    private readonly Mock<IRandomService> _randomServiceMock; 
    
    public FighterFactoryTests()
    {
        _name = "Lemon";
        _initiative = 1;
        _raceMock = new Mock<IRace>();
        _fighterTypeMock = new Mock<IFighterType>();
        _armorMock = new Mock<IArmor>();
        _weaponMock = new Mock<IWeapon>();
        _randomServiceMock = new Mock<IRandomService>();
    }

    [Fact]
    public void CreateFighter_ValidArguments_ReturnsFighter()
    {
        // Act
        IFighter fighter = FighterFactory.CreateFighter(
            _name,
            _initiative,
            _raceMock.Object,
            _fighterTypeMock.Object,
            _armorMock.Object,
            _weaponMock.Object,
            _randomServiceMock.Object);
        
        // Arrange
        Assert.NotNull(fighter);
        Assert.IsType<Fighter>(fighter);
        Assert.Equal(_name, fighter.Name);
        Assert.Equal(_initiative, fighter.Initiative);
    }
    
    [Fact]
    public void CreateFighter_EmptyName_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            FighterFactory.CreateFighter(
                "",
                _initiative,
                _raceMock.Object,
                _fighterTypeMock.Object,
                _armorMock.Object,
                _weaponMock.Object,
                null!));
    }
    
    [Fact]
    public void CreateFighter_InitiativeIsMinAcceptable_ReturnsFighter()
    {
        // Arrange
        int initiative = IFighter.MinInitiative;
        
        // Act
        IFighter fighter = FighterFactory.CreateFighter(
            _name,
            initiative,
            _raceMock.Object,
            _fighterTypeMock.Object,
            _armorMock.Object,
            _weaponMock.Object,
            _randomServiceMock.Object);
        
        // Assert
        Assert.Equal(initiative, fighter.Initiative);
    }
    
    [Fact]
    public void CreateFighter_InitiativeIsMaxAcceptable_ReturnsFighter()
    {
        // Arrange
        int initiative = IFighter.MaxInitiative;
        
        // Act
        IFighter fighter = FighterFactory.CreateFighter(
            _name,
            initiative,
            _raceMock.Object,
            _fighterTypeMock.Object,
            _armorMock.Object,
            _weaponMock.Object,
            _randomServiceMock.Object);
        
        // Assert
        Assert.Equal(initiative, fighter.Initiative);
    }
    
    [Fact]
    public void CreateFighter_InitiativeLessMin_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int initiative = IFighter.MinInitiative - 1;
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            FighterFactory.CreateFighter(
                _name,
                initiative,
                _raceMock.Object,
                _fighterTypeMock.Object,
                _armorMock.Object,
                _weaponMock.Object,
                null!));
    }
    
    [Fact]
    public void CreateFighter_InitiativeMoreMax_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int initiative = IFighter.MaxInitiative + 1;
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            FighterFactory.CreateFighter(
                _name,
                initiative,
                _raceMock.Object,
                _fighterTypeMock.Object,
                _armorMock.Object,
                _weaponMock.Object,
                null!));
    }
}