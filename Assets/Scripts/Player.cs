using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    ///     Reference to the bingo game manager.
    /// </summary>
    private BingoGame bingoGame;

    /// <summary>
    ///     Array of cards this player is using this game.
    /// </summary>
    private BingoCard[] cards;

    /// <summary>
    ///     Most recently clicked bingo cell.
    /// </summary>
    private BingoCell bingoCell;

    /// <summary>
    ///     Whether the game has already begun.
    /// </summary>
    private bool hasGameBegun = false;

    /// <summary>
    ///     Number of bingos found after a click by the player.
    /// </summary>
    private int bingosFound = 0;

    /// <summary>
    ///     Setup this player's cards based on input.
    /// </summary>
    public void SetUp(BingoGame bingoGame)
    {
        this.bingoGame = bingoGame;
        cards = new BingoCard[bingoGame.BingoCards];
        hasGameBegun = true;

        if (bingoGame.BingoCards == 1) {
            cards[0] = new BingoCard(5, 5, 5f, Color.white, 10, new Vector3(-12.5f, -12.5f));
        } else if (bingoGame.BingoCards == 2) {
            cards[0] = new BingoCard(5, 5, 5f, Color.white, 10, new Vector3(7.5f, -12.5f));
            cards[1] = new BingoCard(5, 5, 5f, Color.cyan, 10, new Vector3(-32.5f, -12.5f));
        } else {
            cards[0] = new BingoCard(5, 5, 5f, Color.white, 10, new Vector3(-52.5f, -12.5f));
            cards[1] = new BingoCard(5, 5, 5f, Color.cyan, 10, new Vector3(-12.5f, -12.5f));
            cards[2] = new BingoCard(5, 5, 5f, Color.white, 10, new Vector3(27.5f, -12.5f));
        }
    }

    /// <summary>
    ///     Check for player clicks.
    /// </summary>
    private void Update()
    {
        if (hasGameBegun) {
            bingosFound = 0;

            if (Input.GetMouseButtonDown(0))
            {
                // Check if the position that got clicked has a bingoCell.
                // If it does, check if the cell can get marked and if it does,
                // check the card if a new bingo is found.
                for (int i = 0; i < bingoGame.BingoCards; i++) {
                    bingoCell = cards[i].GetCell(GetMouseWorldPosition());

                    if (bingoCell != null) {
                        if (bingoCell.MarkCell(bingoGame.CalledNumbers)) {
                            bingosFound = bingoCell.BingoCard.CheckForBingo();
                        }
                        break;
                    }
                }
                bingoGame.SubtractFoundBingos(bingosFound);
            }

            if (bingoGame.BingosLeft <= 0) {
                hasGameBegun = false;
            }
        }
    }

    /// <summary>
    ///     Get world position from mouse position.
    /// </summary>
    /// <returns>World position from mouse position.</returns>
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }
}
