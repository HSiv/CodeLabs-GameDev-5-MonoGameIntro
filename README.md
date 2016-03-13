<a name="HOLTop" />
# Introduction to MonoGame #

---

<a name="Overview" />
## Overview ##

MonoGame is a free open source, cross platform gaming Framework. Developers using this framework can target Windows 10 , MacOS, Linux , Android and iOS. All of which can use the same code base. This session will take you through the process of building a sample arcade game from scratch.

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

<a name="Setup" />
### Setup ###
In order to run the exercises in this module, you will need to set up your environment first.

1. Open Windows Explorer and browse to the module's **Source** folder.
1. Right-click on **Setup.cmd** and select **Run as administrator** to launch the setup process that will configure your environment and install the Visual Studio code snippets for this module.
1. If the User Account Control dialog box is shown, confirm the action to proceed.

> **Note:** Make sure you have checked all the dependencies for this module before running the setup.

<a name="CodeSnippets" />
### Using the Code Snippets ###

Throughout the module document, you will be instructed to insert code blocks. For your convenience, most of this code is provided as Visual Studio Code Snippets, which you can access from within Visual Studio 2015 to avoid having to add it manually. 

>**Note**: Each exercise is accompanied by a starting solution located in the **Begin** folder of the exercise that allows you to follow each exercise independently of the others. Please be aware that the code snippets that are added during an exercise are missing from these starting solutions and may not work until you have completed the exercise. Inside the source code for an exercise, you will also find an **End** folder containing a Visual Studio solution with the code that results from completing the steps in the corresponding exercise. You can use these solutions as guidance if you need additional help as you work through this module.

---

<a name="Exercises" />
## Exercises ##
This module includes the following exercise:

1.  [Getting Started : Creating your first game](#Exercise1)

Estimated time to complete this module: **60 minutes**

>**Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.

<a name="Getting Started" />
### Getting Started : Creating your first game ###

In this exercise you will create your first MonoGame Windows 10 project. 

<a name="Ex1Task1" />
#### Task 1 - Create Project ####

To get started we need to first create a new "MonoGame Windows 10 Universal Project".

1. Got to File->New Project
2. File the MonoGame secton in Visual C#->MonoGame
3. Select "MonoGame Windows 10 Universal Project"
4. Change the name to "AlienAttackUniversal"
5. Select where you want to save the soluton. It will default to c:\users\<user>\documents\visual studio 2015\Projects
6. Click OK 

If you take a look at the code you will see allot of "using Microsoft.Xna.Framework" type using clauses... whats going on there? This is MonoGame right? Yes this is MonoGame :) It is a XNA Api compabile cross platform gaming framework. To be XNA Api compatible it needs to use the same namespaces. So if you have used XNA in the past you will feel right at home with MonoGame.


<a name="Ex1Task2" />
#### Task 2 - Rename Game1.cs ####

The default project will create a class called Game1. While you can leave this as it is, its good practice to rename this class. 

1. Right click on Game1.cs  and select Rename.
2. Type in "AlienAttackGame.cs". You should see a dialog asking if you want to rename all references in the project
3. Click Yes.

<a name="Ex1Task3" />
#### Task 3 - Add Assets ####

No game would be complete without graphics and sound. Fortunately this session comes with a complete set of assets for you to use. In order to get the maxumin performance for your game you will need to pre-process the assets into a optimized format. This can be done using the MonoGame Pipeline Tool. Its purpose is to task assets like .png/.wav and compress/optimize them for the platform you are targeting. In this case Windows 10. This will allow you to take advangate of things like texture compression which in turn will allow you to include more graphics! and who doesn't like more graphics.

1. Click on the "Content" folder in the Solution to expand it.
2. Double click on "Content.mgcb". This should open the file in the MonoGame Pipeline Tool. 
    If the file opens in the code editor. Right Click on "Content.mgcb" and click Open With. The select the "MonoGame Pipeline Tool"
3. Goto Edit->Add->Existing Folder
4. Navigate to "End\AlienAttackUniversal\Content\gfx"
5. Click Add. A dialog will pop up to ask if you want to copy or link the files. Select Copy and click ok.
6. Goto Edit->Add->Existing Folder
7. Navigate to "End\AlienAttackUniversal\Content\sfx"
8. Click Add. A dialog will pop up to ask if you want to copy or link the files. Select Copy and click ok.
9. Goto Edit->Add->Existing Item
10. Navigate to "End\AlienAttackUniversal\Content\Font.spritefont"
11. Click Add. A dialog will pop up to ask if you want to copy or link the files. Select Copy and click ok.


