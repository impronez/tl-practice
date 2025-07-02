namespace PropertiesApi.Contracts.RoomTypes;

public class RoomTypeResponse
{
    public RoomTypeResponse(
        Guid roomTypeId,
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities)
    {
        Id = roomTypeId;
        PropertyId = propertyId;
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        Services = services;
        Amenities = amenities;
    }

    public Guid Id { get; }
    public Guid PropertyId { get; }
    public string Name { get; }
    public decimal DailyPrice { get; }
    public string Currency { get; }
    public int MinPersonCount { get; }
    public int MaxPersonCount { get; }
    public string Services { get; }
    public string Amenities { get; }
}