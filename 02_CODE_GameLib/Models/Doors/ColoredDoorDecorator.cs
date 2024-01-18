using CODE_GameLib.Enums;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors;

public class ColoredDoorDecorator: BaseDoorDecorator
{
    private readonly Color _color; 
    
    public override void Interact(Player player)
        {
            if (player.Inventory.HasKey(_color))
            {
                IsOpen = true;
            }
        }
    
    public ColoredDoorDecorator(IDoor decoratee, Color color) : base(decoratee)
    {
        _color = color;
        IsOpen = false;
    }

    public Color GetColor()
    {
        return _color;
    }
}