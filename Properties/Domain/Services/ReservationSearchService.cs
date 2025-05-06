using Domain.Entities;
using Domain.Repositories;
using Domain.Services.Common;
using Domain.Services.Interfaces;

namespace Domain.Services;

public class ReservationSearchService : IReservationSearchService
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IPropertiesRepository _propertiesRepository;
    private readonly IRoomTypesRepository _roomTypesRepository;

    public ReservationSearchService(
        IReservationsRepository reservationsRepository,
        IPropertiesRepository propertiesRepository,
        IRoomTypesRepository roomTypesRepository)
    {
        _reservationsRepository = reservationsRepository;
        _propertiesRepository = propertiesRepository;
        _roomTypesRepository = roomTypesRepository;
    }

    public async Task<List<AvailableRoomsByProperty>> SearchAsync(
        string city,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int personCount,
        decimal maxDailyPrice)
    {
        List<Property> properties = await GetPropertiesByCity(city);
        List<Reservation> allReservations = await _reservationsRepository.GetAllAsync();

        var availableRoomsTasks = properties.Select(p =>
            GetAvailableRoomsForProperty(p, allReservations, arrivalDate, departureDate, personCount, maxDailyPrice)
        );

        var availableRoomsResults = await Task.WhenAll(availableRoomsTasks);

        return availableRoomsResults
            .Where(result => result.AvailableRoomTypes.Any())
            .ToList();
    }

    private async Task<AvailableRoomsByProperty> GetAvailableRoomsForProperty(
        Property property,
        List<Reservation> allReservations,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int personCount,
        decimal maxDailyPrice)
    {
        List<RoomType> roomTypesByProperty = await _roomTypesRepository
            .GetByPropertyIdAsync(property.Id);
        
        List<RoomType> eligibleRoomTypes = roomTypesByProperty
        .Where(r => r.DailyPrice <= maxDailyPrice && personCount <= r.MaxPersonCount)
        .ToList();

        List<RoomType> availableRoomTypes = eligibleRoomTypes
            .Where(rt => IsRoomTypeAvailable(rt, property.Id, allReservations, arrivalDate, departureDate))
            .ToList();

        return new AvailableRoomsByProperty(property, availableRoomTypes);
    }
    
    private bool IsRoomTypeAvailable(
        RoomType roomType,
        Guid propertyId,
        List<Reservation> reservations,
        DateOnly arrivalDate,
        DateOnly departureDate)
    {
        return !reservations.Any(r =>
            r.PropertyId == propertyId &&
            r.RoomTypeId == roomType.Id &&
            DatesOverlap(
                DateOnly.FromDateTime(r.ArrivalDateTime),
                DateOnly.FromDateTime(r.DepartureDateTime),
                arrivalDate,
                departureDate));
    }
    
    private static bool DatesOverlap(DateOnly start1, DateOnly end1, DateOnly start2, DateOnly end2)
    {
        return start1 < end2 && start2 < end1;
    }
    
    private async Task<List<Property>> GetPropertiesByCity(string city)
    {
        List<Property> properties = await _propertiesRepository.GetAllAsync();
        return properties
            .Where(pr => pr.City == city)
            .ToList();
    }
}