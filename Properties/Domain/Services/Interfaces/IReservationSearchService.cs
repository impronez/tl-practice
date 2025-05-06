using Domain.Services.Common;

namespace Domain.Services.Interfaces;

public interface IReservationSearchService
{
    Task<List<AvailableRoomsByProperty>> SearchAsync(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int personCount,
        decimal maxDailyPrice);
}