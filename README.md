<a name="HOLTop"></a>
# Introduction to MonoGame #

---

<a name="Overview"></a>
## Overview ##

MonoGame is a free open source, cross platform gaming Framework. Developers using this framework can target Windows 10, MacOS, Linux, Android, iOS and more, all using the same code base. This session will take you through the process of building a sample arcade game from scratch.

<a name="Objectives"></a>
### Objectives ###

In this module, you will see how to:

- Process your game assets to make sure they are optimized for your target platform
- Load game assets
- Draw images to the screen
- Handle input from the keyboard
- Play music and sounds 

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

<a name="Exercises"></a>
## Exercises ##

This module includes the following exercise:

1.  [Creating your first game](#Exercise1)

Estimated time to complete this module: **60 minutes**

> **Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.


<a name="Exercise1"></a>
### Exercise 1: Creating your first MonoGame game ###

In this exercise you will create a game for Windows 10 using MonoGame. Since we only have an hour we'll be modifying an existing project to add functionality. The existing project is a space invaders style game. It is currently missing the player ship, input, and its sound effects, so when you run the project you will only see aliens. During the course of this Code Lab you will be adding new code to add these features.

<a name="Ex1Task1"></a>
#### Task 1 - Opening Project ####

1. In **Visual Studio 2015** go to **File->Open Project**.

1. Navigate to **Source\Ex1\Begin**.

1. Open **AlienAttackUniversal.sln** solution.

1. Once the project has loaded make sure your selected build configuration is set to **Debug** and **x86**.

1. Hit **F5** or click the Run **Local Machine** button.

The game should run, but at the moment only the aliens are present, and the player is missing.

<a name="Ex1Task2"></a>
#### Task 2 - Adding missing content ####

No game would be complete without graphics and sound. Fortunately, this session comes with a complete set of assets for you to use. In order to get the maximum performance for your, game you will need to pre-process the assets into an optimized format for the target platform, in this case Windows 10. This can easily be done using the MonoGame Pipeline Tool. Its purpose is to take assets like PNG and WAV files and compress/optimize them for the platform you are targeting. This will allow you to take advantage of things like texture compression which, in turn, will allow you to include more graphics.

Most of the content has already been added to the game, but some bits are missing. In this section we will use the Pipeline tool to add these assets to the game.

1. Click the **Content** folder in the Solution Explorer to expand it.

1. Double click on **Content.mgcb**. This should open the file in the MonoGame Pipeline Tool. If the file opens in the code editor, right-click on **Content.mgcb** and click **Open With**, and then select **MonoGame Pipeline Tool**.

1. Right-click on the **gfx** folder in the pipeline tool and click **Add->Existing Item**.

1. Navigate to **Source\Ex1\Begin\Content\gfx\player.png** and click **Open**.

1. Right-click on the **sfx** folder in the pipeline tool and click **Add->Existing Item**.

1. Navigate to **Begin\AlienAttackUniversal\Content\sfx\playerShot.wav** and click **Open**.

1. Select **File->Save** to save the changes.

That is how easy it is to add content to the game. All of the content is included automatically in you project as long as it is in the Content.mcgb file. If you have files that you just want to copy over (like XML files), you can also add those to the Content.mgcb file but set the Build Action to "Copy" and they will also be included without changes.

<a name="Ex1Task3"></a>
#### Task 3 - Adding the Player ####

The project already has a **Player** class located in the **Sprites** directory, however its implementation is empty.  Let's fix that.  

1. Double-click on the **Player.cs** item in Solution Explorer.  Notice that this class inherits from the project's base Sprite class.  Feel free to take a look at the base class, noting that it contains various properties like **Position** and **Velocity**.

1. We now need to load the textures for this sprite. In the constructor, call the **LoadContent** method from the base **Sprite** class as shown:

	````C#
	public Player()
	{
		LoadContent(AlienAttackGame.Instance.Content,  "gfx\\player");
	}
	````

	If you look at the **LoadContent** method, you will see that it uses MonoGame's **ContentManager** class to load the item located at "gfx\\player" as a **Texture2D**, which is the exact file we just added to the Content Pipeline Tool.

	````C#
	public virtual void LoadContent(ContentManager contentManager, string name)
	{
		// load single frame
		Frames = new Texture2D[1];
		Frames[0] = contentManager.Load<Texture2D>(name);
		Width = Frames[0].Width;
		Height = Frames[0].Height;
	}
	````
	
	As you can see this method makes use of the **ContentManager** class to load the texture. The **ContentManager** is the main way to load any kind of asset which has been processed by the content Pipeline. An instance is created on the Game class and is accessible via the **.Content** property. As you can see, we pass in ```AlienAttackGame.Instance.Content``` into the LoadContent method. 

	The **ContentManager** provides a generic **Load** method for loading assets. You can use this to load **Texture2D**, **SpriteFont** and other content types. It also includes type checking, so if you attempt to load a song into a **Texture2D**, for example, the manager will throw an exception.

1. Now that the content is loaded, the sprite needs to be drawn.  In the **Player** class, you will find an overridden **Draw** method which we can fill in with the proper code.  This method will be passed a **SpriteBatch** object.  A **SpriteBatch** does exactly what its name implies: it draws a batch of sprites to the screen.  We will just add the player's **Texture2D** to that list by writing the following code:

	````C#
	public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Frames[0], Position);
	}
	````

	This code uses the **Frames** array from the Sprite class and draws the first (and only) element in that array.  Additionally, we pass in the current **Position** at which to draw the Sprite

1. At this point, go ahead and run the game.  You should now see that there is a player ship at the bottom of the screen, but it still doesn't move or shoot...
	
