using CSharpFunctionalExtensions;

namespace Domain.Entities;

public class Property
{
    protected Property()
    {
    }

    private Property(
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

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Result Update(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        Result validationResult = ValidateFields(name, country, city, address, latitude, longitude);
        if (validationResult.IsFailure)
        {
            return validationResult.ConvertFailure<Property>();
        }

        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;

        return Result.Success();
    }

    public static Result<Property> Create(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        Result validationResult = ValidateFields(name, country, city, address, latitude, longitude);
        if (validationResult.IsFailure)
        {
            return validationResult.ConvertFailure<Property>();
        }

        return new Property(
            Guid.NewGuid(),
            name,
            country,
            city,
            address,
            latitude,
            longitude);
    }

    private static Result ValidateFields(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Property>("Name cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            return Result.Failure<Property>("Country cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            return Result.Failure<Property>("City cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            return Result.Failure<Property>("Address cannot be null or empty.");
        }

        if (latitude is < 0 or > 90)
        {
            return Result.Failure<Property>($"Latitude must be between 0 and 90. Current: [{latitude}]");
        }

        if (longitude is < 0 or > 180)
        {
            return Result.Failure<Property>($"Longitude must be between 0 and 190. Current: [{longitude}]");
        }

        return Result.Success();
    }
}