<a name="HOLTop" />
# Introduction to MonoGame #

---

<a name="Overview" />
## Overview ##

MonoGame is a free open source, cross platform gaming Framework. Developers using this framework can target Windows 10, MacOS, Linux, Android and iOS. All of which can use the same code base. This session will take you through the process of building a sample arcade game from scratch.

<a name="Objectives" />
### Objectives ###

In this module, you will see how to:

- Process your game assets to make sure they are optimized for your target platform
- Load game assets
- Create a Sprite Base class
- Handle Input from Keyboard 
- Render textures to screen

<a name="Prerequisites"></a>
### Prerequisites ###

The following is required to complete this module:

- [Visual Studio Community 2015][1] or greater.
- [MonoGame v3.5][2] or greater.

[1]: https://www.visualstudio.com/products/visual-studio-community-vs
[2]: http://www.monogame.net/downloads/

<a name="Setup"></a>
### Setup ###
Throughout the module document, you'll be instructed to insert code blocks. For your convenience, most of this code is provided as Visual Studio Code Snippets, which you can access from within Visual Studio 2015 to avoid having to add it manually. To install the code snippets run the setup script:

1. Open Windows Explorer and browse to the module's **Source** folder.
1. Right-click **Setup.cmd** and select **Run as administrator** to launch the setup process that will configure your environment and install the Visual Studio code snippets for this module.
1. If the User Account Control dialog box is shown, confirm the action to proceed.

> **Note**: Each exercise is accompanied by a starting solution located in the **Begin** folder of the exercise, when applicable, that allows you to follow each exercise independently of the others. Please be aware that the code snippets that are added during an exercise are missing from these starting solutions and may not work until you've completed the exercise. Inside the source code for an exercise, you'll also find an **End** folder, when applicable, containing a Visual Studio solution with the code that results from completing the steps in the corresponding exercise. You can use these solutions as guidance if you need additional help as you work through this module.

---

<a name="Exercises" />
## Exercises ##

This module includes the following exercise:

1. [Creating your first game](#Exercise1)

Estimated time to complete this module: **60 minutes**

> **Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.

<a name="Exercise1" />
### Exercise 1: Creating your first game ###

In this exercise you will create your first MonoGame Windows 10 project. 

<a name="Ex1Task1" />
#### Task 1 - Creating the Project ####

To get started we need to first create a new "MonoGame Windows 10 Universal Project".

1. Got to **File->New Project**.

1. File the MonoGame section in **Visual C#->MonoGame**.

1. Select **MonoGame Windows 10 Universal Project**.

1. Change the name to "_AlienAttackUniversal_".

1. Select where you want to save the solution. It will default to **c:\users\<user>\documents\visual studio 2015\Projects**.

1. Click **OK**.

If you take a look at the code, you will see allot of "using Microsoft.Xna.Framework" type using clauses... what's going on there? This is MonoGame right? Yes, this is MonoGame :) It is a XNA API compatible cross platform gaming framework. To be XNA API compatible it needs to use the same namespaces. So if you have used XNA in the past you will feel right at home with MonoGame.

<a name="Ex1Task2" />
#### Task 2 - Renaming Game1.cs ####

The default project will create a class called Game1. While you can leave this as it is, its good practice to rename this class. 

1. Right-click on **Game1.cs** and select **Rename**.

1. Type in **AlienAttackGame.cs**. You should see a dialog asking if you want to rename all references in the project.

1. Click **Yes**.

<a name="Ex1Task3" />
#### Task 3 - Adding Assets ####

No game would be complete without graphics and sound. Fortunately, this session comes with a complete set of assets for you to use. In order to get the maximum performance for your game you will need to pre-process the assets into an optimized format. This can be done using the MonoGame Pipeline Tool. Its purpose is to task assets like .png/.wav and compress/optimize them for the platform you are targeting. In this case Windows 10. This will allow you to take advantage of things like texture compression which in turn will allow you to include more graphics! and who doesn't like more graphics.

1. Click on the **Content** folder in the Solution to expand it.

1. Double click on **Content.mgcb**. This should open the file in the MonoGame Pipeline Tool. If the file opens in the code editor. Right-click on **Content.mgcb** and click **Open With**. The select the **MonoGame Pipeline Tool**.

1. Go to **Edit->Add->Existing Folder**.

1. Navigate to **End\AlienAttackUniversal\Content\gfx**.

1. Click **Add**. A dialog will pop up to ask if you want to copy or link the files. Select **Copy** and click **OK**.

1. Go to **Edit->Add->Existing Folder**.

1. Navigate to **End\AlienAttackUniversal\Content\sfx**.

1. Click Add. A dialog will pop up to ask if you want to copy or link the files. Select Copy and click **OK**.

1. Go to **Edit->Add->Existing Item**.

1. Navigate to **End\AlienAttackUniversal\Content\Font.spritefont**.

1. Click **Add**. A dialog will pop up to ask if you want to copy or link the files. Select **Copy** and click **OK**.

<a name="Ex1Task4" />
#### Task 4 - Loading a Texture ####

Now that all the assets have been added to our Content.mgcb file they will be compiled and automatically included in our application package. Our next task is to use the Content manager to load a texture. The code we are about to add will be removed later but it will demonstrate how easy it is to load assets.

1. Open **AlienAttackGame.cs** if it is not already open.

1. Add the following code just under the **SpriteBatch** declaration on line 12.
	
	````C#
	Texture2D background;
	````

1. Navigate to the **LoadContent** method on line 42 and add the following code

	````C#
	background = Content.Load<Texture2D> ("gfx/bgScreen");
	````

That is all there is to it. The same can be applied to SoundEffect/Meshes etc. The Content.Load method is a generic method so we just specify the type we are trying to load. If the types do not match an error will be raised. For example, if we try to load a sound effect and pass <Texture2D> we will get an error.

<a name="Ex1Task5" />
#### Task 5 - Drawing a Texture ####

To render any 2 dimensional (2D) textures we can make use of the SpriteBatch class. This class is designed to draw textures and text to the screen. You can use it to efficiently *batch* the drawing of items, this helps to make sure you are making the most productive use of the video resources. The game template creates a spriteBatch for you.

The **SpriteBatch** has **.Begin** and **.End** methods. You use these to define a *batch*, all the calls to the various **.Draw** methods between a **.Begin/.End** pair will be optimized and sent to the GPU as efficiently as possible. Let's draw the texture we loaded in Task 4.

1. In the **AlienAttachGame.cs** file, find the Draw method. It should contain the following code

	````C#
	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		// TODO: Add your drawing code here

		base.Draw(gameTime);
	}
	````

1. Under the **//TODO** section add the following code

	(Code Snippet - _IntroMonoGame - DrawBackground_)

	````C#
	spriteBatch.Begin();
	spriteBatch.Draw(background, Vector2.Zero, Color.White);
	spriteBatch.End();
	````

1. Make sure your selected build configuration is set to "Debug" "x64".

1. Hit **F5** or click the Run **Local Machine** button.

Congratulations, you just loaded and rendered your first texture!

<a name="Ex1Task6" />
#### Task 6 - Drawing Text ####

Drawing text is similar to drawing a texture. We first need to load a font, and then use the SpriteBatch to render the text.

1. Go back to where we declared the background **Texture2D** and add the following code.

	````C#
	SpriteFont font;
	````

1. Go to the LoadContent method and add the following underneath were we loaded the texture.

	````C#
	font = Content.Load<SpriteFont>("Font");
	````

1. Next modify the Draw code we pasted earlier to include a call to spriteBatch.DrawString just underneath the call to draw our texture.

	````C#
	spriteBatch.DrawString(font, "MonoGame Rocks!", Vector2.Zero, Color.White);
	````

1.  Hit **F5** or click the Run **Local Machine** button.

You should see the text in the top left corner of the screen. Note the order of the batch items is important. If we had put the call to DrawString above the line which draws the texture it would not be show. This is because the texture would be drawn after the text. Because the texture takes up the entire screen it will overwrite the text.  

<a name="Ex1Task7" />
#### Task 7 - Playing Music ####

No game would be complete without Sound Effects and Music. MonoGame has two classes which make working with these items easier. SoundEffect and Song. SoundEffect is usually used to play short audio items, Song is used to play longer items that might need to be streamed. Loading both of these items is done in the same way we load textures and fonts.

