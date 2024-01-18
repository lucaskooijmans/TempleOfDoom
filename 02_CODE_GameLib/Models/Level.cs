using System.Collections.Generic;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
	public class Level
	{
		private Dictionary<int, Room.Room> Rooms { get; }
		public Player Player { get; }
		
		public string LevelName { get; }

		public Level(Player player, Dictionary<int, Room.Room> rooms, string levelName) { 
			Player = player;
			Rooms = rooms;
			LevelName = levelName;
		}
	}



}
