using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Reservation
{
    protected Reservation()
    {
    }

    private Reservation(
        Guid reservationId,
        Guid propertyId,
        Guid roomTypeId,
        DateTime arrivalDateTime,
        DateTime departureDateTime,
        string guestName,
        string guestPhoneNumber,
        decimal total,
        Currency currency)
    {
        Id = reservationId;
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        ArrivalDateTime = arrivalDateTime;
        DepartureDateTime = departureDateTime;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
        Total = total;
        Currency = currency;
    }

    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }
    public decimal Total { get; set; }
    public Currency Currency { get; set; }

    public static Result<Reservation> Create(
        Guid propertyId,
        Guid roomTypeId,
        DateTime arrivalDateTime,
        DateTime departureDateTime,
        string guestName,
        string guestPhoneNumber,
        decimal roomTypePrice,
        Currency currency)
    {
        Result validationResult = ValidateFields(
            arrivalDateTime,
            departureDateTime,
            guestName,
            guestPhoneNumber);

        if (validationResult.IsFailure)
        {
            return Result.Failure<Reservation>(validationResult.Error);
        }

        decimal total = CalculateTotalPrice(arrivalDateTime,
            departureDateTime,
            roomTypePrice);

        return new Reservation(
            Guid.NewGuid(),
            propertyId,
            roomTypeId,
            arrivalDateTime,
            departureDateTime,
            guestName,
            guestPhoneNumber,
            total,
            currency
        );
    }

    private static decimal CalculateTotalPrice(
        DateTime arrivalDateTime,
        DateTime departureDateTime,
        decimal dailyPrice)
    {
        int fullDays = (departureDateTime.Date - arrivalDateTime.Date).Days;

        double extraHours = (departureDateTime - arrivalDateTime).TotalHours - (fullDays * 24);
        decimal extraCharge = extraHours > 0
            ? (dailyPrice / 24m) * (decimal)extraHours
            : 0m;

        return (dailyPrice * fullDays) + extraCharge;
    }

    private static Result ValidateFields(
        DateTime arrivalDateTime,
        DateTime departureDateTime,
        string guestName,
        string guestPhoneNumber)
    {
        if (DateOnly.FromDateTime(departureDateTime) <= DateOnly.FromDateTime(arrivalDateTime))
        {
            return Result.Failure<Reservation>(
                $"Departure date [{departureDateTime}] cannot be earlier or equal than arrival date [{arrivalDateTime}].");
        }

        if (DateOnly.FromDateTime(arrivalDateTime) < DateOnly.FromDateTime(DateTime.Now))
        {
            return Result.Failure<Reservation>(
                $"Arrival date [{arrivalDateTime}] cannot be earlier than current date.");
        }

        if (string.IsNullOrWhiteSpace(guestName))
        {
            return Result.Failure<Reservation>("Guest name cannot be empty or null.");
        }

        if (string.IsNullOrWhiteSpace(guestPhoneNumber))
        {
            return Result.Failure<Reservation>("Guest phone number cannot be empty or null.");
        }

        return Result.Success();
    }
}