using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Items
{
	public class DisappearingBoobytrap : Boobytrap
	{
		public DisappearingBoobytrap(Position position, int damage) : base(position, damage)
		{
		}

		public override void Interact(Player player)
		{
			base.Interact(player);
			Exists = false;
		}
	}
}
