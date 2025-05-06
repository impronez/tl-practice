using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts.RoomTypes;

namespace PropertiesApi.Controllers;

[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypesController(IRoomTypeService roomTypeService)
    {
        _roomTypeService = roomTypeService;
    }

    [HttpPost("api/properties/{propertyId:guid}/roomtypes")]
    public async Task<IActionResult> Create(Guid propertyId, [FromBody] CreateRoomTypeRequest createRoomTypeRequest)
    {
        Result<RoomType> result = await _roomTypeService.CreateAsync(
            propertyId,
            createRoomTypeRequest.Name,
            createRoomTypeRequest.DailyPrice,
            createRoomTypeRequest.Currency,
            createRoomTypeRequest.MinPersonCount,
            createRoomTypeRequest.MaxPersonCount,
            createRoomTypeRequest.Services,
            createRoomTypeRequest.Amenities);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created("", result.Value.Id);
    }

    [HttpGet("api/roomtypes/{id:guid}")]
    public async Task<IActionResult> GetByRoomTypeId(Guid id)
    {
        RoomType? existingRoomType = await _roomTypeService.GetByRoomTypeIdAsync(id);
        if (existingRoomType is null)
        {
            return NotFound();
        }

        RoomTypeResponse roomTypeResponse = new RoomTypeResponse(
            existingRoomType.Id,
            existingRoomType.PropertyId,
            existingRoomType.Name,
            existingRoomType.DailyPrice,
            existingRoomType.Currency.Value,
            existingRoomType.MinPersonCount,
            existingRoomType.MaxPersonCount,
            existingRoomType.Services,
            existingRoomType.Amenities);

        return Ok(roomTypeResponse);
    }

    [HttpGet("/api/properties/{propertyId:guid}/roomtypes")]
    public async Task<IActionResult> GetByPropertyId(Guid propertyId)
    {
        List<RoomType> roomTypes = await _roomTypeService.GetByPropertyIdAsync(propertyId);
        
        IEnumerable<RoomTypeResponse> roomTypesResponse = roomTypes
            .Select(r => new RoomTypeResponse(
                r.Id,
                r.PropertyId,
                r.Name,
                r.DailyPrice,
                r.Currency.Value,
                r.MinPersonCount,
                r.MaxPersonCount,
                r.Services,
                r.Amenities));

        return Ok(roomTypesResponse);
    }

    [HttpPut("api/roomtypes/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRoomTypeRequest updateRoomTypeRequest)
    {
        Result updateResult = await _roomTypeService.UpdateAsync(
            id,
            updateRoomTypeRequest.Name,
            updateRoomTypeRequest.DailyPrice,
            updateRoomTypeRequest.Currency,
            updateRoomTypeRequest.MinPersonCount,
            updateRoomTypeRequest.MaxPersonCount,
            updateRoomTypeRequest.Services,
            updateRoomTypeRequest.Amenities);

        if (updateResult.IsFailure)
        {
            return BadRequest(updateResult.Error);
        }

        return NoContent();
    }

    [HttpDelete("api/roomtypes/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Result deleteResult = await _roomTypeService.DeleteAsync(id);
        if (deleteResult.IsFailure)
        {
            return BadRequest(deleteResult.Error);
        }

        return NoContent();
    }
}