<a name="Ex1Task4" />
#### Task 4 - Load a Texture ####

Now that all the assets have been added to our Content.mgcb file they will be compiled and automatically included in our application package. Our next task is to use the Content manager to load a texture. The code we are about to add will be removed later but it will demonstrate how easy it is to load assets.

1. Open AlienAttackGame.cs if it is not already open.
2. Add the following code just under the SpriteBatch declaration on line 12.
	Texture2D background;
3. Navigate to the LoadContent method on line 42 and add the following code
	background = Content.Load<Texture2D> ("gfx/bgScreen");

That is all there is to it. The same can be applied to SoundEffect/Meshes etc. The Content.Load method is a generic method so we just specifcy the type we are trying to load. If the types do not match a error will be raised. For example if we try to load a sound effect and pass <Texture2D> we will get an error.

<a name="Ex1Task5" />
#### Task 5 - Draw a Texture ####

To render any 2 dimensional (2D) textures we can make use of the SpriteBatch class. This class is designed to draw textures and text to the screen. You can use it to efficiently *batch* the drawing of items, this helps to make sure you are making the most productive use of the video resources. The game template creates a spriteBatch for you.

The SpriteBatch has .Begin and .End methods. You use these to define a *batch*, all the calls to the various .Draw methods between a .Begin/.End pair will be optimized and sent to the GPU as efficiently as possible. Lets draw the texture we loaded in Task 4.

1. In the AlienAttachGame.cs file, find the Draw method. It should contain the following code
       protected override void Draw(GameTime gameTime)
       {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

2. Under the //TODO section add the following code

            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.End();

3. Make sure your selected build configuration is set to "Debug" "x64"
4. Hit F5 or click the Run "Local Machine" button.

Congratulations, you just loaded and rendered your first texture!

<a name="Ex1Task6" />
#### Task 6 - Draw Text ####


Drawing text is similar to drawing a texture. We first need to load a font , and then use the SpriteBatch to render the text.

1. Go back to where we declared the background Texture2D and add
	SpriteFont font;

2. Go to the LoadContent method and add the following underneath were we loaded the texture.
	font = Content.Load<SpriteFont>("Font");
3. Next modify the Draw code we pasted earlier to include a call to spriteBatch.DrawString just underneath the call to draw our texture.
	spriteBatch.DrawString(font, "MonoGame Rocks!", Vector2.Zero, Color.White);

4.  Hit F5 or click the Run "Local Machine" button.

You should see the text in the top left corner of the screen. Note the order of the batch items is important. If we had put the call to DrawString above the line which draws the texture it would not be show. This is because the texture would be drawn after the text. Because the texture takes up the entire screen it will overwrite the text.  

<a name="Ex1Task7" />
#### Task 7 - Playing Music ####

No game would be complete without Sound Effects and Music. MonoGame has two classes which make working with these items easier. SoundEffect and Song. SoundEffect is usually used to play short audio items, Song is used to play longer items that might need to be streamed. Loading both of these items is done in the same way we load textures and fonts.

Depending on the format of your audio will depend on the defalt content *Processor* that is used to optimize the content. For .wav files the Sound Effect processor is used, for files like .wma the Song processor is used. If you have a .wav file that you want to use for music you can always change the Processor it used in the Pipeline Tool to be "Song - MonoGame"

Lets load the music for the game.

1.  Go back to where we declared the background Texture2D and add
	Song song;
2.  Right click on the "Song" class and use the Quick Actions to add a using for *Microsoft.Xna.Framework.Media* or add it manually
	using Microsoft.Xna.Framework.Media;
3.  Go to the LoadContent method and add the following underneath were we loaded the texture.
 	song = Content.Load<Song>("sfx/theme");
	MediaPlayer.IsRepeating = true;
	MediaPlayer.Play(song);
3. Hit F5 or click the Run "Local Machine" button.

You should hear the music playing when the app starts.

<a name="Ex1Task8" />
#### Task 8 - Playing Sound ####

1.  Go back to where we declared the background Texture2D and add
	SoundEffect playerShot;
2.  Right click on the "SoundEffect" class and use the Quick Actions to add a using for *Microsoft.Xna.Framework.Audio* or add it manually
	using Microsoft.Xna.Framework.Audio;
3.  Go to the LoadContent method and add the following underneath were we loaded the texture.
 	playerShot = Content.Load<SoundEffect>("sfx/playerShot");
	playerShot.Play ();
3. Hit F5 or click the Run "Local Machine" button.
 
When the application starts you should hear the playerShot sound effect play. 

<a name="Ex1Task9" />
#### Task 9 - DrawableGameComponent ####

At this point you know how to load the basic assets needed to write a game. There are other assets like Shaders and custom content but to get started all you need is Textures, Fonts, SoundEffects and Music. We can now start writing the game. Our game will have a couple of screens. A Title screen and a Game screen. Each will be responsible for rendering itself and responding to player input. To implement these we are going to use the *DrawableGameComponent* class. MonoGame has a concept of *Components* which you can add to the game  and will automatically update and render themselve. First there is athe GameComponent which can be used for code that just needs to update every frame. The DrawableGameComponent adds a virtual Draw method which can be used to draw items. Since both our Title screne and Game screen will need to draw graphics they will both derive from DrawableGameComponent.

1. In the solution explorer right click on the AlienAttackUniveral project and click Add->New Folder. Name this folder Screens.
2. Right click on the Screens folder and click Add->Class. Call this class TitleScreen.cs
3. Add the following using clauses to the top of the TitleScreen.cs . These will import the required MonoGame namespaces
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
4. Change the TitleScreen class so it derives from DrawableGameComponent.
	class TitleScreen : DrawableGameComponent
5. Next add a contructor. The base GameComponent class *requires* a Game instance parameter for its constructor to our constructor needs to look like the following.
	public TitleScreen(Game game) : base(game)
        {
	}

6. Next we need to add some private fields to hold Textures, Fonts and a SpriteBatch with which to draw them.
	private readonly Texture2D _titleScreen;
        private readonly SpriteFont _font;
        private readonly SpriteBatch _spriteBatch;
7. Now update the constructor to create/load these items.
	public TitleScreen(Game game) : base(game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            _titleScreen = game.Content.Load<Texture2D>("gfx\\titleScreen");
            _font = game.Content.Load<SpriteFont>("Font");
        }
8. Finally we need to override the Draw method. The Draw (and Update) method takes a GameTime parameter. This is how we keep tack of time in the game. It holds how much time has elapsed since the game started and the elasped time since the last frame. we'll use this information later in our GameScreen. For now the Draw method on the TitleScreen just needs to render the texture we loaded as some text.
	public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_titleScreen, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_font, "Press Enter or Tap to Play", new Vector2(1200, 960), Color.White);
            _spriteBatch.End();
        }