<a name="Ex1Task4"></a>
#### Task 4 - Getting the Player Moving ####

1. Open **AlienAttackGame.cs** from Solution Explorer.

1. Navigate to the **Update** method.  This method is called once per frame in order for you to update anything in the game world before it is drawn to the screen.  This is where we will read input and move the ship's position.

1. At the top of the **Update** method add the following to get the keyboard's current state:

	````C#
	_keyboardState = Keyboard.GetState();
	````

1. At the bottom of the **Update** method, right before the call to **base.Update**, add the following code, which will save the current frame's keyboard state so we can compare it on the next frame:

	````C#
	_lastKeyboard = _keyboardState;
	````

1. Now, let's read the arrow keys to determine if we should move the ship left or right.  Add the following code inside the null check on **_player**:

	(Code Snippet - _IntroMonoGame - ReadArrowKeys_)

	````C#
	if (_player != null)
	{
		if (_keyboardState.IsKeyDown(Keys.Left) && _player.Position.X > 0)
			_player.Velocity = -PlayerVelocity;
		else if (_keyboardState.IsKeyDown(Keys.Right) && _player.Position.X + _player.Width < ScreenWidth)
			_player.Velocity = PlayerVelocity;
		else
			_player.Velocity = Vector2.Zero;
			
		_player.Update(gameTime);
	}
	````

	This chunk of code uses the **IsKeyDown** method from the **KeyboardState** class to determine if a keyboard key is held down.  We check if the Left arrow key is down (**Keys.Left**) and, if it is, we set the **Velocity** propery on the player to the negative **PlayerVelcocity** value.  If the Right arrow key is down (**Keys.Right**), we set the velocity to the positive **PlayerVelocity** value.  Finally, if neither is held down, we set the **Velocity** property to **Vector2.Zero**, which will not move the ship in either direction this frame.

1. Now, run the game again.  You should now be able to move the ship left and right with the arrow keys!

<a name="Ex1Task5"></a>
#### Task 5 - Adding Player Firing ####

Our next task is to get our ship to shoot.  

1. Back in our **Update** method, just below where we added the last few lines of code to move the ship, let's add some code to check the state of the space bar, and fire a shot when it's pressed:

	````C#
	if((_keyboardState.IsKeyDown(Keys.Space) && !_lastKeyboard.IsKeyDown(Keys.Space) &&
			gameTime.TotalGameTime.TotalMilliseconds - _lastShotTime > ShotTime))
	{
		AddPlayerShot();
		_lastShotTime = gameTime.TotalGameTime.TotalMilliseconds;
	}
	````
This code will again use the **IsKeyDown** method to check whether the space bar is pressed (**Keys.Space**), but it also checked to make sure that both a) it wasn't pressed down on the last frame, and b) it has been more than **ShotTime** since the last time a shot was fired.  This ensures the player can't just hold the space bar down and fire shots continuously.  If those conditions are met, we call the **AddPlayerShot** method, and then save off the current time in the **_lastShotTime** member for comparison on subsequent frames.

1. At this point, you can run the game again and you will be able to fire shots from the player ship and destroy the aliens.  See the **HandleCollisions** and **UpdatePlayerShots** methods for more information on how the game does collision detection and moves the shots up the screen.

<a name="Ex1Task6"></a>
#### Task 6 - Adding Shot Sound Effect ####

Now that we can fire shots up the screen, it would be nice if there was a sound effect to accompany the action.  First, we need to load the proper effect using the Content object, just like we did with the player ship graphic.

1. Navigate to the **LoadContent** method.

1. Add the following code to load the PlayerShot sound effect:

	````C#
	_playerShot = Content.Load<SoundEffect>("sfx\\playerShot");
	````
1. Now that we have the effect loaded, we just need to play it when the shot is fired.  Navigate back to the **Update** method, and add the following line of code following the call to **AddPlayerShot**:

 	````C#
	 _playerShot.Play();
	 ````
That's it!  Now the game will fire shots and play the sound effect accordingly.

<a name="Ex1Task7"></a>
#### Task 7 - Drawing the Score ####

Throughout the game, we have been keeping score based on the player destroying the enemy ships.  For every ship destroyed, the player earns 100 points, which is stored in the **_score** member variable.  Let's draw this to the screen.

1. Navigate to the **Draw** method

1. Near the bottom of the method, prior to the call to **_spritebatch.End**, add the following lines of code:

	````C#
	Vector2 scoreSize = _font.MeasureString("Score: " + _score);
	_spriteBatch.DrawString(_font, "Score: " + _score, ScorePosition - scoreSize/2.0f, Color.Aqua);
	````
This code uses a **SpriteFont** to draw text to the screen.  A **SpriteFont** already exists in the project and is included in the Content Pipeline Tool.  This is a simple XML file that defines the font to use, its size, and other characteristics.  When the Pipeline Tool runs at compile time, a MonoGame-specific file is created which contains all of the alphanumeric symbols of the font as you specified.  In code, we can simply load that font, and use the **SpriteFont** class to draw text to the screen.

	In the example above, the first line of code uses the **MeasureString** method to determine the width and height of the rectangle that would be necessary to draw the string passed in.  The second line of code calls **DrawString** to actually draw the text to the screen, using the previously returned size to place it centered on the screen.  

<a name="Summary"></a>
## Summary ##

MonoGame is an extremely flexible framework that allows you to write a game in any way you see fit.  It does not force you to do things in a particular way. As a result, there are many different ways to write a game.  This module should have given you a grasp of:

- Processing and Loading Content
- Drawing Textures
- Playing Sound and Music.
- Drawing Text

Check out the much longer **README_Full.md** document in this repo to see how this game was built from absolute scratch, with a bit more context on proper game architecture.