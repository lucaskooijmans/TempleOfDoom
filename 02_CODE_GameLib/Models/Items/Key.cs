using CODE_GameLib.Enums;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Items
{
    public class Key : IItem
    {
        public Position Position { get; }
        public bool Exists { get; private set; }

        public Color Color { get; }

        public Key(Position position, Color color)
        {
            Position = position;
            Exists = true;
            Color = color;
        }
        public void Interact(Player player)
        {
            player.AddItemToInventory(this);
            Exists = false;
        }
    }
}