using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO
{
    public class DTOItem
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
        
        // Optional values

        [JsonPropertyName("damage")]
        public int? Damage { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }
    }

}