Depending on the format of your audio will depend on the default content *Processor* that is used to optimize the content. For .wav files the Sound Effect processor is used, for files like .wma the Song processor is used. If you have a .wav file that you want to use for music, you can always change the processor it used in the Pipeline Tool to be "Song - MonoGame".

Let's load the music for the game.

1. Go back to where we declared the background **Texture2D** and add the following code.

	````C#
	Song song;
	````

1. Right-click on the "Song" class and use the **Quick Actions** to add a using for _Microsoft.Xna.Framework.Media_ or add it manually.

	````C#
	using Microsoft.Xna.Framework.Media;
	````

1. Go to the **LoadContent** method and add the following underneath were we loaded the texture.

	(Code Snippet - _IntroMonoGame - LoadAndPlaySong_)

	````C#
	song = Content.Load<Song>("sfx/theme");
	MediaPlayer.IsRepeating = true;
	MediaPlayer.Play(song);
	````

1. Hit **F5** or click the Run **Local Machine** button.

You should hear the music playing when the app starts.

<a name="Ex1Task8" />
#### Task 8 - Playing Sound ####

1.  Go back to where we declared the background Texture2D and add the following code.

	````C#
	SoundEffect playerShot;
	````

1. Right-click on the **SoundEffect** class and use the **Quick Actions** to add a using for _Microsoft.Xna.Framework.Audio_ or add it manually.

	````C#
	using Microsoft.Xna.Framework.Audio;
	````

1. Go to the LoadContent method and add the following underneath were we loaded the texture.

	(Code Snippet - _IntroMonoGame - LoadAndPlayShot_)

	````C#
	playerShot = Content.Load<SoundEffect>("sfx/playerShot");
	playerShot.Play();
	````

1. Hit **F5** or click the Run **Local Machine** button.
 
When the application starts you should hear the playerShot sound effect play. 

<a name="Ex1Task9" />
#### Task 9 - Using the DrawableGameComponent class ####

At this point you know how to load the basic assets needed to write a game. There are other assets like Shaders and custom content but to get started all you need is Textures, Fonts, SoundEffects and Music. We can now start writing the game. Our game will have a couple of screens. A Title screen and a Game screen. Each will be responsible for rendering itself and responding to player input. To implement these, we are going to use the *DrawableGameComponent* class. MonoGame has a concept of *Components* which you can add to the game  and will automatically update and render themselves. First, there is the GameComponent which can be used for code that just needs to update every frame. The DrawableGameComponent adds a virtual Draw method which can be used to draw items. Since both our Title screen and Game screen will need to draw graphics they will both derive from DrawableGameComponent.

1. In the **Solution Explorer** right-click on the **AlienAttackUniveral** project and click **Add->New Folder**. Name this folder _Screens_.

1. Right-click on the Screens folder and click **Add->Class**. Call this class **TitleScreen.cs**.

1. Add the following using clauses to the top of the **TitleScreen.cs**. These will import the required MonoGame namespaces.

	(Code Snippet - _IntroMonoGame - TileScreenUsings_)

	````C#
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	````

1. Change the **TitleScreen** class so it derives from **DrawableGameComponent**.

1. Next, add a constructor. The base GameComponent class _requires_ a Game instance parameter for its constructor to our constructor needs to look like the following.

	````C#
	public TitleScreen(Game game) : base(game)
	{
	}
	````

1. Next, we need to add some private fields to hold Textures, Fonts and a SpriteBatch with which to draw them.

	(Code Snippet - _IntroMonoGame - TileScreenTextureAndSpritesFields_)

	````C#
	private readonly Texture2D _titleScreen;
	private readonly SpriteFont _font;
	private readonly SpriteBatch _spriteBatch;
	````

1. Now, update the constructor to create/load these items.

	(Code Snippet - _IntroMonoGame - TileScreenConstructor_)

	````C#
	public TitleScreen(Game game) : base(game)
	{
		_spriteBatch = new SpriteBatch(game.GraphicsDevice);
		_titleScreen = game.Content.Load<Texture2D>("gfx\\titleScreen");
		_font = game.Content.Load<SpriteFont>("Font");
	}
	````

1. Finally, we need to override the Draw method. The Draw (and Update) method takes a GameTime parameter. This is how we keep track of time in the game. It holds how much time has elapsed since the game started and the elapsed time since the last frame. We'll use this information later in our GameScreen. For now, the Draw method on the TitleScreen just needs to render the texture we loaded as some text.

	(Code Snippet - _IntroMonoGame - TileScreenDraw_)

	````C#
	public override void Draw(GameTime gameTime)
	{
		_spriteBatch.Begin();
		_spriteBatch.Draw(_titleScreen, Vector2.Zero, Color.White);
		_spriteBatch.DrawString(_font, "Press Enter or Tap to Play", new Vector2(1200, 960), Color.White);
		_spriteBatch.End();
	}
	````

In the next section we will update the AlienAttackGame class to make use of this new class. 

<a name="Ex1Task10" />
#### Task 10 - Updating the Game class ####

Now that we have a basic TitleScreen we need to be able to render it. To do that we need to remove all of the code we added to the AlienAttackGame class earlier and add a few new methods.

1. Open the **AlienAttackGame.cs** if it is not already open.

1. Add a using clause for _AlienAttackUniversal.Screens_.

	````C#
	using AlienAttackUniversal.Screens;
	````

1. Select the _entire_ **AlienAttackGame** _class_ and replace it with the following.
 
	(Code Snippet - _IntroMonoGame - AlienAttackGame_)

	````C#
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class AlienAttackGame : Game
	{        
		public AlienAttackGame()
		{            
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
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
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
		}
	}
	````

1. We need to create an enumeration to define the current _screen_ in the game. Add the following code above the **AlienAttackGame** class.

	(Code Snippet - _IntroMonoGame - GameStateEnum_)

	````C#
 	public enum GameState
	{
		TitleScreen,
		GameScreen
	};
	````

1. Now, we need to declare a few properties to store which screen we are on and allow access to the instance of the game from anywhere. We also want to define our screen resolution that we want to use. Add the following to the top of the **AlienAttackGame** class.

	(Code Snippet - _IntroMonoGame - TileScreenFields_)

	````C#
	public static AlienAttackGame Instance;
	private readonly GraphicsDeviceManager _graphics;
	private DrawableGameComponent _screen;
	public static int ScreenWidth = 1920;
	public static int ScreenHeight = 1080;
	````


1. Next, update the constructor to create/set all of these properties.

	(Code Snippet - _IntroMonoGame - TileScreenSetProperties_)

	````C#
	Instance = this;
	_graphics = new GraphicsDeviceManager(this);
	// set our screen size based on the device
	_graphics.PreferredBackBufferWidth = ScreenWidth;
	_graphics.PreferredBackBufferHeight = ScreenHeight;
	Content.RootDirectory = "Content";
	````

	Note we set the static Instance field to this. Using this field, we can access the *instance* of the game just by using AlienAttackGame.Instance. We also create our graphics device manager and set our preferred resolution. The Content manager is also given the "RootDirectory" which is the base directory it will look for the compiled assets.

1. Next, we need to add a method which will allow us to change which screen we are showing. This method is called SetState and will update the **_screen** field we just created. Add the following code somewhere near the bottom of the **AlienAttackGame** class.

	(Code Snippet - _IntroMonoGame - TileScreenSetState_)

	````C#
	public void SetState(GameState newState)
	{
		switch(newState)
		{
			case GameState.TitleScreen:
				_screen = new TitleScreen(this);
				break;
			case GameState.GameScreen:
				break;
		}
	}
	````

	This method currently only updates the **_screen** field for a TitleScreen gamestate. We'll add code for the GameScreen later.

1. Now, find the Initialize method and add a call to SetState (GameState.TitleScreen). This is to that when the game starts it will default to the title screen. Initialize is only called when the game starts.

	(Code Snippet - _IntroMonoGame - TileScreenInitialize_)

	````C#
	protected override void Initialize()
	{
		SetState(GameState.TitleScreen);
		base.Initialize();
	}
	````


1. Add the following code to the Update method. This will make sure that our _current_ screen is updated every frame.

	````C#
	_screen.Update(gameTime);
	````

1. Finally, add the following code to the Draw method. This will draw our _current_ screen.

	````C#
	_screen.Draw(gameTime);
	````


1. Hit **F5** or click the Run **Local Machine** button.

You should now see the title screen rendered.

