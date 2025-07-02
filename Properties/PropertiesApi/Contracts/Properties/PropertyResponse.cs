namespace PropertiesApi.Contracts.Properties;

public class PropertyResponse
{
    public PropertyResponse(
        Guid id,
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Country { get; }
    public string City { get; }
    public string Address { get; }
    public double Latitude { get; }
    public double Longitude { get; }
}