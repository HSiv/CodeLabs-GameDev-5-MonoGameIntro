using Microsoft.Xna.Framework;

namespace AlienAttackUniversal
{
	public class Enemy : Sprite
	{
		public Enemy()
		{
			LoadContent(AlienAttackGame.Instance.Content, "gfx\\enemy1\\enemy1_{0}", 10);
		}

		public override void Update(GameTime gameTime)
		{
			AnimateReverse(gameTime, 60);
		}
	}
}
