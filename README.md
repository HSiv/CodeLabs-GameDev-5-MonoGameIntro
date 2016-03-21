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

> **Note:** Make sure you have checked all the dependencies for this module before running the setup.

<a name="Exercises" />
## Exercises ##

This module includes the following exercise:

1.  [Getting Started: Creating your first game](#Exercise1)

Estimated time to complete this module: **60 minutes**

> **Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.


<a name="Getting Started" />
### Getting Started: Creating your first game ###

In this exercise you will create your first MonoGame Windows 10 project. Since we only have an hour we'll be modifying an existing project to add functionality. The existing project is a space invaders style game. It is currently missing a player, so when you run the project you will only see aliens. During the course of this Lab you will be adding new code as well as uncommenting code existing code blocks.

<a name="Ex1Task1" />
#### Task 1 - Open Project ####

1. In **Visual Studio 2015** go to **File->Open Project**.

2. Navigate to the module folder and into **Source/Ex1/Begin**.

3. Open the **AlienAttackUniversal.sln**.

4. Once the project has loaded make sure your selected build configuration is set to "Debug" "x64".

5. Hit **F5** or click the Run **Local Machine** button.

The game should run and you will be presented with a Title screen. Hit space or enter and the game will start. At the moment only the aliens are present, the player is missing. Note if you don't seem **Local Machine** next to the run button check the **Configuration** you have selected. If it is not **x64** you will need to change it.

<a name="Ex1Task2" />
#### Task 2 - Add missing content ####

No game would be complete without graphics and sound. Fortunately, this session comes with a complete set of assets for you to use. In order to get the maximun performance for your game you will need to pre-process the assets into an optimized format. This can be done using the MonoGame Pipeline Tool. Its purpose is to task assets like .png/.wav and compress/optimize them for the platform you are targeting. In this case Windows 10. This will allow you to take advantage of things like texture compression which in turn will allow you to include more graphics! and who doesn't like more graphics.

Most of the content has already been added to the game, but some bits are missing. In this section we will use the Pipeline tool to add these assets to the game.

1. Click the **Content** folder in the Solution to expand it.

1. Double click on **Content.mgcb**. This should open the file in the MonoGame Pipeline Tool. If the file opens in the code editor. Right-click on **Content.mgcb** and click **Open With**. The select the **MonoGame Pipeline Tool**.

1. Right-click on the **gfx** folder in the pipeline tool and click **Add->Existing Folder**.

1. Navigate to **Begin\AlienAttackUniversal\Content\gfx\player** and click **Add**. 

1. Right-click on the **gfx** folder in the pipeline tool and click **Add->Existing Folder**.

1. Navigate to **Begin\AlienAttackUniversal\Content\gfx\pshot** and click **Add**. 

1. Right-click on the **sfx** folder in the pipeline tool and click **Add->Existing Item**.

1. Navigate to **Begin\AlienAttackUniversal\Content\sfx\playerShot.wav** and click **Add**.

That is how easy it is to add content to the game. Add the content is included automatically in you project as long as it is in the Content.mcgb file. Sometimes you have files that you just want to copy over (like .xml files). You can also add those to the Content.mgcb file but set the Build Action to "Copy" and they will also be included.

<a name="Ex1Task3" />
#### Task 3 - Add the Player ####

The project has a base class for our sprites adding new sprites is going to be very straightforward. This base class is called "Sprite" and if you take a look at the code you will see it handles allot of things. Animation is one of them as well as handling updating a sprites position using its Velocity. Right now we want to add a new Sprite for our Player. 

1. Right-click on the **Sprites** folder and click **Add->Class**. Call this class **Player.cs**.

1. Change the **Player** so it derives from **Sprite**.

	````C#
	class Player : Sprite	
	{
	}
	````

1. We now need to load the textures for this sprite. So let's just add a constructor and call LoadContent in it.

	````C#
	public Player()
	{
		LoadContent(AlienAttackGame.Instance.Content,  "gfx\\player\\player");
	}
	````

That is it! All the other logic is handled in the Sprite class. At this point you should probabl take a look at the Sprite class. If we look at the *LoadContent* method you will see the following code.

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

As you can see this method makes use of the *ContentManager* class to load the texture. The *ContentManager* is the main way to load any kind of asset which has been processed by the content Pipeline. An instance is created on the Game class and is accessable via the *.Content* property. You can see we pass in ```AlienAttackGame.Instance.Content``` into the LoadContent method. 

The *ContentManager* provides a generic Load method for loading assets. You can use this to load *Texture2D*, *SpriteFont* and other content types. It includes type checking. So if you attempt to load a song into a Texture2D the manager will throw an error. 

<a name="Ex1Task4" />
#### Task 4 - Getting the Player Moving ####

This task will involve uncommenting some existing code. In this section we will update the **GameScreen** to display and move our player. All the relevant code sections have been marked with a `//TODO Uncomment <Task> <Step>`. Just follow the **//TODO** items.

1. Open the Task List in **Visual Studio 2015**. **View->TaskList**.

1. Look for **Ex1Task4 - Step 1** Double click on it and then uncomment the code 

	````C#
	private Player _player;
	````

1. Go to **Ext1Task4 - Step 2** and uncomment the code.

	````C#
	private readonly Player _livesIcon;
	````

1. Go to **Ext1Task4 - Step 3** and uncomment the code.

	````C#
	_player = new Player();
	_player.Position = new Vector2(AlienAttackGame.ScreenWidth / 2 - _player.Width / 2, AlienAttackGame.ScreenHeight - 120);
	````

1. Go to **Ext1Task4 - Step 4** and uncomment the code.

	````C#
	_livesIcon = new Player();
	_livesIcon.Position = new Vector2(20, AlienAttackGame.ScreenHeight - 80);
	_livesIcon.Scale = new Vector2(0.5f, 0.5f);
	````

1. Go to **Ext1Task4 - Step 5** and uncomment the code. This is the entire MovePlayer method. In this method we make use of the InputManager.ControlState to decide if we need to move the player left or right. Depending on the result we alter the velocity which will then be used in the Sprite.Update method to move the player.

	````C#
	_livesIcon = new Player();
	_livesIcon.Position = new Vector2(20, AlienAttackGame.ScreenHeight - 80);
	_livesIcon.Scale = new Vector2(0.5f, 0.5f);
	````

1. Go to **Ext1Task4 - Step 6** and uncomment the code. Note how we always pass gameTime into the methods dealing with Update and Draw. This is so we can update things based on the amount of time that has elapsed between frames.

	````C#
	MovePlayer(gameTime);
	````

1. Go to **Ext1Task4 - Step 7** and uncomment the code.

	````C#
	// draw the player
	if (_player != null)
		_player.Draw(gameTime, _spriteBatch);
	````

1. Hit **F5** or click the Run **Local Machine** button.

You should now see our space ship at the bottom of the screen. If you press the **Left/Right** keys, you will be able to move the ship.

<a name="Ex1Task5" />
#### Task 5 - Add the PlayerShot ####

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

	````C#
	public PlayerShot()
	{
		LoadContent(AlienAttackGame.Instance.Content, "gfx\\pshot\\pshot_{0}", 3);
		Velocity = new Vector2(0, -300 / 1000.0f);
	}
	````

1. We also need to override the **Update** method. This will let use update the animation for this sprite.

	````C#
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		AnimateReverse(gameTime, 100);
	}
	````