In the next section we will update the AlienAttackGame class to make use of this new class. 

<a name="Ex1Task10" />
#### Task 10 - Updating the Game class ####

Now that we have a basic TitleScreen we need to be able to render it. To do that we need to remove all of the code we added to the AlienAttackGame class earlier and add a few new methods.

1. Open the AlienAttackGame.cs if it is not already open.
2. Add a using clause for 
	using AlienAttackUniversal.Screens;
3. Select the *entire* AlienAttackGame *class* and replace it with the following 
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

4. We need to create an enumeration to define the current *screen* in the game. Add the following code above the AlienAttackGame class.		public enum GameState
	{
		TitleScreen,
		GameScreen
	};
5. Now we need to declare a few properties to store which screen we are on and allow access to the instance of the game from anywhere. We also want to define our screen resolution that we want to use. Add the following to the top of the AlienAttackGame class
	public static AlienAttackGame Instance;
	private readonly GraphicsDeviceManager _graphics;
	private DrawableGameComponent _screen;
	public static int ScreenWidth = 1920;
	public static int ScreenHeight = 1080;

6. Next update the constructor to create/set all of these properties
	Instance = this;
	_graphics = new GraphicsDeviceManager(this);
	// set our screen size based on the device
	_graphics.PreferredBackBufferWidth = ScreenWidth;
	_graphics.PreferredBackBufferHeight = ScreenHeight;
	Content.RootDirectory = "Content";

Note we set the static Instance field to this. Using this field we can access the *instance* of the game just by using AlienAttackGame.Instance. We also create our graphics device manager and set our preferred resolution. The Content manager is also given the "RootDirectory" which is the base directory it will look for the compiled assets.

