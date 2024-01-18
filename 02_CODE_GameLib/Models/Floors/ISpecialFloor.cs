using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Floors;

public interface ISpecialFloor : IPositioned
{
    public Position Position { get; }
    public void Interact(IEntity entity);
    
    public bool IsSpecialFloor(Position position)
    {
        return Position.Equals(position);
    }
}