using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienAttackUniversal.Screens
{
	public class GameScreen : DrawableGameComponent
	{
		private Player _player;
		private readonly Player _livesIcon;
		private Explosion _playerExplosion;
		private readonly Texture2D _bgScreen;
		private readonly EnemyGroup _enemyGroup;
		private readonly SpriteFont _font;
		private readonly SpriteBatch _spriteBatch;

		private readonly List<PlayerShot> _playerShots;

		private int _score;
		private int _lives;
		private double _lastTime;
		private bool _loseGame;

		private readonly Vector2 _playerVelocity = new Vector2(400 / 1000.0f, 0);

		public GameScreen(Game game) : base(game)
		{
			_spriteBatch = new SpriteBatch(game.GraphicsDevice);

			AudioManager.StartTheme();

			_player = new Player();
			_playerShots = new List<PlayerShot>();
			_font = game.Content.Load<SpriteFont>("font");

			// draw a lives status icon in the lower left
			_livesIcon = new Player();

			// cache explosion
			new Explosion();

			_bgScreen = game.Content.Load<Texture2D>("gfx\\bgScreen");
			_player.Position = new Vector2(AlienAttackGame.ScreenWidth/2 - _player.Width/2, AlienAttackGame.ScreenHeight - 120);
			_livesIcon.Position = new Vector2(20, AlienAttackGame.ScreenHeight-80);
			_livesIcon.Scale = new Vector2(0.5f, 0.5f);

			_enemyGroup = new EnemyGroup();

			_lives = 2;
		}

		public override void Update(GameTime gameTime)
		{
			MovePlayer(gameTime);
			UpdatePlayerShots(gameTime);

			// as long as we're not in the lose state, update the enemies
			if(!_loseGame)
				_enemyGroup.Update(gameTime);
			else if(InputManager.ControlState.Fire)
			{
				AudioManager.StopTheme();
				AlienAttackGame.Instance.SetState(GameState.TitleScreen);
			}

			HandleCollisions(gameTime);
		}

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
					AudioManager.PlayCue(AudioManager.Cue.Explosion);
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
				AudioManager.PlayCue(AudioManager.Cue.Explosion);
			}

			// see if an enemy hit the player directly
			if(_player != null && _enemyGroup.HandleEnemyPlayerCollision(_player))
			{
				// blow up the player
				_playerExplosion = new Explosion();
				Vector2 center = _player.Position + (_player.Size/2.0f);
				_playerExplosion.Position = center - (_playerExplosion.Size/2.0f);
				_player = null;
				AudioManager.PlayCue(AudioManager.Cue.Explosion);
				_loseGame = true;
			}

			// if the player explosion animation is running, update it
			if(_playerExplosion != null)
			{
				// if this is the last frame
				if(_playerExplosion.Update(gameTime) && !_loseGame)
				{
					// remove it
					_playerExplosion = null;

					// we lose if we have no lives left
					if(_lives == 0)
						_loseGame = true;
					else
					{
						// subract 1 life and reset the board
						_lives--;
						_enemyGroup.Reset();
						_playerShots.Clear();
						_player = new Player();
						_player.Position = new Vector2(AlienAttackGame.ScreenWidth/2 - _player.Width/2, AlienAttackGame.ScreenHeight - 100);
					}
				}
			}
		}

		private void UpdatePlayerShots(GameTime gameTime)
		{
			// if we are allowed to fire, add a shot to the list
			if(_player != null && InputManager.ControlState.Fire && gameTime.TotalGameTime.TotalMilliseconds - _lastTime > 500)
			{
				// create a new shot over the ship
				PlayerShot ps = new PlayerShot();
				ps.Position = new Vector2((_player.Position.X + _player.Width/2.0f) - ps.Width/2.0f, _player.Position.Y - ps.Height);
				_playerShots.Add(ps);
				_lastTime = gameTime.TotalGameTime.TotalMilliseconds;
				AudioManager.PlayCue(AudioManager.Cue.PlayerShot);
			}

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

		private void MovePlayer(GameTime gameTime)
		{
			if(_player != null)
			{
				if(InputManager.ControlState.FingerPosition != 0)
					_player.Position = new Vector2(InputManager.ControlState.FingerPosition, _player.Position.Y);
				else
				{
					// move left
					if(InputManager.ControlState.Left && _player.Position.X > 0)
						_player.Position -= _playerVelocity * gameTime.ElapsedGameTime.Milliseconds;

					// move right
					if(InputManager.ControlState.Right && _player.Position.X + _player.Width < AlienAttackGame.ScreenWidth)
						_player.Position += _playerVelocity * gameTime.ElapsedGameTime.Milliseconds;
				}
				_player.Update(gameTime);
			}
		}

		public override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin();

			_spriteBatch.Draw(_bgScreen, Vector2.Zero, Color.White);

			// draw the player
			if(_player != null)
				_player.Draw(gameTime, _spriteBatch);

			// draw the enemy board
			_enemyGroup.Draw(gameTime, _spriteBatch);

			// draw the player shots
			foreach(PlayerShot playerShot in _playerShots)
				playerShot.Draw(gameTime, _spriteBatch);

			// draw the explosion
			if(_playerExplosion != null)
				_playerExplosion.Draw(gameTime, _spriteBatch);

			// draw the score
			Vector2 scoreSize = _font.MeasureString("Score: " + _score);
			_spriteBatch.DrawString(_font, "Score: " + _score, new Vector2((AlienAttackGame.ScreenWidth - scoreSize.X) / 2, 25), Color.Aqua);

			// draw the lives icon
			_livesIcon.Draw(gameTime, _spriteBatch);
			_spriteBatch.DrawString(_font, "x" + _lives, new Vector2(_livesIcon.Position.X + (_livesIcon.Width*_livesIcon.Scale.X) + 8, _livesIcon.Position.Y), Color.White);

			// draw the proper text, if required
			if(_enemyGroup.AllDestroyed())
			{
				Vector2 size = _font.MeasureString("You win!");
				_spriteBatch.DrawString(_font, "You win!", new Vector2((AlienAttackGame.ScreenWidth - size.X) / 2, (AlienAttackGame.ScreenHeight - size.Y) / 2), Color.Green);
			}

			if(_loseGame)
			{
				Vector2 size = _font.MeasureString("Game Over");
				_spriteBatch.DrawString(_font, "Game Over", new Vector2((AlienAttackGame.ScreenWidth - size.X) / 2, (AlienAttackGame.ScreenHeight - size.Y) / 2), Color.Red);
			}

			_spriteBatch.End();
		}
	}
}
