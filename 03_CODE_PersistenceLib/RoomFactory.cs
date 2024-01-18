using CODE_GameLib.Models;
using CODE_PersistenceLib.DTO;
using CODE_GameLib.Models.Entities.Enemy;
using CODE_GameLib.Models.Room;

namespace CODE_PersistenceLib;

public static class RoomFactory
{
    
    public static Room CreateRoom(DTORoom dtoRoom)
    {
        Room room = new Room(dtoRoom.Id, dtoRoom.Width, dtoRoom.Height, dtoRoom.Type);
        
        if (dtoRoom.Enemies != null)
        {
            foreach (DTOEnemy dtoEnemy in dtoRoom.Enemies)
            {
                room.Entities.Add(new EnemyAdapter(dtoEnemy.Type, dtoEnemy.X, dtoEnemy.Y, dtoEnemy.MinX, dtoEnemy.MaxX, dtoEnemy.MinY, dtoEnemy.MaxY));
            }
        }

        if (dtoRoom.SpecialFloors != null)
        {
            foreach (DTOSpecialFloor dtoSpecialFloor in dtoRoom.SpecialFloors)
            {
                room.SpecialFloors.Add(SpecialFloorTileFactory.Get(dtoSpecialFloor));
            }
        }


        if (dtoRoom.DTOItems != null)
        {
            foreach (DTOItem dtoItem in dtoRoom.DTOItems)
            {
                Position position = new Position(dtoItem.X, dtoItem.Y);
                room.Items.Add(ItemFactory.Get(dtoItem.Type, position, dtoItem.Damage, dtoItem.Color));
            }
        }

        return room;
    }
    
}