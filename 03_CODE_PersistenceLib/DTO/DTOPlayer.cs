using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO
{
	public class DTOPlayer
	{
		[JsonPropertyName("startRoomId")]
		public int StartRoomId { get; set; }
		[JsonPropertyName("startX")]
		public int StartX { get; set; }
		[JsonPropertyName("startY")]
		public int StartY { get; set; }
		[JsonPropertyName("lives")]
		public int Lives { get; set; }
	}
}
