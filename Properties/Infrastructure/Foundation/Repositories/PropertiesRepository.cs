using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class PropertiesRepository : IPropertiesRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public PropertiesRepository(HotelManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Property property)
    {
        await _dbContext.Properties.AddAsync(property);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _dbContext.Properties.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Properties.FindAsync(id);
    }

    public async Task UpdateAsync(Property property)
    {
        Property? existingProperty = await GetByIdAsync(property.Id);
        if (existingProperty is null)
        {
            throw new InvalidOperationException($"Property with id {property.Id} does not exist");
        }

        existingProperty.Name = property.Name;
        existingProperty.Address = property.Address;
        existingProperty.City = property.City;
        existingProperty.Country = property.Country;
        existingProperty.Latitude = property.Latitude;
        existingProperty.Longitude = property.Longitude;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        Property? existingProperty = await GetByIdAsync(id);
        if (existingProperty is null)
        {
            throw new InvalidOperationException($"Property with id {id} does not exist");
        }

        _dbContext.Properties.Remove(existingProperty);
        await _dbContext.SaveChangesAsync();
    }
}