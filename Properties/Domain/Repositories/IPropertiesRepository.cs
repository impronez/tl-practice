using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertiesRepository
{
    Task AddAsync(Property property);
    Task<List<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(Guid id);
    Task UpdateAsync(Property property);
    Task DeleteByIdAsync(Guid id);
}