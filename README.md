# BingoTest
A simple Unity bingo game to test new and old functionality in.

## Gameplay
The game starts with a simple Main Menu scene with only a Play, Options, Credits and Quit button available. The options button lets you configure the music and SFX volume. When play is pressed the player is taken through a Loading scene into the Game scene.

In the game scene the player is presented with a 'Rules' window that allows the player to set with how many cards they want to play (1-3) and how many bingos they need to find in order to finish the game (1-12, increasing with more cards until 36). Click the Begin button to start.

The player is given 6 seconds to interpret the cards/get familiar with the setup, after which a stopwatch is enabled, showing up at the right top. At the 5s mark and every 5s afterwards a number is called (shown as a bingo ball in the left top), allowing the player to mark that number on their bingo card if they have it. The 4 most recently called numbers will stay on the screen.
The game can at all times be paused, with the quit button. This will open a prompt to return to the main menu. During the game the player is also able to change the sound settings.

Once all the necessary bingos are found, a window is shown with results, showing their chosen options and the time they completed it in. They can choose to go back to the Main Menu from here or to instantly start a new game. No data is saved.

## Implementation
A loader manages the scene-switching, asynchronously loading the game scene while showing the loading scene, giving 1 Update tick delay.

The Game itself is managed with a Singleton GameManager holding the states: 
- Rules ('Rules' window), 
- SetUp (5s analyzing phase),
- Play (actual gameplay), 
- Pause,
- TearDown (breaks down the game scene),
- Results (stops the game and shows results).

A UIManager is subscribed to the state changes to enable/disable state-respective canvases.

The GameManager also manages the game logic and creates and keeps track of the bingo cards and bingos found. This also sends the results through to the 'Results' window. This logic relies on the Stopwatch for spawning new called numbers (and therefore game progress).

The called numbers will be spawned from, and put into, an object pool. This prevents an abundance of unused objects and also means after the first 4 objects there is no need to instantiate more.

Depending on player choice, the GameManager will make 1-3 BingoCard objects, which create new child-gameobjects for every cell of the bingo card. The specific cell information is contained within a separate BingoCell class, which keeps track of the state of the cell (if its marked or not, and following that, what color the cell should be).

The Player-class handles all action related to player input. It checks where the player clicked, what cell that matches with (or none) and then marks the correct cell. This will in turn also, if a new cell has been marked, loop through all available cells related to the newly marked cell, to check if a new bingo was found. This already also has a system for checking if a double- or triple-bingo was found, but these are currently only displayed in Debug.Log messages. If a new bingo was found, the number of bingos is subtracted from the total bingos remaining to be found and a sound is played. At 0 the game ends, and the results are shown.

This game uses LeanTween for in-script UI animations (and it works pretty nicely).
