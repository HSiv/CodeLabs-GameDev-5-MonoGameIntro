using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienAttackUniversal.Sprites
{
	public class Player : Sprite
	{
		public Player()
		{
			LoadContent(AlienAttackGame.Instance.Content,  "gfx\\player");
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if(Frames == null)
				return;

			spriteBatch.Draw(Frames[0], Position, color:Color.White, scale:Scale);
		}
	}
}
