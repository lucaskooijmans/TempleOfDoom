using System;
using System.IO;
using CODE_GameLib.Enums;
using CODE_GameLib.Models.Doors;
using CODE_PersistenceLib.DTO;

namespace CODE_PersistenceLib;

public class DoorFactory
{

    public IDoor CreateDoor(DTODoor[] dtoDoors)
    {
        IDoor door = new Door();
        
        foreach (DTODoor dtoDoor in dtoDoors)
        {
            switch (dtoDoor.Type)
            {
                case "toggle":
                    door = new ToggleDoorDecorator(door);
                    break;
                case "closing gate":
                    door = new ClosingGateDoorDecorator(door);
                    break;
                case "open on odd":
                    door = new OpenOnOddLivesDecorator(door);
                    break;
                case "open on stones in room":
                    door = new OpenOnStonesInRoomDoorDecorator(door, dtoDoor.StonesAmount);
                    break;
                case "colored":
                    Color doorColor = Enum.Parse<Color>(dtoDoor.Color ?? throw new InvalidDataException("A door needs a color value"), true);
                    door = new ColoredDoorDecorator(door, doorColor);
                    break;
                default:
                    throw new ArgumentException("Unknown type of door");
                
            }
            
        }

        return door;

    }
    
}