using Microsoft.Xna.Framework;

namespace AlienAttackUniversal.Sprites
{
	public class Explosion : Sprite
	{
		public Explosion()
		{
			LoadContent(AlienAttackGame.Instance.Content, "gfx\\explosion\\explosion_{0}", 9);
		}

		public new bool Update(GameTime gameTime)
		{
			// if it's the final frame, return true to let the other side know we're done
			if(FrameIndex == 8)
				return true;

			AnimateLoop(gameTime, 60);

			return false;
		}
	}
}