7. Next we need to add a method which will allow us to change which screen we are showing. This methid is called SetState and will update the _screen field we just created. Add the following code somewhere near the bottom of the AlienAttackGame class.
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
This method currently only updates the _screen field for a TitleScreen gamestate. We'll add code for the GameScreen later.

8. Now find the Initialize method and add a call to SetState (GameState.TitleScreen). This is to that when the game starts it will default to the title screen. Initialize is only called when the game starts.
	protected override void Initialize()
        {
            SetState(GameState.TitleScreen);
            base.Initialize();
        }

9. Add the following code to the Update method. This will make sure that our *current* screen is updated every frame.
	_screen.Update(gameTime);
10. Finally add the following code to the Draw methid. This will draw our *current* screen.
 	_screen.Draw(gameTime);
11. Hit F5 or click the Run "Local Machine" button.

You should now see the title screen rendered.

<a name="Ex1Task11" />
#### Task 11 - RenderTargets ####

We managed to get our title screen rendering in the last Task. But it wasn't scaling to fit the screen? If you resized the window it didn't stretch to fill it. What is going on?

Well spriteBatch is a low level drawing system. It doesn't care about scaling or fitting graphics to a window. So we need to somehow scale our graphics ourselves to atapt to the various screen resolutions devices can have. Remember Windows 10 supports everything from a Phone right up to a Xbox One with a 4K TV on it. We need a way to handle ALL of those devices. 

SpriteBatch can take a scale matix which it can use to scale the coordinates and graphics passed to it. But this method can be a bit msessy. Wouldn't it be great if there was a way to draw at a fixed known resolution and then scale that? Well it turns out there is its called a RenderTarget. Simply put a RenderTarget is a texture to can draw to. When to tell the graphics device to use a RenderTarget you are basically telling it to not draw on the screen but onto the texture. Now because a RenderTarget derives from Texture we can then use it with a spriteBatch to draw but scale the results. This way we can draw our game at a fixed resolution. 

What we will do next is add a new helper class called *RenderTargetScaler* which we can then use to scale our entire game to fit the window.

1. Right click on the AlienAttackUniversal Project in the solution explorer and click Add->Class and add a "RenderTargetScaler.cs"
2. Add the following using clauses to the top of RenderTargetScaler.cs to bring in the required namespaces.
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
3. Next we need a few fields for our scaler. The most important of which is our _drawBuffer which is a RenderTarget2D. This is what we will draw our entire game to before using the scaler to draw the _drawBuffer on the screen.
	private readonly RenderTarget2D _drawBuffer;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly int _screenWidth;
        private readonly int _screenHeight;
        private readonly Game _game;
        private readonly SpriteBatch _spriteBatch;

Note there is another SpriteBatch in this list. Normally you will want to keep the number of SpriteBatch instances to a minimum, this is because they maintain an internal list of items that need to be rendered. Its more memory efficient to only have one or two of them. 
4. Next we need to add a constructor. This will need to take an instance of the Game class so we can get things like the Window, the GraphihcsDeviceManager we created in the AlienAttackGame class and the resolution we are going to draw our game in.

	public RenderTargetScaler(Game game, GraphicsDeviceManager graphicsDeviceManager, int width, int height)
        {
        }
        
5. Next we set all of our local fields and create an instance of the _spriteBatch in the new constructor we just added.
	_game = game;            
	_graphicsDeviceManager = graphicsDeviceManager;
	_screenWidth = width;
	_screenHeight = height;
	_spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice); 
6. We now need to create our render target. This requires the GraphicsDevice, and our target width and height.
	_drawBuffer = new RenderTarget2D(graphicsDeviceManager.GraphicsDevice, width, height);

7. That is all we need in the constructor. Next we need a way to tell the GraphicsDevice to use the render target not the screen. We can do that via the GraphicsDevice.SetRenderTarget method. This method takes a RenderTarget2D instance or a null, if we pass null that tells the GraphicsDevice to draw to the screen. If we pass an instance it will use the RenderTarget. We'll add a helper methd to make things a bit eaier for ourselves
	public void SetRenderTarget()
	{
		_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(_drawBuffer);
	}
Any spriteBatch draw calls made after this method is called will draw on the _drawBuffer. As soon as we call SetRenderTartet (null) it will revert back to drawing on the screen. We'll add that in our next helper method.

