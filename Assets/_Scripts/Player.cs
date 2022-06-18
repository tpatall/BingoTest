using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip markedCell;
    [SerializeField] private AudioClip foundBingo;
    [SerializeField] private AudioClip gameOver;

    /// <summary>
    ///     Number of bingos found after a click by the player.
    /// </summary>
    private int totalBingoCards;

    private void Start() {
        totalBingoCards = GameManager.Instance.TotalBingoCards;
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
            GameManager gameManager = GameManager.Instance;

            // Check if the position that got clicked has a bingoCell.
            for (int i = 0; i < totalBingoCards; i++) {
                // Check every card if that position overlaps with a cell.
                bingoCell = gameManager.BingoCards[i].GetCell(GetMouseWorldPosition());

                if (bingoCell != null) {
                    // If it does, check if the cell can get marked.
                    if (bingoCell.MarkCell(gameManager.CalledNumbers)) {
                        // If it does, check the card if a new bingo is found.
                        BingoCard bingoCard = bingoCell.BingoCard;
                        bingosFound = CheckForBingo(bingoCard.Cells, bingoCell.X, bingoCell.Y, bingoCard.Cells.GetLength(0), bingoCard.Cells.GetLength(1));

                        PlaySound(gameManager.BingosLeft, bingosFound);
                    }
                    break;
                }
            }
            gameManager.SubtractFoundBingos(bingosFound);
        }
    }

    /// <summary>
    ///     Play appropriate sound effect. This also prevents overlapping sounds.
    /// </summary>
    /// <param name="bingosLeft">Bingos left to be found.</param>
    /// <param name="bingosFound">Bingos found.</param>
    private void PlaySound(int bingosLeft, int bingosFound) {
        AudioSystem audioSystem = AudioSystem.Instance;
        // Dont overlap sounds.
        if (bingosFound > 0) {
            // If game-over.
            if (bingosLeft - bingosFound <= 0) {
                audioSystem.PlaySound(gameOver, 0.5f);
            }
            else {
                audioSystem.PlaySound(foundBingo, 0.3f);
            }
        }
        else {
            audioSystem.PlaySound(markedCell, 0.1f);
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

    #region bingo-checking
    /// <summary>
    ///     Check the grid, based on the BingoCell grid coordinates, if a new bingo was found.
    /// </summary>
    /// <param name="cells">Grid of BingoCell's.</param>
    /// <param name="x">X-coordinate in grid.</param>
    /// <param name="y">Y-coordinate in grid.</param>
    /// <param name="width">Width of grid.</param>
    /// <param name="height">Height of grid.</param>
    /// <returns>Number of bingos found.</returns>
    private int CheckForBingo(BingoCell[,] cells, int x, int y, int width, int height) {
        int bingosFound = 0;

        if (CheckRow(cells, y, width)) bingosFound++;
        if (CheckColumn(cells, x, height)) bingosFound++;
        
        // If part of up-diagonal (left-bottom to right-top).
        if (x == y) {
            if (CheckDiagonalUp(cells, width, height)) bingosFound++;
        }
        // If part of down-diagonal (left-top to right-bottom).
        else if (y == -x - 1 + height) {
            if (CheckDiagonalDown(cells, width, height)) bingosFound++;
        }

        if (bingosFound == 1) Debug.Log("BINGO!");
        else if (bingosFound == 2) Debug.Log("DOUBLE BINGO!");
        else if (bingosFound == 3) Debug.Log("TRIPLE BINGO!!!");

        return bingosFound;
    }

    /// <summary>
    ///     Checks a single row based on its starting column value.
    /// </summary>
    /// <param name="col">The column with the Marked cell.</param>
    /// <returns>Whether there is a row bingo.</returns>
    private bool CheckRow(BingoCell[,] cells, int col, int width) {
        for (int x = 0; x < width; x++) {
            if (!cells[x, col].Marked) return false;
        }

        return true;
    }

    /// <summary>
    ///     Checks a single column based on its starting row value.
    /// </summary>
    /// <param name="row">The row with the Marked cell.</param>
    private bool CheckColumn(BingoCell[,] cells, int row, int height) {
        for (int y = 0; y < height; y++) {
            if (!cells[row, y].Marked) return false;
        }

        return true;
    }

    /// <summary>
    ///     Checks the diagonal up line (from 0,0 to 4,4) until a cell is not Marked.
    /// </summary>
    private bool CheckDiagonalUp(BingoCell[,] cells, int width, int height) {
        for (int x = 0, y = 0; x < width && y < height; x++, y++) {
            if (!cells[x, y].Marked) return false;
        }

        return true;
    }

    /// <summary>
    ///     Checks the diagonal down line (from 0,4 to 4,0) until a cell is not Marked.
    /// </summary>
    private bool CheckDiagonalDown(BingoCell[,] cells, int width, int height) {
        for (int x = 0, y = height - 1; x < width && y >= 0; x++, y--) {
            if (!cells[x, y].Marked) return false;
        }

        return true;
    }

    // Extra (unused) logic for differentiating between what bingos were found.
    // For if you want to add weights to certain bingos
    // (currently unnecessary because the chance at finding any is random and thus equal).
    /*
    /// <summary>
    ///     Logic for bingo announcements in case of double or triple bingos.
    ///     Currently unnecessary
    /// </summary>
    /// <param name="rowBingo">When a row has a bingo.</param>
    /// <param name="colBingo">When a column has a bingo.</param>
    /// <param name="diagBingo">When a diagonal has a bingo.</param>
    /// <returns>Number of bingos found.</returns>
    private int AnnounceBingo(bool rowBingo, bool colBingo, bool diagBingo) {
        if (rowBingo) {
            if (colBingo) {
                if (diagBingo) {
                    Debug.Log("TRIPLE BINGO!");
                    return 3;
                }
                else {
                    Debug.Log("DOUBLE BINGO!");
                    return 2;
                }
            }
            else {
                if (diagBingo) {
                    Debug.Log("DOUBLE BINGO!");
                    return 2;
                }
                else {
                    Debug.Log("ROW BINGO!");
                    return 1;
                }
            }
        }
        else {
            if (colBingo) {
                if (diagBingo) {
                    Debug.Log("DOUBLE BINGO!");
                    return 1;
                }
                else {
                    Debug.Log("COLUMN BINGO!");
                    return 1;
                }
            }
            else {
                if (diagBingo) {
                    Debug.Log("DIAGONAL BINGO!");
                    return 1;
                }
                else
                    return 0;
            }
        }
    }*/
    #endregion
}
