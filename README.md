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

<a name="Exercises" />
## Exercises ##
This module includes the following exercise:

1.  [Getting Started : Creating your first game](#Exercise1)

Estimated time to complete this module: **60 minutes**

**Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.


<a name="Getting Started" />
### Getting Started : Creating your first game ###

In this exercise you will create your first MonoGame Windows 10 project. Since we only have a hour we'll be modifying an existing project to add functionality. The existing project is a space invaders style game. It is currently missing a player, so when you run the project you will only see aliens. During the course of this Lab you will be adding new code as well as uncommenting code existing code blocks.

<a name="Ex1Task1" />
#### Task 1 - Open Project ####

1. In Visual Studio 2015 Goto File->Open Project
2. Navigate to the Labs folder and into "Source/Ex1/Begin"
3. Open the "AlienAttackUniversal.sln"
4. Once the project has loaded make sure your selected build configuration is set to "Debug" "x64"
5. Hit F5 or click the Run "Local Machine" button.

The game should run and you will be presented with a Title screen. Hit space or enter and the game will start. At the moment only the aliens are present, the player is missing. Note if you don't seem "Local Machine" next to the run button check the Configuration you have selected. If it is not "x64" you will need to change it.

<a name="Ex1Task2" />
#### Task 2 - Add missing content ####

No game would be complete without graphics and sound. Fortunately this session comes with a complete set of assets for you to use. 
In order to get the maxumin performance for your game you will need to pre-process the assets into a optimized format. This can be done using the MonoGame Pipeline Tool. Its purpose is to task assets like .png/.wav and compress/optimize them for the platform you are targeting. In this case Windows 10. This will allow you to take advantage of things like texture compression which in turn will allow you to include more graphics! and who doesn't like more graphics.

Most of the content has already been added to the game, but some bits are missing. In this section we will use the Pipeline tool to add these assets to the game.

1. Click on the "Content" folder in the Solution to expand it.
2. Double click on "Content.mgcb". This should open the file in the MonoGame Pipeline Tool. 
    If the file opens in the code editor. Right Click on "Content.mgcb" and click Open With. The select the "MonoGame Pipeline Tool"
3. Right Click on the "gfx" folder in the pipeline tool and click Add->Existing Folder
4. Navigate to "Begin\AlienAttackUniversal\Content\gfx\player"
5. Click Add. 
6. Right Click on the "gfx" folder in the pipeline tool and click Add->Existing Folder
7. Navigate to "Begin\AlienAttackUniversal\Content\gfx\pshot"
8. Click Add. 
6. Right Click on the "sfx" folder in the pipeline tool and click Add->Existing Item
7. Navigate to "Begin\AlienAttackUniversal\Content\sfx\playerShot.wav"
11. Click Add.

That is how easy it is to add content to the game. Add the content is included automatically in you project as long as it is in the Content.mcgb file. Some times you have files that you just want to copy over (like .xml files). You can also add those to the Content.mgcb file but set the Build Action to "Copy" and they will also be included.

<a name="Ex1Task3" />
#### Task 3 - Add the Player ####

The project has a base class for our sprites adding new sprites is going to be very straighforward. This base class is called "Sprite" and if you take a look at the code you will see it handles allot of things. Animation is one of them as well as handling updating a sprites position using its Velocity. Right now we want to add a new Sprite for our Player. 

1. Right click on the Sprites folder and click Add->Class. Call this class Player.cs 
2. Change the Player so it derives from Sprite
```csharp
	class Player : Sprite	
	{
	}
```

3. We now need to load the textures for this spirte. So lets just add a constructor and call LoadContent in it.
```csharp
	public Player()
	{
		LoadContent(AlienAttackGame.Instance.Content,  "gfx\\player\\player");
	}
```

That is it! All the other logic is handled in the Sprite class. 


<a name="Ex1Task4" />
#### Task 4 - Getting the Player Moving ####

This task will involve uncommenting some existing code. In this section we will update the GameScreen to display and move our player. All the relevant code sections have been marked with a 

	//TODO Uncomment <Task> <Step>
	
Just follow the //TODO items.

1. Open the Task List in Visual Studio 2015. View->TaskList
2. Look for Ex1Task4 - Step 1 Double click on it and then uncomment the code 
```csharp
	private Player _player;
```
3. Go to Ext1Task4 - Step 2 and uncomment the code.
```csharp
	 private readonly Player _livesIcon;
```
4. Go to Ext1Task4 - Step 3 and uncomment the code.
```csharp
	_player = new Player();
        _player.Position = new Vector2(AlienAttackGame.ScreenWidth / 2 - _player.Width / 2, AlienAttackGame.ScreenHeight - 120);
```
5. Go to Ext1Task4 - Step 4 and uncomment the code.
```csharp
	_livesIcon = new Player();
        _livesIcon.Position = new Vector2(20, AlienAttackGame.ScreenHeight - 80);
        _livesIcon.Scale = new Vector2(0.5f, 0.5f);
```
6. Go to Ext1Task4 - Step 5 and uncomment the code. This is the entire MovePlayer method. In this method we make use of the InputManager.ControlState to decide if we need to move the player left or right. Depending on the result we alter the velocity which will then be used in the Sprite.Update method to move the player.
```csharp
	_livesIcon = new Player();
        _livesIcon.Position = new Vector2(20, AlienAttackGame.ScreenHeight - 80);
        _livesIcon.Scale = new Vector2(0.5f, 0.5f);
```
7. Go to Ext1Task4 - Step 6 and uncomment the code. Note how we always pass gameTime into the methods dealing with Updat and Draw. This is so we can update things based on the amount of time that has elasped between frames.
```csharp
	MovePlayer(gameTime);
```
8. Go to Ext1Task4 - Step 7 and uncomment the code.
```csharp
	// draw the player
        if (_player != null)
        	_player.Draw(gameTime, _spriteBatch);
```
9. Hit F5 or click the Run "Local Machine" button.

You should now see our space ship at the bottom of the screen. If you press the Left/Right keys you will be able to move the ship.

<a name="Ex1Task5" />
#### Task 5 - Add the PlayerShot ####

Our next task is to get our ship to shoot. 

1. Right click on the Sprites folder and click Add->Class. Call this class PlayerShot.cs 
2. Change the PlayerShot so it derives from Sprite
```csharp
	class PlayerShot : Sprite
```

3. Add the following using clause
```csharp
	using Microsoft.Xna.Framework;
```	

4. We now need to load the textures for this spirte. So lets just add a constructor and call LoadContent in it. We also need to set the Velocity for this sprite. Note we are using an animated sprite this time, there are 3 frames availalbe pshot_0, pshot_1 and pshot_2
```csharp
	public PlayerShot()
	{
		LoadContent(AlienAttackGame.Instance.Content, "gfx\\pshot\\pshot_{0}", 3);
		Velocity = new Vector2(0, -300 / 1000.0f);
	}
```		

5. We also need to override the Update method. This will let use update the animation for this sprite.
```csharp
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		AnimateReverse(gameTime, 100);
	}
```


<a name="Ex1Task6" />
#### Task 6 - Shooting ####


1. Open the Task List in Visual Studio 2015. View->TaskList
2. Look for Ex1Task6 - Step 1 Double click on it and then uncomment the code 
```csharp
	private readonly List<PlayerShot> _playerShots;
```
3. Look for Ex1Task6 - Step 2 Double click on it and then uncomment the code 
```csharp
	 _playerShots = new List<PlayerShot>();
```
4. Look for Ex1Task6 - Step 3 Double click on it and then uncomment the code. This code is the entire "UpdatePlayerShots" method. you should see an error in the code. "AudioManager.Cue.PlayerShot" is not defined. We will get to that shortly.
5. Look for Ex1Task6 - Step 4 Double click on it and then uncomment the code 
```csharp
	UpdatePlayerShots(gameTime);
```
6. Look for Ex1Task6 - Step 5 Double click on it and then uncomment the code 
```csharp
	// draw the player shots
        foreach (PlayerShot playerShot in _playerShots)
        	playerShot.Draw(gameTime, _spriteBatch);
```
7. Look for Ex1Task6 - Step 6 Double click on it and then uncomment the code. This fixes the error with  "AudioManager.Cue.PlayerShot" you may have noticed earlier.
```csharp
	PlayerShot,
```
8.  Look for Ex1Task6 - Step 7 Double click on it and then uncomment the code 
```csharp
	private static readonly SoundEffect _playerShot;
```
9.  Look for Ex1Task6 - Step 8 Double click on it and then uncomment the code 
```csharp
	_playerShot = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\playerShot");
```
10.  Look for Ex1Task6 - Step 9 Double click on it and then uncomment the code 
```csharp
	case Cue.PlayerShot:
                    _playerShot.Play();
                    break;
```
11. Hit F5 or click the Run "Local Machine" button.

Now when the game runs you should be able to fire using the space bar.

<a name="Ex1Task7" />
#### Task 7 - Blowing stuff up! ####

so far we have moving aliens, moving players and lots of shootng, but not explisions. In this task we will add the final bits for the game. Most of the code we will be looking at will be collision handling. for sprite based games collisions are normally handled by checking to see if BoundingBoxes are intersecting. MonoGame has a BoundingBox class. The Sprite class has an boundingbox property which defines where the sprite is in the game. We can use the bounding boxes from two different sprites to see if they collide. This is how we detect if a shot has hit a player.

1. Open the Task List in Visual Studio 2015. View->TaskList
2. Look for Ex1Task7 - Step 1 Double click on it and then uncomment the code. This is a large chunk of Collision handling code. The methods are HandlePlayerShotCollision, HandleEnemyShotCollision and HandleEnemyPlayerCollision.
3. Look for Ex1Task7 - Step 2 Double click on it and then uncomment the code. Again this is a fair size chunk of collision code. 
```csharp
	private readonly List<PlayerShot> _playerShots;
```
3. Look for Ex1Task7 - Step 3 Double click on it and then uncomment the code. 
```csharp
	_playerShots.Clear();
        _player = new Player();
        _player.Position = new Vector2(AlienAttackGame.ScreenWidth / 2 - _player.Width / 2, AlienAttackGame.ScreenHeight - 100);
```
4. Look for Ex1Task7 - Step 4 Double click on it and then uncomment the code. 
```csharp
	_playerShots.Clear();
```
5. Hit F5 or click the Run "Local Machine" button.


That should be everything. You now have a fully functioning game!
