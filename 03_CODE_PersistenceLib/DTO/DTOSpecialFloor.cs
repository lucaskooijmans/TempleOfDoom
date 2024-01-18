using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO;

public class DTOSpecialFloor
{
    [JsonPropertyName("type")] 
    public string Type { get; set; }
    
    [JsonPropertyName("x")] 
    public int X { get; set; }
    
    [JsonPropertyName("y")] 
    public int Y { get; set; }
}