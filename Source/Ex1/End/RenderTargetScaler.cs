using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienAttackUniversal
{
	class RenderTargetScaler
	{
		private readonly RenderTarget2D _drawBuffer;
		private readonly GraphicsDeviceManager _graphicsDeviceManager;
		private readonly int _screenWidth;
		private readonly int _screenHeight;
		private readonly Game _game;
		private readonly SpriteBatch _spriteBatch;

		public RenderTargetScaler(Game game, GraphicsDeviceManager graphicsDeviceManager, int width, int height)
		{
			_game = game;            
			_graphicsDeviceManager = graphicsDeviceManager;
			_screenWidth = width;
			_screenHeight = height;

			_spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice);

			// create a surface to draw on which is then scaled to the screen size on the PC
			_drawBuffer = new RenderTarget2D(graphicsDeviceManager.GraphicsDevice,
											width, height);

		}

		public void SetRenderTarget()
		{
			_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(_drawBuffer);
		}

		public void Draw()
		{
			float outputAspect = _game.Window.ClientBounds.Width / (float)_game.Window.ClientBounds.Height;
			float preferredAspect = _screenWidth / (float)_screenHeight;

			Rectangle dst;

			if (outputAspect <= preferredAspect)
			{
				// output is taller than it is wider, bars on top/bottom
				int presentHeight = (int)((_game.Window.ClientBounds.Width / preferredAspect) + 0.5f);
				int barHeight = (_game.Window.ClientBounds.Height - presentHeight) / 2;

				dst = new Rectangle(0, barHeight, _game.Window.ClientBounds.Width, presentHeight);
			}
			else
			{
				// output is wider than it is tall, bars left/right
				int presentWidth = (int)((_game.Window.ClientBounds.Height * preferredAspect) + 0.5f);
				int barWidth = (_game.Window.ClientBounds.Width - presentWidth) / 2;

				dst = new Rectangle(barWidth, 0, presentWidth, _game.Window.ClientBounds.Height);
			}

			_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);

			// clear to get black bars
			_graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);

			// draw a quad to get the draw buffer to the back buffer
			_spriteBatch.Begin();
				_spriteBatch.Draw(_drawBuffer, dst, Color.White);
			_spriteBatch.End();
		}
	}
}
