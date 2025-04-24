using Properties.Domain.Entities;
using Properties.Domain.Repositories;

namespace Properties.Infrastructure.Repositories;

public class PropertiesRepository : IPropertiesRepository
{
    private static readonly List<Property> Properties = new();
    
    public void Add(Property property)
    {
        Properties.Add(property);
    }

    public IEnumerable<Property> GetAll()
    {
        return Properties.ToList();
    }

    public Property? GetById(Guid id)
    {
        return Properties.FirstOrDefault(p => p.Id == id);
    }

    public void Update(Property property)
    {
        Property? existingProperty = GetById(property.Id);
        if (existingProperty is null)
        {
            throw new InvalidOperationException($"Property with id [{property.Id}] not found");
        }
        
        existingProperty.Name = property.Name;
    }

    public void Delete(Guid id)
    {
        Property? existingProperty = Properties.FirstOrDefault(p => p.Id == id);
        if (existingProperty is null)
        {
            throw new InvalidOperationException($"Property with id [{id}] not found");
        }
        
        Properties.Remove(existingProperty);
    }
}