using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Services.Common;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Contracts;

namespace ReservationApi.Controllers;

[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsService _reservationsService;

    public ReservationsController(IReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }

    [HttpPost("api/reservations")]
    public async Task<IActionResult> Create([FromBody] CreateReservationRequest createReservationRequest)
    {
        Result<Reservation> reservationResult = await _reservationsService.CreateAsync(
            createReservationRequest.PropertyId,
            createReservationRequest.RoomTypeId,
            createReservationRequest.ArrivalDateTime,
            createReservationRequest.DepartureDateTime,
            createReservationRequest.GuestName,
            createReservationRequest.GuestPhoneNumber,
            createReservationRequest.PersonCount);

        if (reservationResult.IsFailure)
        {
            return BadRequest(reservationResult.Error);
        }

        return Created("", reservationResult.Value.Id);
    }

    [HttpGet("api/reservations/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Reservation? reservation = await _reservationsService.GetByIdAsync(id);
        if (reservation is null)
        {
            return NotFound($"Reservation with id {id} not found");
        }

        ReservationResponse reservationResponse = new ReservationResponse(
            reservation.Id,
            reservation.PropertyId,
            reservation.RoomTypeId,
            reservation.ArrivalDateTime,
            reservation.DepartureDateTime,
            reservation.GuestName,
            reservation.GuestPhoneNumber,
            reservation.Total,
            reservation.Currency.Value);

        return Ok(reservationResponse);
    }

    [HttpGet("api/reservations")]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid? propertyId,
        [FromQuery] Guid? roomTypeId,
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureDate,
        [FromQuery] string? guestName,
        [FromQuery] string? guestPhoneNumber,
        [FromQuery] decimal? minTotal,
        [FromQuery] decimal? maxTotal)
    {
        IEnumerable<Reservation> reservations = await _reservationsService.GetFilteredAsync(
            propertyId,
            roomTypeId,
            arrivalDate,
            departureDate,
            guestName,
            guestPhoneNumber,
            minTotal,
            maxTotal);

        return Ok(reservations);
    }

    [HttpDelete("api/reservations/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Result deleteResult = await _reservationsService.DeleteByIdAsync(id);
        if (deleteResult.IsFailure)
        {
            return BadRequest(deleteResult.Error);
        }

        return NoContent();
    }

    [HttpGet("api/search")]
    public async Task<IActionResult> Search(
        [FromQuery] string city,
        [FromQuery] DateOnly arrivalDate,
        [FromQuery] DateOnly departureDate,
        [FromQuery] int personCount,
        [FromQuery] decimal maxTotal)
    {
        IEnumerable<AvailableRoomsByProperty> availableRoomsByProperties = await _reservationsService.SearchAsync(
            city,
            arrivalDate,
            departureDate,
            personCount,
            maxTotal);
        
        return Ok(availableRoomsByProperties);
    }
}