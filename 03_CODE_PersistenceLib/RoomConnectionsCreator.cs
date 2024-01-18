using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Enums;
using CODE_GameLib.Models.Doors;
using CODE_GameLib.Models.Items;
using CODE_GameLib.Models.Room;
using CODE_GameLib.Models.Utils;
using CODE_PersistenceLib.DTO;

namespace CODE_PersistenceLib;

public static class RoomConnectionsCreator
{
    
    private static Dictionary<int, Room> _roomDict;
    public static Dictionary<int, Room> Build(DTORoom[] rooms, DTOConnection[] connections)
    {
        _roomDict = rooms
            .Select(RoomFactory.CreateRoom)
            .ToDictionary(v => v.Id, v => v);

        DTOConnection[] doorConnections = connections.Where(c =>
                c.RoomIdSouth != null || c.RoomIdNorth != null || c.RoomIdWest != null || c.RoomIdEast != null)
            .ToArray();
        
        
        foreach (Dictionary<Direction, Connection> connection in doorConnections.Select(DTOConnectionToDictionary))
        {
            foreach (KeyValuePair<Direction, Connection> kvp in connection)
            {
                Direction direction = kvp.Key;
                Connection entrance = kvp.Value;

                entrance.Room.ConnectedRooms[direction] = connection[direction.Opposite()];
            }
        }
        
        ConnectPressurePlatesToDoors();

        return _roomDict;
    }
    
    private static Dictionary<Direction, Connection> DTOConnectionToDictionary(DTOConnection connection)
    {
        DTODoor[] dtoDoorTypes = connection.Doors;
        DoorFactory doorFactory = new DoorFactory(); 
        IDoor door = doorFactory.CreateDoor(dtoDoorTypes);

        Dictionary<Direction, Connection> dict = new Dictionary<Direction, Connection>();
        void AddConnection(Direction direction, int? roomId)
            => dict.Add(direction, new Connection(_roomDict[roomId!.Value], door));
        if (connection.RoomIdNorth != null) AddConnection(Direction.North, connection.RoomIdNorth);
        if (connection.RoomIdEast != null) AddConnection(Direction.East, connection.RoomIdEast);
        if (connection.RoomIdSouth != null) AddConnection(Direction.South, connection.RoomIdSouth);
        if (connection.RoomIdWest != null) AddConnection(Direction.West, connection.RoomIdWest);
        return dict;
    }

    private static void ConnectPressurePlatesToDoors()
    {
        foreach (KeyValuePair<int,Room> room in _roomDict)
        {
            if (!room.Value.Items.OfType<PressurePlate>().Any()) continue;
            Dictionary<Direction,Connection>.ValueCollection connections = room.Value.ConnectedRooms.Values;
            
            foreach (Connection connection in connections)
            {
                IDoor door = BaseDoorDecorator.GetDecoratorOrNull(connection.Door, typeof(ToggleDoorDecorator));

                if (door is not ToggleDoorDecorator toggleDoorDecorator) continue;
                List<PressurePlate> pressurePlates = room.Value.Items.OfType<PressurePlate>().ToList();

                foreach (PressurePlate pressurePlate in pressurePlates)
                {
                    pressurePlate.Attach(toggleDoorDecorator);
                }
            }
        }
    }
}