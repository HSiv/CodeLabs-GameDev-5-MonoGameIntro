using Microsoft.Xna.Framework;

namespace AlienAttackUniversal
{
	public class EnemyShot : Sprite
	{
		public EnemyShot()
		{
			LoadContent(AlienAttackGame.Instance.Content, "gfx\\eshot\\eshot_{0}", 3);
			Velocity = new Vector2(0, 350 / 1000.0f);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			AnimateReverse(gameTime, 60);
		}
	}
}
