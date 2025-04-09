namespace Accommodations.Dto;

public class BookingDto
{
    public static BookingDto Create(string userIdString,
        string category,
        string startDateString,
        string endDateString,
        string currencyString)
    {
        var isCorrectId = int.TryParse(userIdString, out var id);
        if (!isCorrectId)
            throw new ArgumentException($"Invalid user id: '{userIdString}. User id must be an integer'");

        var isCorrectStartDate = DateTime.TryParse(startDateString, out var startDate);
        if (!isCorrectStartDate)
            throw new ArgumentException($"Invalid start date: '{startDateString}. Date format: 'MM/DD/YYYY'");

        var isCorrectEndDate = DateTime.TryParse(endDateString, out var endDate);
        if (!isCorrectEndDate)
            throw new ArgumentException($"Invalid end date: '{endDateString}. Date format: 'MM/DD/YYYY'");

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Empty category");

        var isCorrectCurrency = Enum.TryParse(currencyString, true, out CurrencyDto currency);
        if (!isCorrectCurrency)
            throw new ArgumentException("Invalid currency");

        return new BookingDto
        {
            UserId = id,
            StartDate = startDate,
            EndDate = endDate,
            Category = category,
            Currency = currency
        };
    }

    public int UserId { get; private init; }
    public DateTime StartDate { get; private init; }
    public DateTime EndDate { get; private init; }
    public string Category { get; private init; }
    public CurrencyDto Currency { get; private init; }
}