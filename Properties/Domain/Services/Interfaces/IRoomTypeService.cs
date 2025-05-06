using CSharpFunctionalExtensions;
using Domain.Entities;

namespace Domain.Services.Interfaces;

public interface IRoomTypeService
{
    Task<Result<RoomType>> CreateAsync(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currencyString,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities);

    Task<RoomType?> GetByRoomTypeIdAsync(Guid id);

    Task<List<RoomType>> GetByPropertyIdAsync(Guid propertyId);

    Task<Result> UpdateAsync(
        Guid id,
        string name,
        decimal dailyPrice,
        string currencyString,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities);

    Task<Result> DeleteAsync(Guid id);
}