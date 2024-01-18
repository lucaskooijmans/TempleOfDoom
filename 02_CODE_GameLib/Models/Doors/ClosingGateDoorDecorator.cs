using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors;

public sealed class ClosingGateDoorDecorator : BaseDoorDecorator
{
    
    private bool _locked;

    public ClosingGateDoorDecorator(IDoor decoratee) : base(decoratee)
    {
        IsOpen = true;
    }
    
    public override void Interact(Player player)
    {
        base.Interact(player);

        if (_locked)
        {
            IsOpen = false;
        }

        _locked = true;
    }
}