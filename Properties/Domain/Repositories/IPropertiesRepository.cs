using Properties.Domain.Entities;

namespace Properties.Domain.Repositories;

public interface IPropertiesRepository
{
    public void Add(Property property);
    public IEnumerable<Property> GetAll();
    public Property? GetById(Guid id);
    public void Update(Property property);
    public void Delete(Guid id);
}