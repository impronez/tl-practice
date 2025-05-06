using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IPropertiesRepository _propertiesRepository;
    private readonly IReservationsRepository _reservationsRepository;

    public RoomTypeService(
        IRoomTypesRepository roomTypesRepository,
        IPropertiesRepository propertiesRepository,
        IReservationsRepository reservationsRepository)
    {
        _roomTypesRepository = roomTypesRepository;
        _propertiesRepository = propertiesRepository;
        _reservationsRepository = reservationsRepository;
    }

    public async Task<Result<RoomType>> CreateAsync(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currencyString,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities)
    {
        Property? property = await _propertiesRepository.GetByIdAsync(propertyId);
        if (property is null)
        {
            return Result.Failure<RoomType>($"Property with id '{propertyId}' does not exist");
        }

        Result<RoomType> result = RoomType.Create(
            propertyId,
            name,
            dailyPrice,
            currencyString,
            minPersonCount,
            maxPersonCount,
            services,
            amenities);

        if (result.IsSuccess)
        {
            await _roomTypesRepository.AddAsync(result.Value);
        }

        return result;
    }

    public async Task<RoomType?> GetByRoomTypeIdAsync(Guid id)
    {
        return await _roomTypesRepository.GetByRoomTypeIdAsync(id);
    }

    public async Task<List<RoomType>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _roomTypesRepository.GetByPropertyIdAsync(propertyId);
    }

    public async Task<Result> UpdateAsync(Guid id,
        string name,
        decimal dailyPrice,
        string currencyString,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities)
    {
        RoomType? roomType = await GetByRoomTypeIdAsync(id);
        if (roomType is null)
        {
            return Result.Failure($"Room type with id '{id}' does not exist");
        }

        Result updateResult = roomType.Update(
            name,
            dailyPrice,
            currencyString,
            minPersonCount,
            maxPersonCount,
            services,
            amenities);

        if (updateResult.IsSuccess)
        {
            await _roomTypesRepository.UpdateAsync(roomType);
        }

        return updateResult;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        RoomType? roomType = await _roomTypesRepository.GetByRoomTypeIdAsync(id);
        if (roomType is null)
        {
            return Result.Failure($"Room type with id '{id}' does not exist");
        }
        
        bool isExistsReservations = await _reservationsRepository.ExistsByRoomTypeIdAsync(id);
        if (isExistsReservations)
        {
            return Result.Failure($"Room type with id '{id}' have associated reservations");
        }

        await _roomTypesRepository.DeleteByIdAsync(id);

        return Result.Success();
    }
}