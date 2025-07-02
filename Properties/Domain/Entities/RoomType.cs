using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class RoomType
{
    protected RoomType()
    {
    }

    private RoomType(
        Guid roomTypeId,
        Guid propertyId,
        string name,
        decimal dailyPrice,
        Currency currency,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities)
    {
        Id = roomTypeId;
        PropertyId = propertyId;
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        Services = services;
        Amenities = amenities;
    }

    public Guid Id { get; set; }
    public Guid PropertyId { get; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public string Services { get; set; }
    public string Amenities { get; set; }

    public Result Update(
        string name,
        decimal dailyPrice,
        string currencyString,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities)
    {
        Result validationResult = ValidateFields(name,
            dailyPrice,
            minPersonCount,
            maxPersonCount);

        if (validationResult.IsFailure)
        {
            return Result.Failure<RoomType>(validationResult.Error);
        }

        Result<Currency> currencyResult = Currency.Create(currencyString);
        if (currencyResult.IsFailure)
        {
            return Result.Failure<RoomType>(currencyResult.Error);
        }

        Name = name;
        DailyPrice = dailyPrice;
        Currency = currencyResult.Value;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        Services = services;
        Amenities = amenities;

        return Result.Success();
    }

    public static Result<RoomType> Create(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currencyString,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities)
    {
        Result validationResult = ValidateFields(name,
            dailyPrice,
            minPersonCount,
            maxPersonCount);

        if (validationResult.IsFailure)
        {
            return Result.Failure<RoomType>(validationResult.Error);
        }

        Result<Currency> currencyResult = Currency.Create(currencyString);
        if (currencyResult.IsFailure)
        {
            return Result.Failure<RoomType>(currencyResult.Error);
        }

        return new RoomType(
            roomTypeId: Guid.NewGuid(),
            propertyId: propertyId,
            name,
            dailyPrice,
            currencyResult.Value,
            minPersonCount,
            maxPersonCount,
            services,
            amenities);
    }

    private static Result ValidateFields(
        string name,
        decimal dailyPrice,
        int minPersonCount,
        int maxPersonCount)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<RoomType>("Name cannot be null or empty.");
        }

        if (dailyPrice < 0)
        {
            return Result.Failure<RoomType>($"Daily price [{dailyPrice}] cannot be negative.");
        }

        if (minPersonCount <= 0)
        {
            return Result.Failure<RoomType>($"Min person count [{minPersonCount}] must be greater than 0.");
        }

        if (maxPersonCount <= 0)
        {
            return Result.Failure<RoomType>($"Max person count [{maxPersonCount}] must be greater than 0.");
        }

        if (maxPersonCount < minPersonCount)
        {
            return Result.Failure<RoomType>(
                $"Min person count [{minPersonCount}] cannot be greater than max [{maxPersonCount}].");
        }

        return Result.Success();
    }
}