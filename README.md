# BingoTest
A simple Unity bingo game to test new and old functionality in.

## Gameplay
The game starts with a simple Main Menu scene with only a Play button available that takes the player through a Loading scene into the Game scene.

In the Game scene the player is presented with a 'Rules' window that allows the player to set with how many cards they want to play (1-3) and how many bingos they need to find in order to finish the game (1-12, increasing with more cards until 36). Click the Begin button to start.

The player is given 5 seconds to interpret the cards/get familiar with the setup, after which a stopwatch is enabled, showing up at the right top. At the 5s mark and every 5s afterwards a number is called (shown as a bingo ball in the left top), allowing the player to mark that number on their bingo card if they have it. The 4 most recently called numbers will stay on the screen.
The Game can at all times be paused, giving the player also the option to look at all previously called numbers.

Once all the necessary bingos are found, a window is shown with results, showing their chosen options and the time they completed it in. They can choose to go back to the Main Menu from here.

## Implementation
A loader manages the scene-switching, asynchronously loading the game scene while showing the loading scene, giving 1 Update tick delay.

The Game itself is managed with a Singleton GameManager holding the states: 
- Rules ('Rules' window), 
- SetUp (5s analyzing phase),
- Play (actual gameplay), 
- Pause, 
- Results (stops the game and shows results), 
- End (loading back to the main menu).

A UIManager is subscribed to the state changes to enable/disable state-respective canvases.

The BingoManager manages the game logic and is also subscribed to the state changes, handling both the SetUp of the game as the GamePlay and the Pause functionality. This also sends the results through to the 'Results' window. This logic relies on the Stopwatch for spawning new called numbers.

Depending on player choice, the BingoManager will make 1-3 BingoCard objects, which create new gameobjects for every cell of the bingo card. The specific cell information is contained within a separate BingoCell class, which keeps track of the state of the cell (if its marked or not, and following that, what color the cell should be).

In the BingoCard-class there is also the bingo-checking logic, which, if a new cell has been marked, will loop through all available cells in an efficient manner, to check if a new bingo was found. This already also has a system for checking if a double- or triple-bingo was found, but these are currently only displayed in Debug.Log messages. If a new bingo was found, the number of bingos is subtracted from the total bingos remaining to be found. At 0 the game ends, and the results are shown.
