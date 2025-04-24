namespace PropertiesApi.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Property(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null or empty", nameof(name));
        }
        
        Id = Guid.NewGuid();
        Name = name;
    }
}