<a name="Ex1Task11" />
#### Task 11 - Using RenderTargets ####

We managed to get our title screen rendering in the last Task. But it wasn't scaling to fit the screen? If you resized the window it didn't stretch to fill it. What is going on?

Well spriteBatch is a low level drawing system. It doesn't care about scaling or fitting graphics to a window. So we need to somehow scale our graphics ourselves to adapt to the various screen resolutions devices can have. Remember Windows 10 supports everything from a Phone right up to an Xbox One with a 4K TV on it. We need a way to handle ALL of those devices. 

SpriteBatch can take a scale matrix which it can use to scale the coordinates and graphics passed to it. But this method can be a bit messy. Wouldn't it be great if there was a way to draw at a fixed known resolution and then scale that? Well, it turns out there is; it's called a **RenderTarget**. Simply put a RenderTarget is a texture you can draw to. When to tell the graphics device to use a RenderTarget you are basically telling it to not draw on the screen but onto the texture. Now because a RenderTarget derives from Texture we can then use it with a spriteBatch to draw but scale the results. This way we can draw our game at a fixed resolution. 

What we will do next is add a new helper class called _RenderTargetScaler_ which we can then use to scale our entire game to fit the window.

1. Right-click on the AlienAttackUniversal Project in the solution explorer and click **Add->Class** and add a "RenderTargetScaler.cs".

1. Add the following using clauses to the top of **RenderTargetScaler.cs** to bring in the required namespaces.

	(Code Snippet - _IntroMonoGame - RenderTargetScalerUsings_)

	````C#
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	````

1. Next, we need a few fields for our scaler. The most important of which is our _drawBuffer which is a RenderTarget2D. This is what we will draw our entire game to before using the scaler to draw the _drawBuffer on the screen.

	(Code Snippet - _IntroMonoGame - RenderTargetScalerFields_)

	````C#
	private readonly RenderTarget2D _drawBuffer;
	private readonly GraphicsDeviceManager _graphicsDeviceManager;
	private readonly int _screenWidth;
	private readonly int _screenHeight;
	private readonly Game _game;
	private readonly SpriteBatch _spriteBatch;
	````

	Note there is another SpriteBatch in this list. Normally you will want to keep the number of SpriteBatch instances to a minimum, this is because they maintain an internal list of items that need to be rendered. It's more memory efficient to only have one or two of them. 

1. Next, we need to add a constructor. This will need to take an instance of the Game class so we can get things like the Window, the **GraphicsDeviceManager** we created in the **AlienAttackGame** class and the resolution we are going to draw our game in.

	(Code Snippet - _IntroMonoGame - RenderTargetScalerConstructor_)

	````C#
	public RenderTargetScaler(Game game, GraphicsDeviceManager graphicsDeviceManager, int width, int height)
	{
	}
	````

1. Next, we set all of our local fields and create an instance of the _spriteBatch in the new constructor we just added.

	(Code Snippet - _IntroMonoGame - RenderTargetScalerSetFields_)

	````C#
	_game = game;            
	_graphicsDeviceManager = graphicsDeviceManager;
	_screenWidth = width;
	_screenHeight = height;
	_spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice); 
	````

1. We now need to create our render target. This requires the GraphicsDevice, and our target width and height.

	````C#
	_drawBuffer = new RenderTarget2D(graphicsDeviceManager.GraphicsDevice, width, height);
	````

1. That is all we need in the constructor. Next we need a way to tell the GraphicsDevice to use the render target not the screen. We can do that via the GraphicsDevice.SetRenderTarget method. This method takes a RenderTarget2D instance or a null, if we pass null that tells the GraphicsDevice to draw to the screen. If we pass an instance it will use the RenderTarget. We'll add a helper method to make things a bit easier for ourselves.

	(Code Snippet - _IntroMonoGame - RenderTargetScalerSetRenderTarget_)

	````C#
	public void SetRenderTarget()
	{
		_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(_drawBuffer);
	}
	````

Any spriteBatch draw calls made after this method is called will draw on the **_drawBuffer**. As soon as we call SetRenderTartet (null) it will revert back to drawing on the screen. We'll add that in our next helper method.

1. Now we need a Draw method. This is where we will calculate our scaling factor, clear the render target (using null), then use the spriteBatch to draw our **_drawBuffer** to the screen. For now, just add the new Draw method.

	````C#
	public void Draw ()
	{
	}
	````

In the next section we'll figure out our scaling factors and fill in the rest of the Draw method.

<a name="Ex1Task12" />
#### Task 12 - Scaling to Screen ####

In the previous section we added a helper class to handle scaling our game. When scaling we will want to use a technique called Letterboxing. This is where rather than just stretching the texture so it fills the entire window, we stretch it so we maintain the aspect ratio of the texture. For example, if we have a texture that is 1024x768 (4:3) and stretched it to a window that was 1280x720 (16:9) that would look really weird and distorted. So what we do it create a "letterbox" by adding padding to either the top/bottom or the left/right to make sure we maintain the original aspect ratio of the texture. 

1. You should already be in the Draw method. The first thing we need to do is figure out the current aspect ratios for our window and our fixed resolution we are drawing at. We do that by dividing the width by the height.

	(Code Snippet - _IntroMonoGame - ScreenAspect_)

	````C#
	float outputAspect = _game.Window.ClientBounds.Width / (float)_game.Window.ClientBounds.Height;
	float preferredAspect = _screenWidth / (float)_screenHeight;
	````

1. Add a Rectangle called dst. This will hold our destination rectangle which is where we will draw the **_drawBuffer**. We will calculate this in the next step.

	````C#
	Rectangle dst;
	````

1. Next, we need to decide if we need to add padding to the top or the sides to get our letterbox effect. Of our *outputAspect* is less than the *preferredAspect* we need to pad the top and bottom. Otherwise we need to add the padding to the left and right.

	(Code Snippet - _IntroMonoGame - ScreenAspectPadding_)

	````C#
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
	````

1. Now, we have the dst Rectangle calculated we can start drawing the _drawBuffer. But before we do we need to make sure the GraphicsDevice is NOT using the render target.

	````C#
	_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
	````

1. Next, we want to clear the screen with a nice Black color. You could use a different color but black is normally used because it matches the default color of a screen :).

	````C#
	_graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
	````

1. Finally, we use the spriteBatch to draw our _drawBuffer. Note this time we are using the dst Rectangle rather than a Vector2 to define where on the screen this texture will be drawn. The SpriteBatch will scale the texture to fill that Rectangle, and because we took care to calculate our Rectangle based on the aspect ratio it will look correct.

	(Code Snippet - _IntroMonoGame - ScreenDrawBuffer_)

	````C#
	_spriteBatch.Begin();
	_spriteBatch.Draw(_drawBuffer, dst, Color.White);
	_spriteBatch.End();
	````

1. We now have all the bits in place in our helper class. Now go back to the AlienAttackGame class and add field for the RenderTargetScaler below all the other fields.

	````C#
	private RenderTargetScaler _scaler;
	````

1. In the AlienAttackGame Initialize method create an instance of the **_scaler** just BEFORE we call SetGameState.

	````C#
	_scaler = new RenderTargetScaler(this, _graphics, ScreenWidth, ScreenHeight);
	````

1. Finally, update the Draw method of the AlienAttackGame to match the following code 

	(Code Snippet - _IntroMonoGame - ScreenDrawGameTime_)

	````C#
	protected override void Draw(GameTime gameTime)
	{
		_scaler.SetRenderTarget();
		_screen.Draw(gameTime);
		_scaler.Draw();
	}
	````

Note how we call **_scaler.SetRenderTarget()** first then after we draw the screen, we then call **_scaler.Draw**. This will make sure the screen is correctly scaled for the window.

<a name="Ex1Task13" />
#### Task 13 - Handling Input ####

In this task we will add input support for our game. MonoGame comes with a complete set of classes for getting input such as GamePads, Keyboard, Mouse and TouchScreens. All of these implement a similar *state* based API. This allows you to get the current state of the input device. Because games are generally not event driven we will be calling these *GetState* methods every frame. 

Rather than littering our game code with lots of different input methods we will wrap them all up into on InputManager class. This will have one **Update** method to retrieve the current input state. It will then expose this state via some static variables so we can access it through the game code. 

1. Right-click on the AlienAttackUniversal Project in the solution explorer and click Add->Class and add a "InputManager.cs". Then change the InputManager class to be static.

	````C#
	static class InputManager
	{
	}
	````

