using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Items
{
    public class SankaraStone : IItem
    {
        public bool Exists { get; set; }
        public Position Position { get; }

        public SankaraStone(Position position)
        {
            Position = position;
            Exists = true;
        }

        public void Interact(Player player)
        {
            player.AddItemToInventory(this);
            Exists = false;
        }
    }
}