using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertiesRepository _propertiesRepository;
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IReservationsRepository _reservationsRepository;
    

    public PropertyService(
        IPropertiesRepository propertiesRepository, 
        IRoomTypesRepository roomTypesRepository,
        IReservationsRepository reservationsRepository)
    {
        _propertiesRepository = propertiesRepository;
        _roomTypesRepository = roomTypesRepository;
        _reservationsRepository = reservationsRepository;
    }

    public async Task<Result<Property>> CreateAsync(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        Result<Property> result = Property.Create(
            name,
            country,
            city,
            address,
            latitude,
            longitude);

        if (result.IsSuccess)
        {
            await _propertiesRepository.AddAsync(result.Value);
        }

        return result;
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _propertiesRepository.GetAllAsync();
    }

    public async Task<Property?> GetByIdAsync(Guid id)
    {
        return await _propertiesRepository.GetByIdAsync(id);
    }

    public async Task<Result> UpdateAsync(
        Guid id,
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        Property? property = await _propertiesRepository.GetByIdAsync(id);
        if (property is null)
        {
            return Result.Failure($"Property with id '{id}' does not exist");
        }

        Result updateResult = property.Update(
            name,
            country,
            city,
            address,
            latitude,
            longitude);

        if (updateResult.IsSuccess)
        {
            await _propertiesRepository.UpdateAsync(property);
        }

        return updateResult;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        Property? property = await _propertiesRepository.GetByIdAsync(id);
        if (property is null)
        {
            return Result.Failure($"Property with id '{id}' does not exist");
        }

        bool isExistsRoomTypes = await _roomTypesRepository.ExistsByPropertyIdAsync(id);
        if (isExistsRoomTypes)
        {
            return Result.Failure($"Property with id '{id}' have associated room types");
        }
        
        bool isExistsReservations = await _reservationsRepository.ExistsByPropertyIdAsync(id);
        if (isExistsReservations)
        {
            return Result.Failure($"Property with id '{id}' have associated reservations");
        }

        await _propertiesRepository.DeleteByIdAsync(id);

        return Result.Success();
    }
}