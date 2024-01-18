using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO
{
	public class DTODoor
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; }
        [JsonPropertyName("no_of_stones")]
        public int StonesAmount { get; set; }
    }

}
