using CSharpFunctionalExtensions;
using Domain.Entities;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts.Properties;

namespace PropertiesApi.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePropertyRequest createPropertyRequest)
    {
        Result<Property> result = await _propertyService.CreateAsync(
            createPropertyRequest.Name,
            createPropertyRequest.Country,
            createPropertyRequest.City,
            createPropertyRequest.Address,
            createPropertyRequest.Latitude,
            createPropertyRequest.Longitude);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created("", result.Value.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Property> properties = await _propertyService.GetAllAsync();
        
        IEnumerable<PropertyResponse> propertiesResponse = properties
            .Select(pr => new PropertyResponse(
                pr.Id,
                pr.Name,
                pr.Country,
                pr.City,
                pr.Address,
                pr.Latitude,
                pr.Longitude));

        return Ok(propertiesResponse);
    }

    [HttpGet("{propertyId:guid}")]
    public async Task<IActionResult> GetById(Guid propertyId)
    {
        Property? property = await _propertyService.GetByIdAsync(propertyId);
        if (property is null)
        {
            return NotFound();
        }

        PropertyResponse propertyResponse = new(
            property.Id,
            property.Name,
            property.Country,
            property.City,
            property.Address,
            property.Latitude,
            property.Longitude);

        return Ok(propertyResponse);
    }

    [HttpPut("{propertyId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid propertyId, UpdatePropertyRequest updatePropertyRequest)
    {
        Result updateResult = await _propertyService.UpdateAsync(
            propertyId,
            updatePropertyRequest.Name,
            updatePropertyRequest.Country,
            updatePropertyRequest.City,
            updatePropertyRequest.Address,
            updatePropertyRequest.Latitude,
            updatePropertyRequest.Longitude);

        if (updateResult.IsFailure)
        {
            return BadRequest(updateResult.Error);
        }

        return NoContent();
    }

    [HttpDelete("{propertyId:guid}")]
    public async Task<IActionResult> Delete(Guid propertyId)
    {
        Result deleteResult = await _propertyService.DeleteAsync(propertyId);
        if (deleteResult.IsFailure)
        {
            return BadRequest(deleteResult.Error);
        }

        return NoContent();
    }
}