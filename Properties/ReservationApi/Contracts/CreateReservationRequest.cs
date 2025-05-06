namespace ReservationApi.Contracts;

public class CreateReservationRequest
{
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public required string GuestName { get; set; }
    public required string GuestPhoneNumber { get; set; }
    public int PersonCount { get; set; }
}