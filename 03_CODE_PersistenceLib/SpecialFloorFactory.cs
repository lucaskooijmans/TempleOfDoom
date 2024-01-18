using System;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Floors;
using CODE_PersistenceLib.DTO;

namespace CODE_PersistenceLib;

public static class SpecialFloorTileFactory
{
    
    public static ISpecialFloor Get(DTOSpecialFloor dtoSpecialFloor)
    {
        switch (dtoSpecialFloor.Type)
        {   
            case "ice":
                return new IceFloor(new Position(dtoSpecialFloor.X, dtoSpecialFloor.Y));
            default:
                throw new ArgumentException("Invalid item type");
        }
    }
    
}