<a name="Ex1Task6" />
#### Task 6 - Shooting ####

1. Open the **Task List** in **Visual Studio 2015** by selecting **View->TaskList**.

1. Look for **Ex1Task6 - Step 1**. Double click on it and then uncomment the code.

	````C#
	private readonly List<PlayerShot> _playerShots;
	````

1. Look for **Ex1Task6 - Step 2**. Double click on it and then uncomment the code.
 
	````C#
	_playerShots = new List<PlayerShot>();
	````

1. Look for **Ex1Task6 - Step 3**. Double click on it and then uncomment the code. This code is the entire **UpdatePlayerShots** method. you should see an error in the code. **AudioManager.Cue.PlayerShot** is not defined. We will get to that shortly.

1. Look for **Ex1Task6 - Step 4**. Double click on it and then uncomment the code.

	````C#
	UpdatePlayerShots(gameTime);
	````

1. Look for **Ex1Task6 - Step 5**. Double click on it and then uncomment the code.

	````C#
	// draw the player shots
	foreach (PlayerShot playerShot in _playerShots)
		playerShot.Draw(gameTime, _spriteBatch);
	````

1. Look for **Ex1Task6 - Step 6** Double click on it and then uncomment the code. This fixes the error with  "AudioManager.Cue.PlayerShot" you may have noticed earlier.

	````C#
	PlayerShot,
	````

1. Look for **Ex1Task6 - Step 7**. Double click on it and then uncomment the code.
 
	````C#
	private static readonly SoundEffect _playerShot;
	````

1. Look for **Ex1Task6 - Step 8**. Double click on it and then uncomment the code.

	````C#
	_playerShot = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\playerShot");
	````

1. Look for **Ex1Task6 - Step 9**. Double click on it and then uncomment the code.

	````C#
	case Cue.PlayerShot:
			_playerShot.Play();
			break;
	````

1. Hit **F5** or click the Run **Local Machine** button.

Now when the game runs you should be able to fire using the space bar.

<a name="Ex1Task7" />
#### Task 7 - Blowing stuff up! ####

So far we have moving aliens, moving players and lots of shooting, but not explisions. In this task we will add the final bits for the game. Most of the code we will be looking at will be collision handling. for sprite based games collisions are normally handled by checking to see if BoundingBoxes are intersecting. MonoGame has a **BoundingBox** class. The **Sprite** class has an boundingbox property which defines where the sprite is in the game. We can use the bounding boxes from two different sprites to see if they collide. This is how we detect if a shot has hit a player.

1. Open the **Task List** in **Visual Studio 2015** by selecting **View->TaskList**.

1. Look for **Ex1Task7 - Step 1**. Double click on it and then uncomment the code. This is a large chunk of Collision handling code. The methods are HandlePlayerShotCollision, HandleEnemyShotCollision and HandleEnemyPlayerCollision.

1. Look for **Ex1Task7 - Step 2**. Double click on it and then uncomment the code. Again this is a fair size chunk of collision code. 

1. Look for **Ex1Task7 - Step 3**. Double click on it and then uncomment the code. 

	````C#
	_playerShots.Clear();
	_player = new Player();
	_player.Position = new Vector2(AlienAttackGame.ScreenWidth / 2 - _player.Width / 2, AlienAttackGame.ScreenHeight - 100);
	````

1. Look for **Ex1Task7 - Step 4**. Double click on it and then uncomment the code. 

	````C#
	_playerShots.Clear();
	````

1. Hit **F5** or click the Run **Local Machine** button.

That should be everything. You now have a fully functioning game!

<a name="Summary" />
## Summary ##

The MonoGame framework does not force you to do things in a particular way. As a result, there are many different ways to write a game. This module should have given you a grasp of:

- Processing and Loading Content
- Drawing Textures
- Drawing Text
- Playing Sound and Music.
- Structuring you code so you can reuse functionality.
