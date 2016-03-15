using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienAttackUniversal.Screens
{
    class TitleScreen : DrawableGameComponent
    {
        private readonly Texture2D _titleScreen;
        private readonly SpriteFont _font;
        private readonly SpriteBatch _spriteBatch;

        public TitleScreen(Game game) : base(game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            _titleScreen = game.Content.Load<Texture2D>("gfx\\titleScreen");
            _font = game.Content.Load<SpriteFont>("Font");
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.ControlState.Start)
                AlienAttackGame.Instance.SetState(GameState.GameScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_titleScreen, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_font, "Press Enter or Tap to Play", new Vector2(1200, 960), Color.White);
            _spriteBatch.End();
        }
    }
}
