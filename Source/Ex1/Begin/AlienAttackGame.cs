using System.Collections.Generic;
using AlienAttackUniversal.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AlienAttackUniversal
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class AlienAttackGame : Game
	{
		public static int ScreenWidth = 1920;
		public static int ScreenHeight = 1080;
		private static int ShotTime = 500;
		private static readonly Vector2 PlayerVelocity = new Vector2(400 / 1000.0f, 0);
		private static readonly Vector2 ScorePosition = new Vector2(ScreenWidth / 2.0f, 30);
		private static Vector2 PlayerStartPosition;

		public static AlienAttackGame Instance;
		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private RenderTargetScaler _scaler;
		private static KeyboardState _keyboardState, _lastKeyboard;
		private Texture2D _bgScreen;
		private Player _player;
		private List<PlayerShot> _playerShots;
		private double _lastShotTime;
		private Explosion _playerExplosion;
		private EnemyGroup _enemyGroup;
		private SpriteFont _font;
		private Song _theme;
		private SoundEffect _playerShot;
		private SoundEffect _explosion;

		private int _score;

		public AlienAttackGame()
		{
			Instance = this;

			_graphics = new GraphicsDeviceManager(this);

			// set our screen size based on the device
			_graphics.PreferredBackBufferWidth = ScreenWidth;
			_graphics.PreferredBackBufferHeight = ScreenHeight;

			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			_scaler = new RenderTargetScaler(this, _graphics, ScreenWidth, ScreenHeight);

			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_player = new Player();
			PlayerStartPosition = new Vector2(ScreenWidth / 2 - _player.Width / 2, ScreenHeight - 120);
			_player.Position = PlayerStartPosition;

			_enemyGroup = new EnemyGroup();

			_playerShots = new List<PlayerShot>();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			_bgScreen = Content.Load<Texture2D>("gfx\\bgScreen");
			_font = Content.Load<SpriteFont>("font");
			_theme = Content.Load<Song>("sfx\\theme");           
			_playerShot = Content.Load<SoundEffect>("sfx\\playerShot");
			_explosion = Content.Load<SoundEffect>("sfx\\explosion");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if(MediaPlayer.State == MediaState.Stopped)
			{
				MediaPlayer.IsRepeating = true;
				MediaPlayer.Play(_theme);
			}

			_keyboardState = Keyboard.GetState();

			if (_player != null)
			{
				_player.Update(gameTime);
			}

			_enemyGroup.Update(gameTime);
			UpdatePlayerShots(gameTime);
			HandleCollisions(gameTime);
			_lastKeyboard = _keyboardState;

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			_scaler.SetRenderTarget();
			_spriteBatch.Begin();

			_spriteBatch.Draw(_bgScreen, Vector2.Zero, Color.White);
			
			// draw the enemy board
			_enemyGroup.Draw(gameTime, _spriteBatch);

			// draw the player shots
			foreach(PlayerShot playerShot in _playerShots)
				playerShot.Draw(gameTime, _spriteBatch);

			// draw the player
			if (_player != null)
				_player.Draw(gameTime, _spriteBatch);

			// draw the player explosion
			if (_playerExplosion != null)
				_playerExplosion.Draw(gameTime, _spriteBatch);

			// draw the score
			Vector2 scoreSize = _font.MeasureString("Score: " + _score);
			_spriteBatch.DrawString(_font, "Score: " + _score, ScorePosition - scoreSize/2.0f, Color.Aqua);

			_spriteBatch.End();
			_scaler.Draw();
		}

#region Game Logic
		private void HandleCollisions(GameTime gameTime)
		{
			// see if a player shot hit an enemy
			for(int i = 0; i < _playerShots.Count; i++)
			{
				PlayerShot playerShot = _playerShots[i];
				// check the shot and see if it it collided with an enemy
				if(playerShot != null && _enemyGroup.HandlePlayerShotCollision(_playerShots[i]))
				{
					// remove the shot, add the score
					_playerShots.RemoveAt(i);
					_score += 100;
					_explosion.Play();
				}
			}

			// see if an enemy shot hit the player
			if(_player != null && _enemyGroup.HandleEnemyShotCollision(_player))
			{
				// blow up the player
				_playerExplosion = new Explosion();
				Vector2 center = _player.Position + (_player.Size/2.0f);
				_playerExplosion.Position = center - (_playerExplosion.Size/2.0f);
				_player = null;
				_explosion.Play();
			}

			// if the player explosion animation is running, update it
			if(_playerExplosion != null)
			{
				// if this is the last frame
				if(_playerExplosion.Update(gameTime))
				{
					// remove it
					_playerExplosion = null;

					// reset the board
					_enemyGroup.Reset();
					_playerShots.Clear();

					_player = new Player();
					_player.Position = PlayerStartPosition;
				}
			}
		}

		private void AddPlayerShot()
		{
			// create a new shot over the ship
			PlayerShot ps = new PlayerShot();
			ps.Position = _player.Position + (_player.Size/2) - (ps.Size/2) - new Vector2(0, ps.Height);
			_playerShots.Add(ps);
		}

		private void UpdatePlayerShots(GameTime gameTime)
		{
			// enumerate the player shots on the screen
			for(int i = 0; i < _playerShots.Count; i++)
			{
				PlayerShot playerShot = _playerShots[i];

				playerShot.Update(gameTime);

				// if it's off the top of the screen, remove it from the list
				if(playerShot.Position.Y + playerShot.Height < 0)
					_playerShots.RemoveAt(i);
			}
		}
#endregion
	}
}
