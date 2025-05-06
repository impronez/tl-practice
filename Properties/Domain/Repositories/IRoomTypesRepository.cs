using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypesRepository
{
    Task AddAsync(RoomType roomType);
    Task<RoomType?> GetByRoomTypeIdAsync(Guid roomTypeId);
    Task<List<RoomType>> GetByPropertyIdAsync(Guid propertyId);
    Task UpdateAsync(RoomType roomType);
    Task<bool> ExistsByPropertyIdAsync(Guid propertyId);
    Task DeleteByIdAsync(Guid id);
}