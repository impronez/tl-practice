using Accommodations.Commands;

namespace Accommodations;

public class CommandHistory
{
    private Dictionary<int, ICommand> _executedCommands = new();
    private int _commandIndex = 0;

    public void AddCommmand(ICommand command)
    {
        _executedCommands.Add(++_commandIndex, command);
    }

    public bool TryGetLastCommand(out ICommand command)
    {
        return _executedCommands.TryGetValue(_commandIndex, out command);
    }

    public void RemoveLastCommand()
    {
        if (_commandIndex > 0)
        {
            _executedCommands.Remove(_commandIndex--);
        }
    }
}