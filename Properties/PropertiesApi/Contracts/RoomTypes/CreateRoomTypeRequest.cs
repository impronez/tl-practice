using System.Text.Json.Serialization;

namespace PropertiesApi.Contracts.RoomTypes;

public class CreateRoomTypeRequest
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("daily_price")] public decimal DailyPrice { get; set; }
    [JsonPropertyName("currency")] public string Currency { get; set; }
    [JsonPropertyName("min_person_count")] public int MinPersonCount { get; set; }
    [JsonPropertyName("max_person_count")] public int MaxPersonCount { get; set; }
    [JsonPropertyName("services")] public string Services { get; set; }
    [JsonPropertyName("amenities")] public string Amenities { get; set; }
}