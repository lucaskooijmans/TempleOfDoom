using CODE_GameLib.Enums;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Floors;

public class IceFloor : ISpecialFloor
{
    public Position Position { get; }
    public Direction direction { get; }
    
    public void Interact(IEntity entity)
    {
        entity.Move(entity.GetLastDirection());
    }

    public IceFloor(Position position)
    {
        Position = position;
    }
}