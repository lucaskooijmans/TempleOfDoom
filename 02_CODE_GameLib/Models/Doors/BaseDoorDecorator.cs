using System;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors;

public abstract class BaseDoorDecorator: IDoor
{
    private IDoor _decoratee;
    private bool _isOpen;
    
    public virtual bool IsOpen { get => _decoratee.IsOpen && _isOpen; protected set => _isOpen = value; }

    protected BaseDoorDecorator(IDoor decoratee)
    {
        _decoratee = decoratee;
        _isOpen = true;
    }
    
    public virtual void Interact(Player player) => _decoratee.Interact(player);
    public static IDoor GetDecoratorOrNull(IDoor door, Type decoratorType)
    {
        while (true)
        {
            if (door.GetType() == decoratorType)
            {
                return door;
            }

            if (door is BaseDoorDecorator decoratedDoor)
            {
                door = decoratedDoor._decoratee;
                continue;
            }

            return null;
        }
    }
}