using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    ///     Number of bingos found after a click by the player.
    /// </summary>
    private int totalBingoCards;

    private void Start() {
        totalBingoCards = BingoManager.Instance.TotalBingoCards;
    }

    /// <summary>
    ///     Check for player clicks.
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BingoCell bingoCell;
            int bingosFound = 0;
            BingoManager bingoManager = BingoManager.Instance;

            // Check if the position that got clicked has a bingoCell.
            for (int i = 0; i < totalBingoCards; i++) {
                bingoCell = bingoManager.BingoCards[i].GetCell(GetMouseWorldPosition());

                if (bingoCell != null) {
                    // If it does, check if the cell can get marked.
                    if (bingoCell.MarkCell(bingoManager.CalledNumbers)) {
                        // If it does, check the card if a new bingo is found.
                        bingosFound = bingoCell.BingoCard.CheckForBingo();
                    }
                    break;
                }
            }
            bingoManager.SubtractFoundBingos(bingosFound);
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
