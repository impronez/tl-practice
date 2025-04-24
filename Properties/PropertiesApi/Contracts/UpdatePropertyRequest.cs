using System.Text.Json.Serialization;

namespace PropertiesApi.Contracts;

public class UpdatePropertyRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}