1. Add the following using clauses to the top of the InputManager.cs class file.

	(Code Snippet - _IntroMonoGame - InputManagerUsings_)

	````C#
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Input;
	using Microsoft.Xna.Framework.Input.Touch;
	````

1. Add the new ControlState enumeration just above the InputManager class.  This structure will be exposed as a static and will let us know which controls are active.

	(Code Snippet - _IntroMonoGame - ControlState_)

	````C#
	public struct ControlState
	{
		public bool Left;
		public bool Right;
		public bool Start;
		public bool Quit;
		public bool Fire;
		public float FingerPosition;
	}
	````

1. We now need to declare some internal fields to hold our control _state_. Note we store the current AND previous state for Keyboard and GamePads. This allows us to calculate what has changed between frames.
 
	(Code Snippet - _IntroMonoGame - InputManagerFields_)

	````C#
	private static KeyboardState _keyboardState, _lastKeyboard;
	private static GamePadState _gamePadState, _lastGamePad;
	private static ControlState _controlState;
	````

1. Next, we need a static constructor. In this we will tell the TouchPanel class which *Gestures* we can enable. In this case if a user taps the screen or id they drag horizontally. We will use the former to handle firing and the latter for moving the player.
 
	(Code Snippet - _IntroMonoGame - InputManagerConstructor_)

	````C#
	static InputManager()
	{
		TouchPanel.EnabledGestures = GestureType.Tap | GestureType.HorizontalDrag;
	}
	````


1. It's time to add the update method. This needs to be public and static so it can be called from anywhere in the game.

	````C#
	public static void Update()
	{
	}
	````

1. Before we fill in the Update method we will add the public static property for the **ControlState** structure we declared earlier.

	(Code Snippet - _IntroMonoGame - InputManagerControlStateProperty_)

	````C#
	public static ControlState ControlState
	{
		get { return _controlState; }
	}
	````

1. Now, for the interesting bit. We need to retrieve the current state for the all the inputs then calculate the ControlState. First this to do is to get the Keyboard and GamePad states. Add the following code to the Update method.

	(Code Snippet - _IntroMonoGame - InputManagerKeyboardState_)

	````C#
	_keyboardState = Keyboard.GetState();
	_gamePadState = GamePad.GetState(PlayerIndex.One);
	````

1. Now we need to set the ControlState fields depending on the states we got. For example, the following code.

	````C#
	_controlState.Quit = (_gamePadState.Buttons.Back== ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Escape));
	````

1. Sets the "Quit" field if the Gamepad "Back" buttons is pressed OR is the Escape Key is pressed on the keyboard. 

1. Add the following code to the Update method.

	(Code Snippet - _IntroMonoGame - InputManagerUpdateControlState_)

	````C#
	_controlState.Quit = (_gamePadState.Buttons.Back == ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Escape));
	_controlState.Start = (_gamePadState.Buttons.B == ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Enter) || _keyboardState.IsKeyDown(Keys.Space) || _gamePadState.IsButtonDown(Buttons.Start));
	_controlState.Left = (_gamePadState.DPad.Left == ButtonState.Pressed || _gamePadState.ThumbSticks.Left.X < -0.1f || _keyboardState.IsKeyDown(Keys.Left));
	_controlState.Right = (_gamePadState.DPad.Right == ButtonState.Pressed || _gamePadState.ThumbSticks.Left.X > 0.1f || _keyboardState.IsKeyDown(Keys.Right));
	_controlState.Fire = ((_gamePadState.Buttons.B == ButtonState.Pressed && _lastGamePad.Buttons.B == ButtonState.Released) || (_keyboardState.IsKeyDown(Keys.Space) && !_lastKeyboard.IsKeyDown(Keys.Space)));
	````
        
1. That covers the GamePad and Keyboard. Next is the Touch Gestures. The TouchPanel class exposed s IsGestureAvailable property which tells us if a gesture was detected. It also provides a ReadGesture method to read the current gesture. Note Gestures are queued so it is possible to get multiple gestures in on frame (e.g., a Tap and a Drag). So we need to loop through until IsGestureAvailable is false. Add the following code to the Update Method
 
	(Code Snippet - _IntroMonoGame - InputManagerTouchPanel_)

	````C#
	while(TouchPanel.IsGestureAvailable)
	{
		GestureSample gs = TouchPanel.ReadGesture();
		_controlState.Fire |= (gs.GestureType == GestureType.Tap);
		_controlState.Start |= (gs.GestureType == GestureType.Tap);
		if(gs.GestureType == GestureType.HorizontalDrag)
		{
			_controlState.Left = (gs.Delta.X < 0);
			_controlState.Right = (gs.Delta.X > 0);
			_controlState.FingerPosition = gs.Position.X;
		}
	}
	````

	You should be able to see we just loop while we have gestures, and depending on the gesture type we will set the control state. If we get a horizontal drag, we will set the Left/Right control state fields depending on which direction the drag was it. We also store the current position for later.

1. Finally, we need to store the current state in the _last*State fields so we can detect changes in the next frame.

	(Code Snippet - _IntroMonoGame - InputManagerStoreCurrentState_)

	````C#
	_lastGamePad = _gamePadState;
	_lastKeyboard = _keyboardState;
	````

1. That covers the InputMananger. We can now use code like the following anywhere in the game to check the input.

	(Code Snippet - _IntroMonoGame - InputManagerQuit_)

	````C#
	If (InputManager.Controlstate.Quit) {
		// quit the game
	}
	````

Our next task is the Audio Manager.

<a name="Ex1Task14" />
#### Task 14 - Handling Audio ####

Just like the InputManager the audio manager will be responsible for handling the loading and playing of all audio in the game. 

1. Right-click on the AlienAttackUniversal Project in the solution explorer and click **Add->Class** and add a **AudioManager.cs**. Then, change the **AudioManager** class to be static.

	````C#
	static class AudioManager
	{
	}
	````

1.  Add the following using clauses to the top of the AudioManager.cs file.

	(Code Snippet - _IntroMonoGame - AudioManagerUsings_)

	````C#
	using Microsoft.Xna.Framework.Audio;
	using Microsoft.Xna.Framework.Media;
	````

1. Add the following Cue enumeration to the AudioManager class. This will let us tell the Manager which sound effect we want to play.

	(Code Snippet - _IntroMonoGame - CueEnum_)

	````C#
	public enum Cue
	{
		EnemyShot,
		PlayerShot,
		Explosion
	};
	````


1. Now we need to add the fields which we will store the sounds in. We will use Song for the music and SoundEffect for each of the game sounds we want to play. 

	(Code Snippet - _IntroMonoGame - AudioManagerFields_)

	````C#
	private static readonly Song _theme;
	private static readonly SoundEffect _enemyShot;
	private static readonly SoundEffect _playerShot;
	private static readonly SoundEffect _explosion;
	````


1. Next, we will add a static constructor to load all the audio from the ContentManger. We'll use the Instance field of the Game to get access to the game instance then call the Content.Load methods for each sound.

	(Code Snippet - _IntroMonoGame - AudioManagerConstructor_)

	````C#
	static AudioManager()
	{
		_theme = AlienAttackGame.Instance.Content.Load<Song>("sfx\\theme");           
		_enemyShot = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\enemyShot");
		_playerShot = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\playerShot");
		_explosion = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\explosion");
	}
	````

1. We now need a way to play the sounds. This is where the Cue enumeration comes in. We'll add a new method "PlayCue" which will take the Cue enum as a parameter. We can then use the switch statement to play the correct sound.

	(Code Snippet - _IntroMonoGame - AudioManagerPlayCue_)

	````C#
	public static void PlayCue(Cue cue)
	{
		// play the effect requested
		switch(cue)
		{
			case Cue.EnemyShot:
				_enemyShot.Play();
				break;
			case Cue.PlayerShot:
				_playerShot.Play();
				break;
			case Cue.Explosion:
				_explosion.Play();
				break;
		}
	}
	````

1. Finally, we need a couple of methods to start and stop the theme music. 

	(Code Snippet - _IntroMonoGame - AudioManagerStartStopTheme_)

	````C#
	public static void StartTheme()
	{
		MediaPlayer.IsRepeating = true;
		MediaPlayer.Play(_theme);
	}

	public static void StopTheme()
	{
		MediaPlayer.Stop();
	}
	````

This is it for the AudioManager. 

