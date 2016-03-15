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
