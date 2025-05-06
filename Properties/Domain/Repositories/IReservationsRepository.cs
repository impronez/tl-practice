using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationsRepository
{
    Task AddAsync(Reservation reservation);
    Task<Reservation?> GetByIdAsync(Guid id);
    Task<List<Reservation>> GetAllAsync();
    Task<bool> ExistsByPropertyIdAsync(Guid propertyId);
    Task<bool> ExistsByRoomTypeIdAsync(Guid roomTypeId);
    Task DeleteByIdAsync(Guid id);
}