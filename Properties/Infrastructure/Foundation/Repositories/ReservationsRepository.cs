using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public ReservationsRepository(HotelManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Reservation reservation)
    {
        await _dbContext.Reservations.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Reservations.FindAsync(id);
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _dbContext.Reservations.ToListAsync();
    }

    public async Task<bool> ExistsByPropertyIdAsync(Guid propertyId)
    {
        return await _dbContext.Reservations
            .AnyAsync(r => r.PropertyId == propertyId);
    }

    public async Task<bool> ExistsByRoomTypeIdAsync(Guid roomTypeId)
    {
        return await _dbContext.Reservations
            .AnyAsync(r => r.RoomTypeId == roomTypeId);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        Reservation? reservation = await GetByIdAsync(id);
        if (reservation is null)
        {
            throw new InvalidOperationException($"Reservation with id '{id}' does not exist");
        }
        
        _dbContext.Reservations.Remove(reservation);
        await _dbContext.SaveChangesAsync();
    }
}