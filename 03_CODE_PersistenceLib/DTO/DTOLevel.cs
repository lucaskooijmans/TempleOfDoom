using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO
{

    public class DTOLevel
    {
        [JsonPropertyName("rooms")]
        public DTORoom[] Rooms { get; set; }
        [JsonPropertyName("connections")]
        public DTOConnection[] Connections { get; set; }
        [JsonPropertyName("player")]
        public DTOPlayer Player { get; set; }
    }

}
