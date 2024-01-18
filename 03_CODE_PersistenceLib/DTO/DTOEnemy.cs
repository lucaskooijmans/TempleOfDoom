using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO;

public class DTOEnemy
{
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("minX")]
    public int MinX { get; set; }
    [JsonPropertyName("maxX")]
    public int MaxX { get; set; }
    [JsonPropertyName("minY")]
    public int MinY { get; set; }
    [JsonPropertyName("maxY")]
    public int MaxY { get; set; }
}