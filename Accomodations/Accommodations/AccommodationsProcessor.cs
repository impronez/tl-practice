using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor
{
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new();
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine("Booking Command Line Interface");
        Console.WriteLine("Commands:");
        Console.WriteLine("'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room");
        Console.WriteLine("'cancel <BookingId>' - to cancel a booking");
        Console.WriteLine("'undo' - to undo the last command");
        Console.WriteLine("'find <BookingId>' - to find a booking by ID");
        Console.WriteLine("'search <StartDate> <EndDate> <CategoryName>' - to search bookings");
        Console.WriteLine("'exit' - to exit the application");

        string input;
        while ((input = Console.ReadLine()) != "exit")
        {
            try
            {
                ProcessCommand(input);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static void ProcessCommand(string input)
    {
        var parts = input.Split(' ');
        var commandName = parts[0];

        switch (commandName)
        {
            case "book":
                ProcessBookCommand(parts);
                break;
            case "cancel":
                ProcessCancelCommand(parts);
                break;
            case "undo":
                ProcessUndoCommand();
                break;
            case "find":
                ProcessFindCommand(parts);
                break;
            case "search":
                ProcessSearchCommand(parts);
                break;
            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }

    private static void ProcessBookCommand(string[] parts)
    {
        if (parts.Length != 6)
        {
            Console.WriteLine("Invalid number of arguments for booking.");
            return;
        }

        var bookingDto = BookingDto.Create(parts[1], parts[2], parts[3], parts[4], parts[5]);

        BookCommand bookCommand = new(_bookingService, bookingDto);
        bookCommand.Execute();
        _executedCommands.Add(++s_commandIndex, bookCommand);
        Console.WriteLine("Booking command run is successful.");
    }

    private static void ProcessCancelCommand(string[] parts)
    {
        if (parts.Length != 2)
        {
            Console.WriteLine("Invalid number of arguments for canceling.");
            return;
        }

        var bookingId = Guid.Parse(parts[1]);
        CancelBookingCommand cancelCommand = new(_bookingService, bookingId);
        cancelCommand.Execute();
        _executedCommands.Add(++s_commandIndex, cancelCommand);
        Console.WriteLine("Cancellation command run is successful.");
    }

    private static void ProcessUndoCommand()
    {
        if (!_executedCommands.TryGetValue(s_commandIndex, out var value))
        {
            Console.WriteLine("There are no executed commands.");
            return;
        }

        value.Undo();
        _executedCommands.Remove(s_commandIndex);
        s_commandIndex--;
        Console.WriteLine("Last command undone.");
    }

    private static void ProcessFindCommand(string[] parts)
    {
        if (parts.Length != 2)
        {
            Console.WriteLine("Invalid arguments for 'find'. Expected format: 'find <BookingId>'");
            return;
        }

        var id = Guid.Parse(parts[1]);
        FindBookingByIdCommand findCommand = new(_bookingService, id);
        findCommand.Execute();
    }

    private static void ProcessSearchCommand(string[] parts)
    {
        if (parts.Length != 4)
        {
            Console.WriteLine(
                "Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'");
            return;
        }

        var startDate = DateTime.Parse(parts[1]);
        var endDate = DateTime.Parse(parts[2]);
        var categoryName = parts[3];
        SearchBookingsCommand searchCommand = new(_bookingService, startDate, endDate, categoryName);
        searchCommand.Execute();
    }
}