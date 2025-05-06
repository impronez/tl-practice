using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services.Common;
using Domain.Services.Interfaces;

namespace Domain.Services;

public class ReservationsService : IReservationsService
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IPropertiesRepository _propertiesRepository;
    private readonly IRoomTypesRepository _roomTypesRepository;
    
    private readonly IReservationSearchService _searchService;

    public ReservationsService(
        IReservationsRepository reservationsRepository,
        IPropertiesRepository propertiesRepository,
        IRoomTypesRepository roomTypesRepository,
        IReservationSearchService searchService)
    {
        _reservationsRepository = reservationsRepository;
        _propertiesRepository = propertiesRepository;
        _roomTypesRepository = roomTypesRepository;
        _searchService = searchService;
    }

    public async Task<Result<Reservation>> CreateAsync(
        Guid propertyId,
        Guid roomTypeId,
        DateTime arrivalDateTime,
        DateTime departureDateTime,
        string guestName,
        string guestPhoneNumber,
        int personCount)
    {
        Property? property = await _propertiesRepository.GetByIdAsync(propertyId);
        RoomType? roomType = await _roomTypesRepository.GetByRoomTypeIdAsync(roomTypeId);

        if (property is null)
        {
            return Result.Failure<Reservation>($"Property with id '{propertyId}' does not exist");
        }

        if (roomType is null)
        {
            return Result.Failure<Reservation>($"Room type with id '{roomTypeId}' does not exist");
        }

        bool isPropertyContainsRoomType = await IsPropertyContainsRoomType(propertyId, roomType); 
        if (!isPropertyContainsRoomType)
        {
            return Result.Failure<Reservation>(
                $"Room type with id '{roomTypeId}' does not pertain to property with id '{propertyId}'");
        }

        if (personCount < 1)
        {
            return Result.Failure<Reservation>($"Person count must be greater than zero");
        }

        if (personCount > roomType.MaxPersonCount)
        {
            return Result.Failure<Reservation>(
                $"Person count [{personCount}] is more than max room type count [{roomType.MaxPersonCount}]'");
        }

        bool isRoomReserved = await IsRoomReserved(roomTypeId, arrivalDateTime, departureDateTime);
        if (isRoomReserved)
        {
            return Result.Failure<Reservation>($"Room type with id '{roomTypeId}' already reserved");
        }

        Result<Reservation> reservationResult = Reservation.Create(
            propertyId,
            roomTypeId,
            arrivalDateTime,
            departureDateTime,
            guestName,
            guestPhoneNumber,
            roomType.DailyPrice,
            roomType.Currency);

        if (reservationResult.IsSuccess)
        {
            await _reservationsRepository.AddAsync(reservationResult.Value);
        }

        return reservationResult;
    }

    public async Task<List<Reservation>> GetFilteredAsync(
        Guid? propertyId,
        Guid? roomTypeId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        string? guestPhoneNumber,
        decimal? minTotal,
        decimal? maxTotal)
    {
        List<Reservation> reservations = await _reservationsRepository.GetAllAsync();

        if (propertyId.HasValue)
        {
            reservations = reservations.Where(r => r.PropertyId == propertyId.Value).ToList();
        }

        if (roomTypeId.HasValue)
        {
            reservations = reservations.Where(r => r.RoomTypeId == roomTypeId.Value).ToList();
        }

        if (arrivalDate.HasValue)
        {
            reservations = reservations.Where(r => DateOnly.FromDateTime(r.ArrivalDateTime) == arrivalDate).ToList();
        }

        if (departureDate.HasValue)
        {
            reservations = reservations.Where(r => DateOnly.FromDateTime(r.DepartureDateTime) == departureDate)
                .ToList();
        }

        if (guestName is not null)
        {
            reservations = reservations.Where(r => r.GuestName == guestName).ToList();
        }

        if (guestPhoneNumber is not null)
        {
            reservations = reservations.Where(r => r.GuestPhoneNumber == guestPhoneNumber).ToList();
        }

        if (minTotal.HasValue)
        {
            reservations = reservations.Where(r => r.Total >= minTotal).ToList();
        }

        if (maxTotal.HasValue)
        {
            reservations = reservations.Where(r => r.Total <= maxTotal).ToList();
        }

        return reservations;
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _reservationsRepository.GetByIdAsync(id);
    }

    public async Task<Result> DeleteByIdAsync(Guid id)
    {
        Reservation? reservation = await GetByIdAsync(id);
        if (reservation is null)
        {
            return Result.Failure($"Reservation with id '{id}' does not exist");
        }

        await _reservationsRepository.DeleteByIdAsync(id);

        return Result.Success();
    }

    public async Task<List<AvailableRoomsByProperty>> SearchAsync(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int personCount,
        decimal maxDailyPrice)
    {
        return await _searchService.SearchAsync(
            city,
            arrivalDate,
            departureDate,
            personCount,
            maxDailyPrice);
    }
    
    private async Task<bool> IsPropertyContainsRoomType(Guid propertyId, RoomType roomType)
    {
        List<RoomType> propertyRoomTypes = await _roomTypesRepository.GetByPropertyIdAsync(propertyId);
        return propertyRoomTypes.Contains(roomType);
    }

    private async Task<bool> IsRoomReserved(Guid roomTypeId, DateTime arrivalDate, DateTime departureDate)
    {
        List<Reservation> reservations = await _reservationsRepository.GetAllAsync();
        
        bool isReserved = reservations
            .Any(r =>
                r.RoomTypeId == roomTypeId &&
                arrivalDate < r.DepartureDateTime && departureDate > r.ArrivalDateTime);

        return isReserved;
    }
}