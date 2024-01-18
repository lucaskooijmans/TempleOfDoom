using System.Collections.Generic;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Models.Room;
using CODE_PersistenceLib.DTO;

namespace CODE_PersistenceLib;
public static class LevelFactory
{
    public static Level CreateLevel(DTOLevel dtoLevel, string fileName)
    {
        Dictionary<int, Room> rooms = RoomConnectionsCreator.Build(dtoLevel.Rooms, dtoLevel.Connections);
        rooms.TryGetValue(dtoLevel.Player.StartRoomId, out Room startRoom);

        Player player = new Player(startRoom, new Position(dtoLevel.Player.StartX, dtoLevel.Player.StartY), dtoLevel.Player.Lives);
        return new Level(player, rooms, fileName);
    }
}