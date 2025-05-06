using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Services.Common;

namespace Domain.Services.Interfaces;

public interface IReservationsService
{
    Task<Result<Reservation>> CreateAsync(Guid propertyId,
        Guid roomTypeId,
        DateTime arrivalDateTime,
        DateTime departureDateTime,
        string guestName,
        string guestPhoneNumber,
        int personCount);

    Task<List<Reservation>> GetFilteredAsync(
        Guid? propertyId,
        Guid? roomTypeId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        string? guestPhoneNumber,
        decimal? minTotal,
        decimal? maxTotal);

    Task<Reservation?> GetByIdAsync(Guid id);
    
    Task<Result> DeleteByIdAsync(Guid id);

    Task<List<AvailableRoomsByProperty>> SearchAsync(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int personCount,
        decimal maxDailyPrice);
}