using System.Linq;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Models.Items;

namespace CODE_GameLib.Models.Doors;

public class OpenOnStonesInRoomDoorDecorator : BaseDoorDecorator
{
    private readonly int _stonesAmount;
    
    public OpenOnStonesInRoomDoorDecorator(IDoor decoratee, int stonesAmount) : base(decoratee)
    {
        _stonesAmount = stonesAmount;
    }
    
    public override void Interact(Player player)
    {
        base.Interact(player);
        if (player.CurrentRoom.Items.Count(item => item.Exists && item is SankaraStone) == _stonesAmount)
        {
            IsOpen = true;
        }
    }
}