using System.Text.Json.Serialization;

namespace PropertiesApi.Contracts.Properties;

public class CreatePropertyRequest
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("country")] public string Country { get; set; }

    [JsonPropertyName("city")] public string City { get; set; }

    [JsonPropertyName("address")] public string Address { get; set; }

    [JsonPropertyName("latitude")] public double Latitude { get; set; }

    [JsonPropertyName("longitude")] public double Longitude { get; set; }
}