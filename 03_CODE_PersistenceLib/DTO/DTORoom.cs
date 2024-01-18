using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CODE_PersistenceLib.DTO
{
	public class DTORoom
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("type")]
		public string Type { get; set; }
		[Range(3, 50)]
		[JsonPropertyName("width")]
		public int Width { get; set; }
		[Range(3, 50)]
		[JsonPropertyName("height")]
		public int Height { get; set; }
		[JsonPropertyName("items")]
		public DTOItem[] DTOItems { get; set; }
		[JsonPropertyName("specialFloorTiles")]
		public DTOSpecialFloor[] SpecialFloors { get; set; }
		[JsonPropertyName("enemies")]
		public DTOEnemy[] Enemies { get; set; }
	}

}
