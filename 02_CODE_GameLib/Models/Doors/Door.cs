using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors
{
    public class Door : IDoor
    {
        public bool IsOpen { get; private set; } = true;

        public void Interact(Player player)
        {
            IsOpen = true;
        }
    }
}