8. Now we need a Draw method. This is where we will calculate our scaling factor, clear the render target (using null), then use the spriteBatch to draw our _drawBuffer to the screen. For now just add the new Draw method
	public void Draw ()
	{
	}

In the next section we'll figure out our scaling factors and fill in the rest of the Draw method.

<a name="Ex1Task12" />
#### Task 12 - Scaling to Screen ####

In the previous section we added a helper class to handle scaling our game. When scaling we will want to use a techique called Letterboxing. This is where rather than just stretching the texture so it fills the entire window, we stretch it so we maintain the aspect ration of the texture. For example of we have a texture that is 1024x768 (4:3) and stretched it to a window that was 1280x720 (16:9) that would look really really weird and distorted. So what we do it create a "letterbox" by adding padding to either the top/bottom or the left/right to make sure we maintain the origional aspect ratio of the texture. 

1. You should already be in the Draw method. The first thing we need to do is figure out the current aspect ratios for our window and our fixed resolution we are drawing at. We do that by dividing the width by the height.
	float outputAspect = _game.Window.ClientBounds.Width / (float)_game.Window.ClientBounds.Height;
	float preferredAspect = _screenWidth / (float)_screenHeight;
2. Add a Rectangle called dst. This will hold our destination rectangle which is where we will draw the _drawBuffer. We will calcuate this in the next step.
	Rectangle dst;
3. Next we need to decide if we need to add padding to the top or the sides to get our letterbox effect. Of our *outputAspect* is less than the *preferredAspect* we need to pad the top and bottom. Otherwise we need to add the padding to the left and right.
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
4. Now we have the dst Rectangle calulcated we can start drawing the _drawBuffer. But before we do we need to make sure the GraphicsDevce is NOT using the render target.
 	_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);

5. Next we want to clear the screen with a nice Black color. You could use a different color but black is normally used because it matches the default color of a screen :).
	_graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
6. Finally we use the spriteBatch to draw our _drawBuffer. Note this time we are using the dst Rectangle rather than a Vector2 to define where on the screen this texture will be drawn. The SpriteBatch will scale the texture to fill that Rectangle, and because we took care to calculate our Rectangle based on the aspect ratio it will look correct.
	_spriteBatch.Begin();
	_spriteBatch.Draw(_drawBuffer, dst, Color.White);
	_spriteBatch.End();

7. We now have all the bits in place in our helper class. Now go back to the AlienAttackGame class and add field for the RenderTargetScaler below all the other fields
 	private RenderTargetScaler _scaler;
8. In the AlienAttackGame Initialize method create an instance of the _scaler just BEFORE we call SetGameState
	_scaler = new RenderTargetScaler(this, _graphics, ScreenWidth, ScreenHeight);
9. Finally update the Draw method of the AlienAttackGame to match the following code 
	protected override void Draw(GameTime gameTime)
	{
		_scaler.SetRenderTarget();
		_screen.Draw(gameTime);
		_scaler.Draw();
	}

Note how we call _scaler.SetRenderTarget () first then after we draw the screen , we then call _scaler.Draw. This will make sure the screen is correctly scaled for the window.

<a name="Ex1Task13" />
#### Task 13 - Handling Input ####

In this task we will add input support for our game. MonoGame comes with a complete set of classes for getting input such as GamePads, Keyboard, Mouse and TouchScreens. All of these impliment a similar *state* based api. This allows you to get the current state of the input decice. Because games are generaelly not event driven we will be calling these *GetState* methods every frame. 

Rather than littering our game code with lots of different input methods we will wrap them all up into on InputManager class. This will have one Update method to retrive the current input state. It will then expose this state via some static variables so we can access it throught the game code. 


1.  Right click on the AlienAttackUniversal Project in the solution explorer and click Add->Class and add a "InputManager.cs"
2.  Add the following using clauses to the top of the InputManager.cs class file
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Input;
	using Microsoft.Xna.Framework.Input.Touch;
3. Add the new ControlState enumeation just above the InputManager class.  This structure will be exposed as a static and will let us know which controls are active.
	public struct ControlState
	{
		public bool Left;
		public bool Right;
		public bool Start;
		public bool Quit;
		public bool Fire;
		public float FingerPosition;
	}

