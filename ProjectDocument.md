# Game Basic Information #

## Summary ##

**A paragraph-length pitch for your game.**

## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**


**If you did work that should be factored in to your grade that does not fit easily into the proscribed roles, add it here! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## User Interface (Alex Long)

### Main Menu
![](./ExampleImages/MainMenu.png)


The main menu is the first screen that the user sees when they start the game. It contains the game title, start button, credits button, and the quit game button. All the buttons have a hover effect that expands the font size of the button text when you hover over them. The hover effect was created by using two animation clips ([OnButtonHover](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/User%20Interface%20Animations/OnButtonHover.anim) and [OnButtonLeave](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/User%20Interface%20Animations/OnButtonLeave.anim)) which was responsible for expanding the font size when the button was highlighted and decreasing it back to the normal size when the button is no longer highlighted. After the two animations were created, I added them into the MainMenuButtons animator controller and created the connections between the different states so that the buttons would play the correct animation based on whether they were highlighted or not. This animator controller is used for the buttons’ hover effect. The use case of a hover effect is not only to provide aesthetic pleasure, but also to provide user feedback so that they know it is a clickable UI element. I have attached a screenshot of the animator controller flow chart below for better visualization. 


![](./ExampleImages/HoverEffectAnimatorFlowChart.png)


When any of the main menu buttons are clicked, it calls the corresponding function from the [MainMenu](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/MainMenu.cs) script which changes the game scene. For example, clicking on the “Start” button will load the level scene and clicking on the “Credits” button will load the credits scene. We also have a “Quit Game” button in case the user wants to exit the application. This scene switching logic is handled by the GameManager so I had to work closely with Alec (GameLogic) to ensure the gameplay logic is integrated successfully. In order for the script to work properly for the buttons, I had to add the MainMenu script and the corresponding button function to the OnClick configuration in Unity. 

![](./ExampleImages/StartOnClick.png)


In addition, the main menu also has an animated background which infinitely loops over the background image by scrolling to the right at a gradual speed. This was achieved by setting the background image’s wrap mode to “Repeat” and creating the [BackgroundImageScroller](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/BackgroundImageScroller.cs) script to scroll the image to the right.

### Credits Scene


![](./ExampleImages/CreditsScreen.png)


The user can reach this screen by clicking on the “Credits” button on the main menu. There is a “Back” button on the top left corner of the screen which allows the user to go back to the main menu screen when clicked. The scene switching logic is handled using the [CreditScreen script](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/CreditScreen.cs) which is attached to the button. In addition, there is a rolling credits [animation](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/User%20Interface%20Animations/Credits%20Scroll%20Animation.anim) which is responsible for gradually moving the credits from the bottom of the screen to the top and repeating afterwards. This was achieved by using the animation tool and placing the credits game object outside of the screen at the bottom during the start of the recording and placing it at the top of the screen near the end of the recording. After the animation file has been created, we simply attach it to the credits game object and the animation will play automatically once the scene loads up. 

### Pause Menu

![](./ExampleImages/PauseMenu.png)


The pause menu was created by using an overlay with a z index that is larger than the rest of the other UI elements so that the user can interact with the buttons instead of the in-game inputs. The user can open the pause menu by either clicking on the pause icon located at the top right corner of the game, or by pressing the escape key(desktop) or the start key(console). In this menu, I reused the same buttons from the main menu screen and added an additional resume button, as well as an audio slider so the user can adjust the volume. When the user clicks on the resume button, we simply deactivate the overlay game object so they can interact with the game again. The [PauseMenu script](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/PauseMenu.cs) is responsible for implementing the game logic for the buttons on this screen and was created in collaboration with Alec Atienza (Game Logic) and Cameron Yee (Input). 

The audio slider was created by using the Slider UI game object and attaching the [VolumeSlider script](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/AudioScripts/VolumeSlider.cs) to implement the volume adjustment logic. I will be explaining more about the audio implementation in the Audio section down below. 

### End Screen

![](./ExampleImages/EndScreen.png)


