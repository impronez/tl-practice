using Microsoft.AspNetCore.Mvc;
using Properties.Domain.Repositories;
using PropertiesApi.Contracts;

namespace PropertiesApi.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertiesRepository _propertiesRepository;

    public PropertiesController(IPropertiesRepository propertiesRepository)
    {
        _propertiesRepository = propertiesRepository;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreatePropertyRequest createPropertyRequest)
    {
        var property = new Properties.Domain.Entities.Property(createPropertyRequest.Name);
        _propertiesRepository.Add(property);
        
        return Created();
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<Contracts.Property> propertiesResponse =
            _propertiesRepository.GetAll().Select(x => new Contracts.Property(x.Id, x.Name));
        
        return Ok(propertiesResponse);
    }
    
    [HttpGet("{propertyId:guid}")]
    public IActionResult Get([FromRoute] Guid propertyId)
    {
        Properties.Domain.Entities.Property? property = _propertiesRepository.GetById(propertyId);
        if (property is null)
        {
            return NotFound();
        }
        
        Contracts.Property propertyResponse = new Contracts.Property(property.Id, property.Name);
        
        return Ok(propertyResponse);
    }
    
    [HttpPut("{propertyId:guid}")]
    public IActionResult Update([FromRoute] Guid propertyId, UpdatePropertyRequest updatePropertyRequest)
    {
        Properties.Domain.Entities.Property? property = _propertiesRepository.GetById(propertyId);
        if (property is null)
        {
            return NotFound();
        }
        
        property.Name = updatePropertyRequest.Name;
        
        return Ok();
    }

    [HttpDelete("{propertyId:guid}")]
    public IActionResult Delete([FromRoute] Guid propertyId)
    {
        Properties.Domain.Entities.Property? property = _propertiesRepository.GetById(propertyId);
        if (property is null)
        {
            return NotFound();
        }
        
        _propertiesRepository.Delete(property.Id);

        return NoContent();
    }
}