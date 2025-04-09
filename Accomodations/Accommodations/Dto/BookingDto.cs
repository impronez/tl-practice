namespace Accommodations.Dto;

public class BookingDto
{
    public static BookingDto Create(string userIdString,
        string category,
        string startDateString,
        string endDateString,
        string currencyString)
    {
        var isCorrectId = int.TryParse(userIdString, out int id);
        if (!isCorrectId)
            throw new ArgumentException("Invalid user id");
        
        var isCorrectStartDate = DateTime.TryParse(startDateString, out DateTime startDate);
        if (!isCorrectStartDate)
            throw new ArgumentException("Invalid start date");
        
        var isCorrectEndDate = DateTime.TryParse(endDateString, out DateTime endDate);
        if (!isCorrectEndDate)
            throw new ArgumentException("Invalid end date");
        
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Empty category");
        
        var isCorrectCurrency = Enum.TryParse(currencyString, out CurrencyDto currency);
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
