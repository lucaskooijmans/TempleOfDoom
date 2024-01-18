using CODE_GameLib.Enums;

namespace CODE_GameLib.Models.Entities;

public interface IEntity
{

    public Position Position { get; }
    public int Lives { get; }

    public bool IsDead { get; }

    public void Damage(int amount);

    public void Move(Direction direction);

    public Direction GetLastDirection();
}