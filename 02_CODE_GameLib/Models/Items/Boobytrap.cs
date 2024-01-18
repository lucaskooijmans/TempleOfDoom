using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Items
{
    public class Boobytrap : IItem
    {
        public Position Position { get; }
        public bool Exists { get; internal set; }

        private readonly int _damage;

        public Boobytrap(Position position, int damage)
        {
            Position = position;
            _damage = damage;
            Exists = true;
        }
        public virtual void Interact(Player player)
        {
            player.Damage(_damage);
        }
    }
}