<a name="Ex1Task15" />
#### Task 15 - Animating Sprites ####

We now have a lot of the basic plumbing in place we can start looking and implementing our players and enemies for the game. We will want to have animated items or sprite because games without animation are kind of dull. Rather than duplicating a bunch of code for enemies and the player we will create a base class which will have most of that logic. There are excellent third party sprite animation systems out there which make this job allot easier. But for this Lab we'll only use what is available in MonoGame.

To animate our sprites, we will use an array of Texture2D's to hold the Texture for each frame. Then just increment an index to change which frame is show. Once we get to the end of the array we will rest the index back to 0 and start again. This is an extremely simple way of animating sprites.

1. In the solution explorer right-click on the AlienAttackUniveral project and click **Add->New Folder**. Name this folder _Sprites_.

1. Right-click on the Sprites folder and click **Add->Class**. Call this class _Sprite.cs_.

1. Add the following using clauses.

	(Code Snippet - _IntroMonoGame - SpriteUsings_)

	````C#
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Content;
	using Microsoft.Xna.Framework.Graphics;
	````

1. First we'll add the properties we need. The first section will hold the information we need for holding and indexing the animations.

	(Code Snippet - _IntroMonoGame - SpriteProperties_)

	````C#
	// all textures in animation set
	protected Texture2D[] Frames { get; set; }
	// current frame to draw
	protected int FrameIndex { get; set; }
	// total number of frames
	protected int FrameCount { get; set; }
	````

1. Next, we'll add some code which defines the position, size, velocity and Scale of the sprite. 

	(Code Snippet - _IntroMonoGame - SpriteSpatialProperties_)

	````C#
	// size of sprite
	public Vector2 Size { get { return new Vector2(Width, Height); } }
	public int Width { get; set; }
	public int Height { get; set; }
	public Vector2 Position { get; set; }
	public Vector2 Velocity { get; set; }
	public float Rotation { get; set; }
	public Vector2 Scale { get; set; }
	````

1. Now, we add a few additional fields for keeping track of the elapsed time for animating and the bounding box for handling collisions. The _animationDirection field will be used to left us reverse the direction of the animation.

	(Code Snippet - _IntroMonoGame - SpriteTimeAndBoudFields_)

	````C#
	// variable to track number of millieconds for animations
	private double _time;
	// bounding box of sprite...used for collisions
	private Rectangle _boundingBox;
	private int _animationDirection = 1;
	````

1. We need a constructor to set the initial value for the Scale.

	(Code Snippet - _IntroMonoGame - SpriteConstructor_)

	````C#
	public Sprite()
	{
		Scale = Vector2.One;
	}
	````

8. We need to be able to load textures for our sprite. Our animated graphics are stored on disk in the format `<name>_<frame>`. So we need two LoadContent methods. One to load a normal non animated sprite and one which will load a group of textures into the array.

	(Code Snippet - _IntroMonoGame - SpriteLoadContent_)

	````C#
	public virtual void LoadContent(ContentManager contentManager, string name)
	{
		// load single frame
		Frames = new Texture2D[1];
		Frames[0] = contentManager.Load<Texture2D>(name);
		Width = Frames[0].Width;
		Height = Frames[0].Height;
	}

	public virtual void LoadContent(ContentManager contentManager, string name, int count)
	{
		// load multiple frames
		Frames = new Texture2D[count];
		for(int i = 0; i < count; i++)
			Frames[i] = contentManager.Load<Texture2D>(string.Format(name, i));
		FrameCount = count;
		Width = Frames[0].Width;
		Height = Frames[0].Height;
	}
	````

1. Next, is a couple of methods we will use to animate the sprite. These will be called as part of the Update method and will take a GameTime so we can accurately track how much time has passed. It will also take a value defining how long a *frame* should last before switching to the next one.

	(Code Snippet - _IntroMonoGame - SpriteAnimateLoop_)

	````C#
	public virtual void AnimateLoop(GameTime gameTime, double frameTime)
	{
		// count number of milliseconds
		_time += gameTime.ElapsedGameTime.TotalMilliseconds;
		// if we're over the time for the next frame, move on
		if(_time > frameTime)
		{
			FrameIndex++;
			_time -= frameTime;
		}
		// if we're past the # of frames, start back at 0
		if(FrameIndex > FrameCount-1)
			FrameIndex = 0;
	}

	public virtual void AnimateReverse(GameTime gameTime, double frameTime)
	{
		// same as above, but reverse direction instead of starting back at 0
		_time += gameTime.ElapsedGameTime.TotalMilliseconds;
		if(_time > frameTime)
		{
			_time -= frameTime;
			if(FrameIndex == 0)
				_animationDirection = 1;
			else if(FrameIndex == FrameCount-1)
				_animationDirection = -1;
			FrameIndex += _animationDirection;
		}
	}
	````

1. Next up is the Update method. Again this will take a GameTime so we can move the player depending on how much time has elapsed since the last frame. If we just incremented the position by Velocity each frame, then the game would run faster on a faster PC than it does on a slower one. This is because the faster PC will be calling Update/Draw more often. This is one of the most important things to remember when writing games. You frame rate is not always going to be the same on every machine.

	(Code Snippet - _IntroMonoGame - SpriteUpdate_)

	````C#
	public virtual void Update(GameTime gameTime)
	{
		// move the sprite 1 velocity unit
		Position += Velocity * gameTime.ElapsedGameTime.Milliseconds;
	}
	````

1. The draw method is really simple. We just use a passed in spritebatch to draw the current frame. If we don't have any frames, we just exit out of the method.

	(Code Snippet - _IntroMonoGame - SpriteDraw_)

	````C#
	public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		if(Frames == null)
			return;
		spriteBatch.Draw(Frames[FrameIndex], position:Position, color:Color.White, scale:Scale);
	}
	````


1. Finally, we need to expose the BoundingBox. Remembering that as the sprite moves location on the screen its bounding box location will change.
	
	(Code Snippet - _IntroMonoGame - BoundingBox_)

	````C#
	public virtual Rectangle BoundingBox
	{
		get 
		{
			// only need to assign this once
			if(_boundingBox == Rectangle.Empty)
			{
				_boundingBox.Width = Width;
				_boundingBox.Height = Height;
			}
			_boundingBox.X = (int)Position.X;
			_boundingBox.Y = (int)Position.Y;
				return _boundingBox;
		}
	}
	````


That is our sprite base class complete. We can now start adding our Player.

<a name="Ex1Task16" />
#### Task 16 - Adding the Player ####

Because we now have a base class for our sprites adding new sprites is going to be very straightforward.

1. Right-click on the **Sprites** folder and click **Add->Class**. Call this class **Player.cs**.


1. Change the **Player** so it derives from **Sprite**.

	````C#
	class Player : Sprite	
	{
	}
	````

1. We now need to load the textures for this sprite. So let's just add a constructor and call LoadContent in it.

	(Code Snippet - _IntroMonoGame - PlayerConstructor_)

	````C#
	public Player()
	{
		LoadContent(AlienAttackGame.Instance.Content,  "gfx\\player\\player");
	}
	````


That is it! All the other logic is handled in the Sprite class. 

<a name="Ex1Task17" />
#### Task 17 - Creating the Game Screen ####

We already added a _TitleScreen_ to our project which derived from _DrawableGameComponent_. We need to do the same for the _GameScreen_.

1. Right-click on the **Screens** folder and click **Add->Class**. Call this class **GameScreen.cs**.

1. Add the following using clauses to the top of the **TitleScreen.cs**. These will import the required MonoGame namespaces.

	(Code Snippet - _IntroMonoGame - ScreenUsings_)

	````C#
	using System.Collections.Generic;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	````

1. Change the **GameScreen** class so it derives from **DrawableGameComponent**.

	````C#
	class GameScreen : DrawableGameComponent
	````


1. Add the following fields. We need a **SpriteBatch** to draw our graphics and field to hold our Player sprite.

	(Code Snippet - _IntroMonoGame - ScreenFields_)

	````C#
	private Player _player;
	private readonly SpriteBatch _spriteBatch;
	````

