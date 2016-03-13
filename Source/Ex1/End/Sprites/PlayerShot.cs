using Microsoft.Xna.Framework;

namespace AlienAttackUniversal.Sprites
{
	public class PlayerShot : Sprite
	{
		public PlayerShot()
		{
			LoadContent(AlienAttackGame.Instance.Content, "gfx\\pshot\\pshot_{0}", 3);
			Velocity = new Vector2(0, -300 / 1000.0f);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			AnimateReverse(gameTime, 100);
		}
	}
}
