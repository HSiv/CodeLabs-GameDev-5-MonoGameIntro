using System;
using Windows.UI.ViewManagement;
using Microsoft.Xna.Framework;
using AlienAttackUniversal.Screens;

namespace AlienAttackUniversal
{
	public enum GameState
	{
		TitleScreen,
		GameScreen
	};

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class AlienAttackGame : Game
	{
		public static AlienAttackGame Instance;

		private readonly GraphicsDeviceManager _graphics;

		private DrawableGameComponent _screen;
		private RenderTargetScaler _scaler;

		public static int ScreenWidth = 1920;
		public static int ScreenHeight = 1080;

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

			// create the title screen
			SetState(GameState.TitleScreen);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
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
			// update the user input
			InputManager.Update();

			// Allows the game to exit
			//if(InputManager.ControlState.Quit)
			//	this.Exit();

			// update the current screen
			_screen.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			_scaler.SetRenderTarget();
			_screen.Draw(gameTime);
			_scaler.Draw();
		}

		public void SetState(GameState newState)
		{
			switch(newState)
			{
				case GameState.TitleScreen:
					_screen = new TitleScreen(this);
					break;
				case GameState.GameScreen:
					_screen = new GameScreen(this);
					break;
			}
		}
	}
}
