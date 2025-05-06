using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class RoomTypesRepository : IRoomTypesRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public RoomTypesRepository(HotelManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(RoomType roomType)
    {
        await _dbContext.RoomTypes.AddAsync(roomType);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RoomType?> GetByRoomTypeIdAsync(Guid roomTypeId)
    {
        return await _dbContext.RoomTypes.FindAsync(roomTypeId);
    }

    public async Task<List<RoomType>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _dbContext.RoomTypes.Where(roomType => roomType.PropertyId == propertyId).ToListAsync();
    }

    public async Task UpdateAsync(RoomType roomType)
    {
        RoomType? existingRoomType = await GetByRoomTypeIdAsync(roomType.Id);
        if (existingRoomType is null)
        {
            throw new InvalidOperationException($"Room type with id {roomType.Id} does not exist");
        }

        existingRoomType.Name = roomType.Name;
        existingRoomType.Amenities = roomType.Amenities;
        existingRoomType.DailyPrice = roomType.DailyPrice;
        existingRoomType.Currency = roomType.Currency;
        existingRoomType.Services = roomType.Services;
        existingRoomType.MinPersonCount = roomType.MinPersonCount;
        existingRoomType.MaxPersonCount = roomType.MaxPersonCount;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsByPropertyIdAsync(Guid propertyId)
    {
        return await _dbContext.RoomTypes
            .AnyAsync(roomType => roomType.PropertyId == propertyId);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        RoomType? existingRoomType = await GetByRoomTypeIdAsync(id);
        if (existingRoomType is null)
        {
            throw new InvalidOperationException($"Room type with id {id} does not exist");
        }

        _dbContext.RoomTypes.Remove(existingRoomType);
        await _dbContext.SaveChangesAsync();
    }
}