1. Add a constructor for the **GameScreen** like we did with the TitleScreen. Remember it needs the game class as a parameter. In the constructor we will create the spriteBatch, the player and start playing the music with the AudioManager.

	(Code Snippet - _IntroMonoGame - ScreenConstructor_)

	````C#
	public GameScreen(Game game) : base(game)
	{
		_spriteBatch = new SpriteBatch(game.GraphicsDevice);
		AudioManager.StartTheme();
		_player = new Player();
		_player.Position = new Vector2(AlienAttackGame.ScreenWidth / 2 - _player.Width / 2, AlienAttackGame.ScreenHeight - 120);
	}
	````

7. Next Up is a method to move the player. We will call this from the GameScreen Update method. We will set our default player velocity to zero so we don't move. Then if we get any input we will change the Velocity so the player goes in the direction we want. We then call _player.Update to apply the `Position += Velocity` logic we added to the Sprite class.
 
	(Code Snippet - _IntroMonoGame - ScreenMovePlayer_)

	````C#
	private void MovePlayer(GameTime gameTime)
	{
		if (_player != null)
		{
			 _player.Velocity = Vector2.Zero;
			 // move left
			 if (InputManager.ControlState.Left && _player.Position.X > 0)
				  _player.Velocity = new Vector2(-400 / 1000.0f, 0);
			 // move right
			 if (InputManager.ControlState.Right && _player.Position.X + _player.Width < AlienAttackGame.ScreenWidth)
				  _player.Velocity = new Vector2(400 / 1000.0f, 0);
			 _player.Update(gameTime);
		}
	}
	````

1. Now add the **Update** method override which will call MovePlayer.

	(Code Snippet - _IntroMonoGame - ScreenUpdate_)

	````C#
	public override void Update(GameTime gameTime)
	{
		MovePlayer(gameTime);
	}
	````

1. Now add our **Draw** method.

	(Code Snippet - _IntroMonoGame - ScreenDraw_)

	````C#
	public override void Draw(GameTime gameTime)
	{
		_spriteBatch.Begin();
		// draw the player
		if (_player != null)
			 _player.Draw(gameTime, _spriteBatch);
		_spriteBatch.End();
	}
	````

1. If you ran the game now you would still be stuck on the title screen. We need to hook up the GameState changes. Open **AlienAttackUniversal.cs** and find the **Update** method then add above the call to _screen.Update.

	````C#
	InputManager.Update();
	````

1. Go to the SetState method and change it so that we create the **GameScreen**.
 
	(Code Snippet - _IntroMonoGame - AlienAttackUniversalSetState_)

	````C#
	public void SetState(GameState newState)
	{
		switch (newState)
		{
			 case GameState.TitleScreen:
				  _screen = new TitleScreen(this);
				  break;
			 case GameState.GameScreen:
				  _screen = new GameScreen(this);
				  break;
		}
	}
	````

1. Finally, open up **TitleScreen** and add the following **Update** override method. This will check for input and then call the **SetState** method.
 
	(Code Snippet - _IntroMonoGame - TileScreenUpdate_)

	````C#
	public override void Update(GameTime gameTime)
	{
		if(InputManager.ControlState.Start)
			AlienAttackGame.Instance.SetState(GameState.GameScreen);
	}
	````

1. Hit **F5** or click the Run **Local Machine** button.
 
You should see your spaceship at the bottom of the screen. And if you press **Left/Right** arrow it should move.

<a name="Ex1Task18" />
#### Task 18 - Adding Shooting ####

Our next task is to get our ship to shoot. 

1. Right-click on the **Sprites** folder and click **Add->Class**. Call this class **PlayerShot.cs**.

1. Change the **PlayerShot** so it derives from **Sprite**.

	````C#
	class PlayerShot : Sprite
	````

1. Add the following using clause.

	````C#
	using Microsoft.Xna.Framework;
	````

1. We now need to load the textures for this sprite. So let's just add a constructor and call LoadContent in it. We also need to set the Velocity for this sprite. Note we are using an animated sprite this time, there are 3 frames available pshot_0, pshot_1 and pshot_2.

	(Code Snippet - _IntroMonoGame - PlayerShotConstructor_)

	````C#
	public PlayerShot()
	{
		LoadContent(AlienAttackGame.Instance.Content, "gfx\\pshot\\pshot_{0}", 3);
		Velocity = new Vector2(0, -300 / 1000.0f);
	}
	````

1. We also need to override the **Update** method. This will let use update the animation for this sprite.

	(Code Snippet - _IntroMonoGame - PlayerShotUpdate_)

	````C#
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		AnimateReverse(gameTime, 100);
	}
	````

1. Now go back to the **GameScreen.cs**. We need to add a list to hold the instances of PlayerShot. We need a list because no one likes a gun that only has one bullet :) We also need a field in which to store the last game time what we fired a bullet. This is so that there is a delay between shots.

	(Code Snippet - _IntroMonoGame - GameScreenPlayerShotFields_)

	````C#
	private readonly List<PlayerShot> _playerShots;
	private double _lastTime;
	````

1. In the **GameScreen** constructor create a new instance of _playerShots.

	````C#
	 _playerShots = new List<PlayerShot>();
	````

1. Now, we add a **UpdatePlayerShots** method. You can see in the code below that we only fire a bullet every 500 milliseconds. We also make use of the AudioManager to play the "PlayerShot" sound. This method also makes sure we update ALL the shots on the screen and remove the ones that have made it off the top of the screen.

	(Code Snippet - _IntroMonoGame - GameScreenUpdatePlayerShots_)

	````C#
	private void UpdatePlayerShots(GameTime gameTime)
	{
		// if we are allowed to fire, add a shot to the list
		if (_player != null && InputManager.ControlState.Fire && gameTime.TotalGameTime.TotalMilliseconds - _lastTime > 500)
		{
			 // create a new shot over the ship
			 PlayerShot ps = new PlayerShot();
			 ps.Position = new Vector2((_player.Position.X + _player.Width / 2.0f) - ps.Width / 2.0f, _player.Position.Y - ps.Height);
			 _playerShots.Add(ps);
			 _lastTime = gameTime.TotalGameTime.TotalMilliseconds;
			 AudioManager.PlayCue(AudioManager.Cue.PlayerShot);
		}

		// enumerate the player shots on the screen
		for (int i = 0; i < _playerShots.Count; i++)
		{
			 PlayerShot playerShot = _playerShots[i];

			 playerShot.Update(gameTime);

			 // if it's off the top of the screen, remove it from the list
			 if (playerShot.Position.Y + playerShot.Height < 0)
				  _playerShots.RemoveAt(i);
		}
	}
	````

1. We also need to call **UpdatePlayerShots** in the **Update** method of **GameScreen**. Your update method should look like the following.

	````C#
	public override void Update(GameTime gameTime)
	{
		MovePlayer(gameTime);
		UpdatePlayerShots(gameTime);
	}
	````

1. Finally, we need to draw all of the shots. Add the following to the GameScreen Draw call. Remember order is important. Since the shots would normally come out from underneath the ship we should draw the shots first.

	````C#
	foreach(PlayerShot playerShot in _playerShots)
		playerShot.Draw(gameTime, _spriteBatch);
	````

1.  Hit **F5** or click the Run **Local Machine** button.

Start the game and by hitting Space you should fire the weapon. Now we just need something to shoot at!

<a name="Ex1Task19" />
#### Task 19 - Adding Enemy Sprite ####

1. Right-click on the **Sprites** folder and click **Add->Class**. Call this class **Enemy.cs**.

1. Add the following using clause.

	````C#
	using Microsoft.Xna.Framework;
	````

1. Change the **Enemy** so it derives from **Sprite**

	````C#
	class Enemy : Sprite	
	{
	}
	````

1. We now need to load the textures for this sprite. So let's just add a constructor and call LoadContent in it.

	(Code Snippet - _IntroMonoGame - EnemyConstructor_)

	````C#
	public Enemy()
	{
		LoadContent(AlienAttackGame.Instance.Content, "gfx\\enemy1\\enemy1_{0}", 10);
	}
	````

1. Because our Enemy is animated we need to call the AnimateReverse method in the **Update**.

	(Code Snippet - _IntroMonoGame - EnemyUpdate_)

	````C#
	public override void Update(GameTime gameTime)
	{
		AnimateReverse(gameTime, 60);
	}
	```` 

That is it! 

<a name="Ex1Task20" />
#### Task 20 - Adding Enemy Shooting ####

1. Right-click on the **Sprites** folder and click **Add->Class**. Call this class **EnemyShot.cs**. 

1. Add the following using clause.

	````C#
	using Microsoft.Xna.Framework;
	````

