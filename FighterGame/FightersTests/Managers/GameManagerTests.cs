using System.Reflection;
using Fighters;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterTypes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utilities.CommandLine;
using Fighters.Utilities.FighterInputProvider;
using Fighters.Utilities.RandomService;
using Moq;

namespace FightersTests.Managers;

public class GameManagerTestsBase
{
    private readonly Mock<ICommandLine> _commandLineMock = new();
    private readonly Mock<IFighterInputProvider> _inputProviderMock = new();
    private readonly Mock<IRandomService> _randomServiceMock = new();

    [Fact]
    public void Run_WithThreeFighters_EndsWithOneWinner()
    {
        // Arrange
        var commands = new Queue<string>(["add", "add", "add", "battle", "exit"]);
        _commandLineMock.Setup(c => c.ReadLine()).Returns(() => commands.Dequeue());

        var names = new Queue<string>(["A", "B", "C"]);
        _inputProviderMock.Setup(p => p.GetName()).Returns(() => names.Dequeue());

        var initiatives = new Queue<int>([1, 2, 2]);
        _inputProviderMock.Setup(p => p.GetInitiative()).Returns(() => initiatives.Dequeue());

        _inputProviderMock.Setup(p => p.GetRace()).Returns(() =>
        {
            var mock = new Mock<IRace>();
            mock.Setup(r => r.Health).Returns(10);
            mock.Setup(r => r.Armor).Returns(0);
            mock.Setup(r => r.Damage).Returns(0);
            return mock.Object;
        });

        _inputProviderMock.Setup(p => p.GetFighterType()).Returns(() =>
        {
            var mock = new Mock<IFighterType>();
            mock.Setup(t => t.Health).Returns(10);
            mock.Setup(t => t.Damage).Returns(5);
            return mock.Object;
        });

        _inputProviderMock.Setup(p => p.GetArmor()).Returns(() =>
        {
            var mock = new Mock<IArmor>();
            mock.Setup(a => a.Armor).Returns(0);
            return mock.Object;
        });

        _inputProviderMock.Setup(p => p.GetWeapon()).Returns(() =>
        {
            var mock = new Mock<IWeapon>();
            mock.Setup(w => w.Damage).Returns(5);
            return mock.Object;
        });

        _randomServiceMock.Setup(r => r.NextFloat(It.IsAny<float>(), It.IsAny<float>())).Returns(1.0f);
        _randomServiceMock.Setup(r => r.NextInt(1, 40)).Returns(0); // без критов

        var gameManager =
            new GameManager(_commandLineMock.Object, _inputProviderMock.Object, _randomServiceMock.Object);

        // Act
        gameManager.Run();

        // Assert
        _commandLineMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Winner:"))), Times.Once);
    }

    [Fact]
    public void Run_WithOnlyOneFighter_NoBattle_AndWarningShown()
    {
        // Arrange
        _inputProviderMock.Setup(p => p.GetName()).Returns("SoloFighter");
        _inputProviderMock.Setup(p => p.GetInitiative()).Returns(1);
        _inputProviderMock.Setup(p => p.GetFighterType()).Returns(Mock.Of<IFighterType>());
        _inputProviderMock.Setup(p => p.GetRace()).Returns(Mock.Of<IRace>());
        _inputProviderMock.Setup(p => p.GetArmor()).Returns(Mock.Of<IArmor>());
        _inputProviderMock.Setup(p => p.GetWeapon()).Returns(Mock.Of<IWeapon>());

        var commandQueue = new Queue<string>(["add", "battle", "exit"]);
        _commandLineMock.Setup(c => c.ReadLine()).Returns(() => commandQueue.Dequeue());

        var gameManager = new GameManager(_commandLineMock.Object, _inputProviderMock.Object, _randomServiceMock.Object);

        gameManager.Run();

        // Assert
        _commandLineMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("must be more than 2"))), Times.Once);
        _commandLineMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Winner"))), Times.Never);
    }
    
    [Fact]
    public void Run_WithExitCommand_ExitsImmediately()
    {
        // Arrange
        var commandQueue = new Queue<string>(["exit"]);
        _commandLineMock.Setup(c => c.ReadLine()).Returns(() => commandQueue.Dequeue());

        var gameManager = new GameManager(_commandLineMock.Object, _inputProviderMock.Object, _randomServiceMock.Object);

        // Act
        gameManager.Run();

        // Assert
        _commandLineMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Welcome"))), Times.Once);
        _commandLineMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Goodbye!"))), Times.Once);
        _commandLineMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Winner"))), Times.Never);
        _inputProviderMock.VerifyNoOtherCalls();
    }
}