using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Utilities.CommandLine;
using Fighters.Utilities.FighterInputProvider;
using Moq;

namespace FightersTests.Utilities.FighterInputProvider;

public class ConsoleFighterInputProviderTests
{
    private readonly ConsoleFighterInputProvider _consoleFighterInputProvider;
    private readonly Mock<ICommandLine> _commandLineMock;

    public ConsoleFighterInputProviderTests()
    {
        _commandLineMock = new Mock<ICommandLine>();
        _consoleFighterInputProvider = new ConsoleFighterInputProvider(_commandLineMock.Object);
    }

    [Fact]
    public void GetName_WithInvalidThenValidIn0ReturnsValidName()
    {
        // Arrange
        Queue<string> inputs = new(["", "  ", "John"]);
        _commandLineMock.Setup(u => u.ReadLine()).Returns(() => inputs.Dequeue());

        // Act
        string name = _consoleFighterInputProvider.GetName();

        // Assert
        Assert.Equal("John", name);
        _commandLineMock.Verify(u => u.Write("Enter fighter's name: "), Times.Exactly(3));
        _commandLineMock.Verify(u => u.WriteLine("Invalid value! Name can't be empty."), Times.Exactly(2));
    }

    [Fact]
    public void GetInitiative_WithInvalidThenValidInput_ReturnsValidInitiative()
    {
        // Arrange
        var inputs = new Queue<string?>(["a", "1000", "1"]);
        _commandLineMock.Setup(u => u.ReadLine()).Returns(() => inputs.Dequeue());

        // Act
        int initiative = _consoleFighterInputProvider.GetInitiative();

        // Assert
        Assert.InRange(initiative, IFighter.MinInitiative,
            IFighter.MaxInitiative);
        Assert.Equal(1, initiative);

        _commandLineMock.Verify(u => u.Write(It.Is<string>(s => s.Contains("Invalid value"))), Times.Exactly(2));
        _commandLineMock.Verify(
            u => u.Write(It.Is<string>(s =>
                s.Contains($"Enter initiative [{IFighter.MinInitiative}..{IFighter.MaxInitiative}]:"))),
            Times.Exactly(3));
    }

    [Fact]
    public void GetRace_WithInvalidThenValidInput_ReturnsValidRace()
    {
        // Arrange
        var inputs = new Queue<string?>(["b", "1000", "0"]);
        _commandLineMock.Setup(u => u.ReadLine()).Returns(() => inputs.Dequeue());

        // Act
        IRace race = _consoleFighterInputProvider.GetRace();

        // Assert
        Assert.NotNull(race);

        _commandLineMock.Verify(u => u.Write(It.Is<string>(s => s.Contains("Invalid value"))), Times.Exactly(2));
        _commandLineMock.Verify(
            u => u.Write(It.Is<string>(s =>
                s.Contains($"Enter race index: "))),
            Times.Exactly(3));
    }
}