1. Change the **EnemyShot** so it derives from **Sprite**.

	````C#
	class EnemyShot : Sprite	
	{
	}
	````

1.  We now need to load the textures for this sprite. So let's just add a constructor and call **LoadContent** in it. We also want to set the Velocity for the bullet.

	(Code Snippet - _IntroMonoGame - EnemyShotConstructor_)

	````C#
	public EnemyShot()
	{
		LoadContent(AlienAttackGame.Instance.Content, "gfx\\eshot\\eshot_{0}", 3);
		Velocity = new Vector2(0, 350 / 1000.0f);
	}
	````

1. We also need to add an **Update** override to handle the animation.

	(Code Snippet - _IntroMonoGame - EnemyShotUpdate_)

	````C#
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		AnimateReverse(gameTime, 60);
	}
	````

<a name="Ex1Task21" />
#### Task 21 - Creating the Explosion Sprites ####

1. Right-click on the **Sprites** folder and click **Add->Class**. Call this class **Explosion.cs**.

1. Add the following using clause.

	````C#
	using Microsoft.Xna.Framework;
	````

1. Change the **Explosion** so it derives from **Sprite**.

	````C#
	class Explosion : Sprite	
	{
	}
	````

1.  We now need to load the textures for this sprite. So lets just add a constructor and call LoadContent in it. 

	(Code Snippet - _IntroMonoGame - ExplosionConstructor_)

	````C#
	public Explosion()
	{
		LoadContent(AlienAttackGame.Instance.Content, "gfx\\explosion\\explosion_{0}", 9);
	}
	````

1. We also need to add an **Update** override to handle the animation. Note we really don't want the explosion to repeat so if we are on the final frame, we will just exit.

	(Code Snippet - _IntroMonoGame - ExplosionUpdate_)

	````C#
	public new bool Update(GameTime gameTime)
	{
		// if it's the final frame, return true to let the other side know we're done
		if(FrameIndex == 8)
			return true;
		AnimateLoop(gameTime, 60);

		return false;
	}
	````

That is all the support classes for the Enemy done. We now need to add the logic which will make them move like they do in space invaders.

<a name="Ex1Task22" />
#### Task 22 - Creating Enemy Group Sprite ####

We want to display the enemies in a grouping. To do this we will need to create an array of EnemyShip objects and then move that array around the screen. We also have an Explosion array to keep track of explosions. As well as a few constants to define things like the number of enemies in the grid and how fast they move. 

1. Right-click on the **Sprites** folder and click **Add->Class**. Call this class **EnemyGroup.cs**.

1. Add the following using clauses.

	(Code Snippet - _IntroMonoGame - EnemyGroupUsings_)

	````C#
	using System;
	using System.Collections.Generic;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	````

1. Change the **EnemyGroup** so it derives from **Sprite**.

	````C#
	class EnemyGroup : Sprite	
	{
	}
	````

1. We need to add a lot of fields. These will be used to keep track of the enemies.

	(Code Snippet - _IntroMonoGame - EnemyGroupFields_)

	````C#
	// grid of enemies
	private readonly Enemy[,] _enemies;
	// all enemy shots
	private readonly List<EnemyShot> _enemyShots;
	// all enemy explosions
	private readonly List<Explosion> _explosions;
	private readonly Random _random;

	// width of single enemy
	private readonly int _enemyWidth;
	private const int EnemyRows = 4;	// number of rows in grid
	private const int EnemyCols = 8;	// number of cols in grid
	private readonly Vector2 EnemyVerticalJump = new Vector2(0, 10);	// number of pixels to jump vertically after hitting edge
	private const int EnemyStartPosition = 10;	// vertical position of grid
	private const int ScreenEdge = 20;	// virtual edge of screen to change direction
	private Vector2 EnemySpacing = new Vector2(16, 32);	// space between sprites
	private readonly Vector2 EnemyVelocity = new Vector2(100 / 1000.0f, 0);	// speed at which grid moves per frame
	````

1. Next, add a constructor to initialize all of the Lists and the Array of Enemies.

	(Code Snippet - _IntroMonoGame - EnemyGroupConstructor_)

	````C#
	public EnemyGroup()
	{
		_random = new Random();
		_enemyShots = new List<EnemyShot>();
		_explosions = new List<Explosion>();
		_enemies = new Enemy[EnemyRows, EnemyCols];
		// create a grid of enemies
		for (int y = 0; y < EnemyRows; y++)
		{
			 for (int x = 0; x < EnemyCols; x++)
			 {
				  Enemy enemy = new Enemy();
				  enemy.Position = new Vector2(x * enemy.Width + EnemySpacing.X, y * enemy.Height + EnemySpacing.Y);
				  _enemies[y, x] = enemy;
			 }
		}
		_enemyWidth = _enemies[0, 0].Width;
		// position the grid centered at the vertical position specified above
		Position = new Vector2(AlienAttackGame.ScreenWidth / 2.0f - ((EnemyCols * (_enemyWidth + EnemySpacing.X)) / 2), EnemyStartPosition);
		Velocity = EnemyVelocity;
	}
	````

1. Add a new method to clear all the enemy shots.

	(Code Snippet - _IntroMonoGame - EnemyGroupReset_)

	````C#
	public void Reset()
	{
		_enemyShots.Clear();
	}
	````

1. We need two methods to find the right most and left most enemy. This will be used to figure out if they have hit the edge of the screen. If we do hit the edge of the screen we need to reverse the group direction.

	(Code Snippet - _IntroMonoGame - EnemyGroupFindEnemy_)

	````C#
	private Enemy FindRightMostEnemy()
	{
		// find the enemy in the right-most position in the grid
		for (int x = EnemyCols - 1; x > -1; x--)
		{
			 for (int y = 0; y < EnemyRows; y++)
			 {
				  if (_enemies[y, x] != null)
						return _enemies[y, x];
			 }
		}
		return null;
	}

	private Enemy FindLeftMostEnemy()
	{
		// find the enemy in the left-most position in the grid
		for (int x = 0; x < EnemyCols; x++)
		{
			 for (int y = 0; y < EnemyRows; y++)
			 {
				  if (_enemies[y, x] != null)
						return _enemies[y, x];
			 }
		}
		return null;
	}
	````

1. We need a method to check if the player has killed all the enemies. Do to this we can just check to see if we have any enemies left. We will get setting the _cell_ where the enemy is to null if it has been destroyed.

	(Code Snippet - _IntroMonoGame - EnemyGroupAllDestroyed_)

	````C#
	public bool AllDestroyed()
	{
		// we won if we can't find any enemies at all
		return (FindLeftMostEnemy() == null);
	}
	````

1. This next method deals with moving the group. Take a moment to read through the code. It deals with the cases where the left/right most sprite hits the edge of the screen. In which case we just revert directions and drops the whole group down. It also updates all the sprites.

	(Code Snippet - _IntroMonoGame - EnemyGroupMoveEnemies_)

	````C#
	private void MoveEnemies(GameTime gameTime)
	{
		Enemy enemy = FindRightMostEnemy();
		// if the right-most enemy hit the screen edge, change directions
		if(enemy != null)
		{
			if(enemy.Position.X + enemy.Width > AlienAttackGame.ScreenWidth - ScreenEdge)
			{
				Position += EnemyVerticalJump;
				Velocity = -EnemyVelocity;
			}
		}
		enemy = FindLeftMostEnemy();
		// if the left-most enemy hit the screen edge, change direction
		if(enemy != null)
		{
			if(enemy.Position.X < ScreenEdge)
			{
				Position += EnemyVerticalJump;
				Velocity = EnemyVelocity;
			}
		}
		// update the positions of all enemies
		for(int y = 0; y < EnemyRows; y++)
		{
			for(int x = 0; x < EnemyCols; x++)
			{
				if(_enemies[y,x] != null)
				{
					// X = position of the whole grid + (X grid position * width of enemy) + padding
					// Y = position of the whole grid + (Y grid position * width of enemy) + padding
					_enemies[y,x].Position = 
						new Vector2((Position.X + (x * (_enemyWidth + EnemySpacing.X))), 
							(Position.Y + (y * (_enemyWidth + EnemySpacing.Y))));
					_enemies[y,x].Update(gameTime);
				}
			}
		}
	}
	````