The end screen appears when the pearl successfully collides with the crown when thrown by the player. It is implemented the same way as the pause menu described above with a few adjustments to the UI elements and game logic. The crown is located at the top of the map so the screenshot shown above is for demonstration purposes. When this screen appears, the player can either restart the game, quit the game, or go back to the main menu by clicking on the corresponding buttons. At the top of the screen, we have a congratulatory message and a set of metrics to inform the user about their completion time and how many pearls it took for them to win the game. I used the metric values that were stored by the Game Manager to update the UI elements which was done using the following [lines](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/EndScreen.cs#L45-L55) in the EndScreen script. An edge case I had to account for was that the user should not be able to open the pause menu when they have reached this screen, as it logically doesn’t make sense and it clutters up the UI elements. This was done by adding an additional check for the current state of the game using the Game Manager in the [PauseScreen script](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/PauseMenu.cs#L49) and only allowing the player to open the pause menu if they have not won the game yet. The EndScreen script was also created in collaboration with Alec Atienza (Game Logic) and Cameron Yee (Input). 

### In-game UI

![](./ExampleImages/InGameUI.png)

The in-game UI consists of several elements: stats, mute audio button, and a pause button. The stats located at the top displays the number of pearls the player has thrown and a timer that keeps track of how long it takes the player to win the game. The UI is updated by calling the values stored in the Game Manager using the [PearlsThrownStatsUI](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/PearlsThrownStatsUI.cs) and the [CompletionTimeUI](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/CompletionTimeUI.cs) scripts. 

The audio button located next to the pause button allows the user to conveniently mute any in-game audio by clicking on it. When the audio is muted, the audio icon changes to a muted audio icon image to reflect the current state of the game. This behavior was implemented in the [ToggleAudio Icon script](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/ToggleAudioIcon.cs) which takes in two sprite images and the audio button game object. The script has a boolean flag to keep track of whether the player is in the muted or unmuted state which allows us to update the button image to reflect the corresponding state. In addition, the actual implementation of pausing the audio is done using the AudioListener.pause property. 

The pause button located at the top right corner allows the user to pause the game when clicked on. This calls the [TogglePauseMenu function](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/UI%20Scripts/PauseMenu.cs#L77-L81) in the PauseMenu script which updates the game state using the game manager so that the proper UI is displayed.


### Camera Controller

The [camera controller](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/CameraController.cs) implemented in our game is similar to the PositionFollowCameraController implemented in exercise 2 with a few minor adjustments. Our camera is centered on the player by default when a pearl is not thrown or if they have teleported to a new location. When the player throws a pearl that goes out of the screen, the camera is then centered on the pearl until the player has teleported to the designated location. It is important to note that the camera moves at a speed of 0.5 when it refocuses on a different target, which allows for a smooth camera movement as opposed to immediately centering on the target. To check whether the pearl is thrown off the screen, we had to convert the pearl’s position using the WorldToViewportPoint function and if its x and y values are not between 0-1, and the z value is not greater than 0, then it is out of the screen. Once this happens, we update the boolean flag and refocus our camera on the pearl by using the Vector3.MoveTowards function. If the player has teleported to the location, we reset the boolean flag to false and refocus the camera back onto the player. This script was created in collaboration with Devin Ly (Level Design). 

### UI Resources

- [Creating a Title Screen Menu](https://www.youtube.com/watch?v=BjEqZfK15Ws)

- [Rolling Credits Scene](https://www.youtube.com/watch?v=cj6hwCjiVZE)

- [Background Animation Scroller](https://www.youtube.com/watch?v=A5YSbgqr3sc)

- [Button Hover Effect](https://www.youtube.com/watch?v=jh3zD-wGBnw)

- [Changing Button Image](https://www.youtube.com/watch?v=__M37MbOa8Q)

- [Checking if GameObject is out of the Screen](https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html)

- [Creating an Audio Slider](https://johnleonardfrench.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/)

- [Bebas Neue Font](https://fonts.google.com/specimen/Bebas+Neue)

Icons:

- [Pause Icon](https://icons8.com/icon/GECJlGuEdNiC/audio)

- [Unmuted Audio Icon](https://icons8.com/icon/mOCY88ZDOzkO/audio)

- [Muted Audio Icon](https://icons8.com/icon/VYymocF7cTKJ/audio)

- [Clock Icon](https://icons8.com/icon/8YCHhvwCdMI0/clock)


## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

# Sub-Roles

## Audio (Alex Long)

[Audio Citations](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Resources/Audio/README.md)

### Background Music

Each scene has a different background music that plays when the scene is successfully loaded. The general theme for the music collection is upbeat and relaxing to bring out positive energy to the players despite this being a rage game. Every scene has an audio source game object which is responsible for playing the background music upon awake and on loop. For example, I used the [CreditsBackgroundMusic](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Resources/UI/Prefabs/CreditsBackgroundMusic.prefab) audio source object in the credits scene to play the background music. 

### Sound Effects

There are three in-game sound effects that are generated based on different scenarios:
- When the player releases the pearl 
- When the pearl collides with the surface 
- When the player teleports 
 
To manage the different sound effects, I created a [SoundEffectManager](https://github.com/Jalastin/ECS-189L-Project/blob/fmain/ECS%20189L%20Game/Assets/Scripts/AudioScripts/SoundEffectManager.cs) script which was attached to the SoundManager game object. The SoundManager object is used in both the PlayerController and the Pearl Controller script to play the corresponding sound effects by calling the functions within the SoundEffectManager script. For [example](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/Pearl%20Scripts/PearlController.cs#L36), when the pearl collides with a surface, the sound manager object will call the function to play the corresponding sound. 

### Volume Slider

The volume slider allows players to conveniently adjust the audio to their level of comfort. This was achieved by creating a [SoundMixer](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Resources/Audio/BackgroundMusic/SoundMixer.mixer) using the Audio Mixer Controller tool. This allows us to control the output volume of the different audio sources we have by exposing the audio mixer parameter to the [VolumeSlider script](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/AudioScripts/VolumeSlider.cs) . The script is responsible for updating both the audio slider UI element, to reflect the latest adjusted volume value, and the Audio Mixer based on user input from the audio slider. Everytime the user adjusts the volume via the audio slider, the [SetVolume](https://github.com/Jalastin/ECS-189L-Project/blob/main/ECS%20189L%20Game/Assets/Scripts/AudioScripts/VolumeSlider.cs#L22-L27) function is called to update the Audio Mixer output and the GameManager with the latest volume value so that it persists across all the scenes.

### Audio Resources

[Audio Mixer](https://johnleonardfrench.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/)

[Adding Sound Effects](https://www.youtube.com/watch?v=JnbDxG04i7)

[Adding Background Music](https://www.youtube.com/watch?v=BKCsH8mQ-lM)

[Audio Mute](https://www.youtube.com/watch?v=AFcHsKd_aMo)

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**
