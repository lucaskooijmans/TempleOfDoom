using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO
{
    public class DTOConnection
    {
        [JsonPropertyName("NORTH")]
        public int? RoomIdNorth { get; set; }
        [JsonPropertyName("SOUTH")]
        public int? RoomIdSouth { get; set; }
        [JsonPropertyName("WEST")]
        public int? RoomIdWest { get; set; }
        [JsonPropertyName("EAST")]
        public int? RoomIdEast { get; set; }
        
        [JsonPropertyName("doors")]
        public DTODoor[] Doors { get; set; }
    }

}