1. The next block of code will cause the ships to fire. We make use of a Random value to decide if we need to shoot or not. Once we know we need to shoot we use the Random value again to pick the ship that will shot. Then we add the new EnemyShot instance to the list and play the Audio Cue. We also go through all the current shots in play and update them. If they are beyond the bottom of the screen we remove them from the list.

	(Code Snippet - _IntroMonoGame - EnemyGroupEnemyFire_)

	````C#
	private void EnemyFire(GameTime gameTime)
	{
		if (AllDestroyed())
			 return;

		// at random times, drop an enemy shot
		if (_random.NextDouble() > 0.99f)
		{
			 int x, y;

			 // find an enemy that hasn't been destroyed
			 do
			 {
				  x = (int)(_random.NextDouble() * EnemyCols);
				  y = (int)(_random.NextDouble() * EnemyRows);
			 }
			 while (_enemies[y, x] == null);

			 // create a shot for that enemy and add it to the list
			 EnemyShot enemyShot = new EnemyShot();
			 enemyShot.Position = _enemies[y, x].Position;
			 enemyShot.Position += new Vector2(0, _enemies[y, x].Height);
			 _enemyShots.Add(enemyShot);

			 AudioManager.PlayCue(AudioManager.Cue.EnemyShot);
		}

		for (int i = 0; i < _enemyShots.Count; i++)
		{
			 // update all shots
			 _enemyShots[i].Update(gameTime);

			 // remove those that are off the screen
			 if (_enemyShots[i].Position.Y > AlienAttackGame.ScreenHeight)
				  _enemyShots.RemoveAt(i);
		}
	}    
	````

1. The next small method is a helper function. It checks to see if two sprites are colliding. It returns true if they are false otherwise.

	(Code Snippet - _IntroMonoGame - EnemyGroupCheckCollision_)

	````C#
	public bool CheckCollision(Sprite s1, Sprite s2)
        {
            // simple bounding box collision detection
            return s1.BoundingBox.Intersects(s2.BoundingBox);
        }
	````

1. The next few blocks of code handle collision. The logic is similar for each function. Loop through the items, check for a Collision and if there is create an explosion.

	(Code Snippet - _IntroMonoGame - EnemyGroupHandleCollisions_)

	````C#
	public bool HandlePlayerShotCollision(PlayerShot playerShot)
	{
		for(int y = 0; y < EnemyRows; y++)
		{
			for(int x = 0; x < EnemyCols; x++)
			{
				// if a player shot hit an enemy, destroy the enemy
				if(_enemies[y,x] != null && CheckCollision(playerShot, _enemies[y,x]))
				{
					Explosion explosion = new Explosion();
					Vector2 center = _enemies[y,x].Position + (_enemies[y,x].Size/2.0f);
					explosion.Position = center - (explosion.Size/2.0f);
					_explosions.Add(explosion);
					_enemies[y,x] = null;
					return true;
				}
			}
		}
		return false;
	}		

	public bool HandleEnemyShotCollision(Player player)
	{
		for(int i = 0; i < _enemyShots.Count; i++)
		{
			// if an enemy shot hit the player, destroy the player
			if(CheckCollision(_enemyShots[i], player))
			{
				_enemyShots.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	public bool HandleEnemyPlayerCollision(Player player)
	{
		for(int y = 0; y < EnemyRows; y++)
		{
			for(int x = 0; x < EnemyCols; x++)
			{
				// if an enemy hit the player, destroy the enemy
				if(_enemies[y,x] != null && CheckCollision(_enemies[y,x], player))
				{
					Explosion explosion = new Explosion();
					Vector2 center = _enemies[y,x].Position + (_enemies[y,x].Size/2.0f);
					explosion.Position = center - (explosion.Size/2.0f);
					_explosions.Add(explosion);
					_enemies[y,x] = null;
					return true;
				}
			}
		}
		return false;
	}
	````

1. We now need to add the Update method override to call our **MoveEnemies** and **EnemyFire** methods. We also need to update any explosions we have. Remember that if the Explosion Update method returned true, it was because it had finished. In which case we need to remove it from the list. 

	(Code Snippet - _IntroMonoGame - EnemyGroupUpdate_)

	````C#
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);

		MoveEnemies(gameTime);
		EnemyFire(gameTime);

		for(int i = 0; i < _explosions.Count; i++)
		{
			// update all explosions, remove those whose animations are over
			if(_explosions[i].Update(gameTime))
				_explosions.RemoveAt(i);
		}
	}
	````

1. Finally, we need to draw our Enemies. Add the following **Draw** method. We just loop through the enemies, shots and explosions and draw them all.

	(Code Snippet - _IntroMonoGame - EnemyGroupDraw_)

	````C#
	public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		// draw all active enemies
		foreach(Enemy enemy in _enemies)
		{
			if(enemy != null)
				enemy.Draw(gameTime, spriteBatch);
		}

		// draw all enemy shots
		foreach(EnemyShot enemyShot in _enemyShots)
			enemyShot.Draw(gameTime, spriteBatch);

		// draw all explosions
		foreach(Explosion explosion in _explosions)
			explosion.Draw(gameTime, spriteBatch);
	}
	````

<a name="Ex1Task23" />
#### Task 23 - Adding the Explosive Finale ####

Our final task is to update the **GameScreen** to use all of these new **Enemy** classes and make this into a game. 

1. Open the **GameScreen.cs** class and add the following field definitions. Note we have an EnemyGroup field.

	(Code Snippet - _IntroMonoGame - GameScreenUsings_)

	````C#
 	private readonly Player _livesIcon;
	private Explosion _playerExplosion;
	private readonly Texture2D _bgScreen;
	private readonly EnemyGroup _enemyGroup;
	private readonly SpriteFont _font;
				
	private int _score;
	private int _lives;
	private double _backToMenuTime;
	private bool _loseGame;
	````

1. We now need to update the constructor to initialize this new fields. Add the following code after the current code in the constructor.

	(Code Snippet - _IntroMonoGame - GameScreenFields_)

	````C#
	_font = game.Content.Load<SpriteFont>("font");
	// draw a lives status icon in the lower left
	_livesIcon = new Player();
	// cache explosion
	new Explosion();
	_bgScreen = game.Content.Load<Texture2D>("gfx\\bgScreen");
	_livesIcon.Position = new Vector2(20, AlienAttackGame.ScreenHeight-80);
	_livesIcon.Scale = new Vector2(0.5f, 0.5f);
	_enemyGroup = new EnemyGroup();
	_lives = 2;
	````

1. Next, we need to add a method to handle all of the collision detection. This will call into the **_enemyGroup** collision methods we added earlier.

	(Code Snippet - _IntroMonoGame - GameScreenHandleCollisions_)

	````C#
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
	````

1. With all this new code we need to update the **Update** method. Currently is just updates the player. We now need it to update the **enemyGroup** and call **HandleCollisions** as well as check for the game over condition. Replace the entire **Update** method in **GameScreen** with the following.

	(Code Snippet - _IntroMonoGame - GameScreenUpdate_)

	````C#
	public override void Update(GameTime gameTime)
	{
		if(_enemyGroup.AllDestroyed() || _loseGame)
		{
			_backToMenuTime += gameTime.ElapsedGameTime.TotalMilliseconds;
			if(_backToMenuTime >= 3000)
			{
				AudioManager.StopTheme();
				AlienAttackGame.Instance.SetState(GameState.TitleScreen);
			}

			_enemyGroup.Reset();
			_playerShots.Clear();
		}
		else
		{
			MovePlayer(gameTime);
			UpdatePlayerShots(gameTime);
		}

		// as long as we're not in the lose state, update the enemies
		if(!_loseGame)
			_enemyGroup.Update(gameTime);

		HandleCollisions(gameTime);			
	}
	````

1. Our final update is to the draw method. We now need to change it to Draw not only the player, but the EnemyGroup. We also draw the Score, Game over Text and the player lives. If you take a look through the code, it should be fairly easy to follow. Replace the entire **Draw** method in **GameScreen** with the following.

	(Code Snippet - _IntroMonoGame - GameScreenDraw_)

	````C#
	public override void Draw(GameTime gameTime)
	{
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

    // draw the explosion
    if (_playerExplosion != null)
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
	````

<a name="Summary" />
## Summary ##

The MonoGame framework does not force you to do things in a particular way. As a result, there are many different ways to write a game. This module should have given you a grasp of:

- Processing and Loading Content
- Drawing Textures
- Drawing Text
- Playing Sound and Music.
- Structuring you code so you can reuse functionality.
