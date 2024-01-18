using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors;

public sealed class OpenOnOddLivesDecorator : BaseDoorDecorator
{
    public OpenOnOddLivesDecorator(IDoor decoratee) : base(decoratee)
    {
        IsOpen = false;
    }
    
    public override void Interact(Player player)
    {
        base.Interact(player);

       IsOpen = player.Lives % 2 == 1;
    }
}