4. We now need to declare some internal fields to hold our control *state*. Note we store the current AND previous state for Keyboard and GamePads. This allows us to caclualate what has changed between frames.
 
	private static KeyboardState _keyboardState, _lastKeyboard;
	private static GamePadState _gamePadState, _lastGamePad;
	private static ControlState _controlState;

5. Next we need a static constructor. In this we will tell the TouchPanel class which *Gestures* we can enabled. In this case if a user taps the screen or id they drag horizontally. We will use the former to handle firing and the latter for moving the player.
 
	static InputManager()
	{
		TouchPanel.EnabledGestures = GestureType.Tap | GestureType.HorizontalDrag;
	}

5. Its time to add the update method. This needs to be public and static so it can be called from anywhere in the game.
	public static void Update()
	{
	}
6. Before we fill in the Update method we will add the public static property for the ControlState structure we decalred earlier.
	public static ControlState ControlState
	{
		get { return _controlState; }
	}

7. Now for the intersting bit. We need to retireve the current state for the all the inputs then caclulate the ControlState. First this to do is to get the Keyboard and GamePad states. Add the following code to the Update method.
	_keyboardState = Keyboard.GetState();
	_gamePadState = GamePad.GetState(PlayerIndex.One);
8. Now we need to set the ControlState fields depending on the states we got. For example the following code 
	_controlState.Quit = (_gamePadState.Buttons.Back== ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Escape));
  sets the "Quit" field if the Gamepad "Back" buttons is pressed OR is the Escape Key is pressed on the keyboard. 
  Add the following code to the Update method.

	_controlState.Quit = (_gamePadState.Buttons.Back == ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Escape));
        _controlState.Start = (_gamePadState.Buttons.B == ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Enter) || 		_keyboardState.IsKeyDown(Keys.Space) || _gamePadState.IsButtonDown(Buttons.Start));
         _controlState.Left = (_gamePadState.DPad.Left == ButtonState.Pressed || _gamePadState.ThumbSticks.Left.X < -0.1f || _keyboardState.IsKeyDown(Keys.Left));
        _controlState.Right = (_gamePadState.DPad.Right == ButtonState.Pressed || _gamePadState.ThumbSticks.Left.X > 0.1f || _keyboardState.IsKeyDown(Keys.Right));
        _controlState.Fire = ((_gamePadState.Buttons.B == ButtonState.Pressed && _lastGamePad.Buttons.B == ButtonState.Released) || (_keyboardState.IsKeyDown(Keys.Space) && !_lastKeyboard.IsKeyDown(Keys.Space)));
        
9. That covers the GamePad and Keyboard. Next is the Touch Gestures. The TouchPanel class exposed s IsGestureAvailable property which tells us if a gesture was detected. It also provides a ReadGesture method to read the current gesture.  Note Gestures are queued so it is possible to get multiple gestures in on frame.. e.g a Tap and a Drag. So we need to loop through until IsGestureAvailable is false. Add the following code to the Update Method
 
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

You shold be able to see we just loop while we have gestures, and depending on the gesture type we will set the control state. If we get a horizontal drag we will set the Left/Right control state fields depending on which direction the drag was it. We also store the current position for later.

10. Finally we need to store the current state in the _last*State fields so we can detect changes in the next frame.
	_lastGamePad = _gamePadState;
        _lastKeyboard = _keyboardState;

That covers the InputMananger. We can now use code like the following anywhere in the game to check the input.

	If (InputManager.Controlstate.Quit) {
		// quit the game
	}
	
Our next task is the Audio Manager.	
<a name="Ex1Task14" />
#### Task 14 - Audio Manager ####

<a name="Ex1Task15" />
#### Task 15 - Game Screen ####

<a name="Ex1Task16" />
#### Task 16 - Sprite ####

<a name="Ex1Task17" />
#### Task 17 - Adding the Player ####

<a name="Ex1Task18" />
#### Task 18 - Enemy ####

<a name="Ex1Task19" />
#### Task 19 - Shooting ####

<a name="Ex1Task20" />
#### Task 20 - Enemy Group ####

<a name="Ex1Task21" />
#### Task 21 - The Explosive Finale ####

<a name="Summary" />
## Summary ##

By completing this module you should have:

- Foo

> **Note:** You can take advantage of the [Visual Studio Dev Essentials]( https://www.visualstudio.com/en-us/products/visual-studio-dev-essentials-vs.aspx) subscription in order to get everything you need to build and deploy your app on any platform.
