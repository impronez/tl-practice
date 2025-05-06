namespace ReservationApi.Contracts;

public class ReservationResponse
{
    public ReservationResponse(
        Guid reservationId,
        Guid propertyId,
        Guid roomTypeId,
        DateTime arrivalDateTime,
        DateTime departureDateTime,
        string guestName,
        string guestPhoneNumber,
        decimal total,
        string currency)
    {
        Id = reservationId;
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        ArrivalDateTime = arrivalDateTime;
        DepartureDateTime = departureDateTime;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
        Total = total;
        Currency = currency;
    }

    public Guid Id { get; }
    public Guid PropertyId { get; }
    public Guid RoomTypeId { get; }
    public DateTime ArrivalDateTime { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; }
}