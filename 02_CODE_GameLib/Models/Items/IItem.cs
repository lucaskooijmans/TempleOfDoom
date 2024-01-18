using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Items
{
    public interface IItem: IPositioned
    {
		public bool Exists { get; }
		public Position Position { get; }
		public void Interact(Player player);
	}
}
