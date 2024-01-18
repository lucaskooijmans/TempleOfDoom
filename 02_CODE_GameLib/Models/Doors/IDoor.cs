using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors;

public interface IDoor
{
    public bool IsOpen { get; }
    
    public void Interact(Player player);
}