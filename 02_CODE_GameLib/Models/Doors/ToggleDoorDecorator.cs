using CODE_GameLib.Models.Items;
using CODE_GameLib.ObservableInterfaces;

namespace CODE_GameLib.Models.Doors;

public sealed class ToggleDoorDecorator: BaseDoorDecorator, Observer
{
    private readonly IDoor _decoratee;
    private bool _isOpen;
    
    public ToggleDoorDecorator(IDoor decoratee) : base(decoratee)
    {
        IsOpen = false;
        _isOpen = false;
        _decoratee = decoratee;
    }

    public void Update(Observable observable)
    {
        if (observable is not PressurePlate pressurePlate) return;
        _isOpen = pressurePlate.Activated;
        IsOpen = _isOpen;

    }
}