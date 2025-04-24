using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts;

namespace PropertiesApi.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private static List<Entities.Property> _properties = [];
    
    [HttpPost]
    public IActionResult Create([FromBody]CreatePropertyRequest createPropertyRequest)
    {
        var property = new Entities.Property(createPropertyRequest.Name);
        _properties.Add(property);
        
        return Created();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<Contracts.Property> propertiesResponse =
            _properties.Select(x => new Contracts.Property(x.Id, x.Name));
        
        return Ok(propertiesResponse);
    }

    [HttpGet("{propertyId:guid}")]
    public IActionResult Get([FromRoute] Guid propertyId)
    {
        Entities.Property? property = _properties.FirstOrDefault(x => x.Id == propertyId);
        if (property is null)
        {
            return NotFound();
        }
        
        Contracts.Property propertyResponse = new Contracts.Property(property.Id, property.Name);
        
        return Ok(propertyResponse);
    }

    [HttpDelete("{propertyId:guid}")]
    public IActionResult Delete([FromRoute] Guid propertyId)
    {
        Entities.Property? property = _properties.FirstOrDefault(x => x.Id == propertyId);
        if (property is null)
        {
            return NotFound();
        }
        
        _properties.Remove(property);

        return NoContent();
    }
}