using CODE_GameLib.Models.Doors;

namespace CODE_GameLib.Models.Room
{
    public class Connection
	{
		public Room Room { get; set; }
		public IDoor Door { get; set; }
		
		public Connection(Room room, IDoor door)
		{
			Room = room;
			Door = door;
		}
	}
}
