using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BingoCard
{
    #region variables
    /// <summary>
    ///     Store references to BingoCell objects.
    /// </summary>
    private BingoCell[,] cells;

    /// <summary>
    ///     Width of the grid.
    /// </summary>
    private int width;

    /// <summary>
    ///     Height of the grid.
    /// </summary>
    private int height;

    /// <summary>
    ///     Size of a cell.
    /// </summary>
    private float cellSize;

    /// <summary>
    ///     Color of the cell text.
    /// </summary>
    private Color color;

    /// <summary>
    ///     Size of the font of the cell text.
    /// </summary>
    private int fontSize;

    /// <summary>
    ///     Origin position of the grid.
    /// </summary>
    private Vector3 originPosition;

    /// <summary>
    ///     Background image of a cell.
    /// </summary>
    private Sprite cellBackground;

    /// <summary>
    ///     Keep track which row has a bingo.
    /// </summary>
    private bool[] rowBingos;

    /// <summary>
    ///     Keep track which column has a bingo.
    /// </summary>
    private bool[] colBingos;

    /// <summary>
    ///     Keep track which diagonal has a bingo.
    /// </summary>
    private bool[] diagBingos;
    #endregion

    public BingoCard(int width, int height, float cellSize, Color color, int fontSize, Vector3 originPosition, Sprite cellBackground) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.color = color;
        this.fontSize = fontSize;
        this.originPosition = originPosition;
        this.cellBackground = cellBackground;

        cells = new BingoCell[width, height];

        // Create array with the length of the opposite dimension.
        rowBingos = new bool[height];
        colBingos = new bool[width];
        // There are only 2 possible diagonal bingos.
        diagBingos = new bool[2];
    }

    /// <summary>
    ///     Create and populate grid with BingoCell's.
    /// </summary>
    public void Setup(GameObject parentObject) {
        // Parent to put all bingo card objects under in hierarchy.
        Transform parent = parentObject.transform;

        // Available numbers for the bingo card.
        var BValues = Enumerable.Range(1, 15).ToList();
        var IValues = Enumerable.Range(16, 15).ToList();
        var NValues = Enumerable.Range(31, 15).ToList();
        var GValues = Enumerable.Range(46, 15).ToList();
        var OValues = Enumerable.Range(61, 15).ToList();

        int value;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (x == 0) {
                    value = BingoUtils.GetRandomItemAndRemoveIt(BValues);
                }
                else if (x == 1) {
                    value = BingoUtils.GetRandomItemAndRemoveIt(IValues);
                }
                else if (x == 2) {
                    value = BingoUtils.GetRandomItemAndRemoveIt(NValues);
                }
                else if (x == 3) {
                    value = BingoUtils.GetRandomItemAndRemoveIt(GValues);
                }
                else {
                    value = BingoUtils.GetRandomItemAndRemoveIt(OValues);
                }

                // The center cell is always free.
                bool free = false;
                if (x == 2 && y == 2) {
                    free = true;
                }

                GameObject newObject = CreateWorldCell(parent, new Vector3(x, y), value);
                TextMesh textMesh = newObject.GetComponent<TextMesh>();
                SpriteRenderer spriteRenderer = newObject.GetComponentInChildren<SpriteRenderer>();

                // Create cell with a reference to this card, in case of multiple cards.
                BingoCell bingoCell = new BingoCell(this, x, y, value, textMesh, spriteRenderer, free);
                cells[x, y] = bingoCell;
            }

            string letter;
            Color letterColor = BingoUtils.GetBingoColor((x + 1) * 15, out letter);
            CreateWorldCell(parent, new Vector3(x, height), letter, letterColor);
        }
    }

    /// <summary>
    ///     Get a BingoCell based on the world position.
    /// </summary>
    /// <param name="worldPosition">Position in the world.</param>
    /// <returns>BingoCell that is located on this position in the world.</returns>
    public BingoCell GetCell(Vector3 worldPosition) {
        int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        int y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

        if (x >= 0 && y >= 0 && x < width && y < height) {
            return cells[x, y];
        }
        else {
            return null;
        }
    }

    #region initialization
    /// <summary>
    ///     Create a BingoCell GameObject to place the text in the world space.
    /// </summary>
    /// <param name="parent">Parent gameObject in hierarchy for order.</param>
    /// <param name="gridCoordinates">World position of this cell.</param>
    /// <param name="value">Cell value.</param>
    /// <returns>The GameObject of this cell.</returns>
    private GameObject CreateWorldCell(Transform parent, Vector3 gridCoordinates, int value) {
        GameObject gameObject = new GameObject("CellTextMesh", typeof(TextMesh));
        GameObject gameObjectChild = new GameObject("CellBackground", typeof(SpriteRenderer));

        Vector3 centralizeOffset = new Vector3(cellSize, cellSize) * 0.5f;
        Vector3 localPosition = gridCoordinates * cellSize + originPosition + centralizeOffset;

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        gameObjectChild.transform.SetParent(transform, false);
        gameObjectChild.transform.localScale = new Vector3(cellSize, cellSize);

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = value.ToString();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.fontSize = fontSize;
        textMesh.color = color;

        SpriteRenderer spriteRenderer = gameObjectChild.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cellBackground;
        spriteRenderer.color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f); // Light-gray.

        return gameObject;
    }

    /// <summary>
    ///     Create a TextMesh GameObject to place the title letters in the world space.
    /// </summary>
    /// <param name="parent">Parent gameObject in hierarchy for order.</param>
    /// <param name="gridCoordinates">World position of this cell.</param>
    private void CreateWorldCell(Transform parent, Vector3 gridCoordinates, string letter, Color letterColor) {
        GameObject gameObject = new GameObject("CellTextMesh", typeof(TextMesh));

        Vector3 centralizeOffset = new Vector3(cellSize, cellSize) * 0.5f;
        Vector3 localPosition = gridCoordinates * cellSize + originPosition + centralizeOffset;

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.fontSize = fontSize;
        textMesh.text = letter.ToString();
        textMesh.color = letterColor;
    }
    #endregion

    #region bingo-checking
    /// <summary>
    ///     Loop over the available squares and check if a bingo has been Marked.
    ///     It only loops over the outer edges once and then iterates until a cell is not Marked.
    /// </summary>
    /// <returns>Number of bingos found.</returns>
    public int CheckForBingo() {
        bool rowBingo = false;
        bool colBingo = false;
        bool diagBingo = false;

        // Loop down first row.
        for (int x = 0; x < width; x++) {
            if (cells[x, 0].Marked && !colBingos[x]) {
                colBingo = CheckColumn(x);
            }

            // There can only be 1 column-bingo per loop.
            if (colBingo) break;
        }

        // Loop down first column.
        for (int y = 0; y < height; y++) {
            if (cells[0, y].Marked && !rowBingos[y]) {
                rowBingo = CheckRow(y);
            }

            // There can only be 1 row-bingo per loop.
            if (rowBingo) break;
        }

        // Loop diagonally up from origin.
        if (cells[0, 0].Marked && !diagBingos[0]) {
            diagBingo = CheckDiagonalUp();
        }
        // There cant be a second diagonal bingo.
        if (!diagBingo &&
            // Loop diagonally down from top.
            cells[0, height - 1].Marked && !diagBingos[1]) {
            diagBingo = CheckDiagonalDown();
        }

        return AnnounceBingo(rowBingo, colBingo, diagBingo);
    }

    /// <summary>
    ///     Checks a single row based on its starting column value.
    /// </summary>
    /// <param name="col">The column with the Marked cell.</param>
    /// <returns>Whether there is a row bingo.</returns>
    private bool CheckRow(int col) {
        for (int x = 0; x < width; x++) {
            if (!cells[x, col].Marked) return false;
        }

        rowBingos[col] = true;
        return true;
    }

    /// <summary>
    ///     Checks a single column based on its starting row value.
    /// </summary>
    /// <param name="row">The row with the Marked cell.</param>
    private bool CheckColumn(int row) {
        for (int y = 0; y < height; y++) {
            if (!cells[row, y].Marked) return false;
        }

        colBingos[row] = true;
        return true;
    }

    /// <summary>
    ///     Checks the diagonal up line (from 0,0 to 4,4) until a cell is not Marked.
    /// </summary>
    private bool CheckDiagonalUp() {
        for (int x = 0, y = 0; x < width && y < height; x++, y++) {
            if (!cells[x, y].Marked) return false;
        }

        diagBingos[0] = true;
        return true;
    }

    /// <summary>
    ///     Checks the diagonal down line (from 0,4 to 4,0) until a cell is not Marked.
    /// </summary>
    private bool CheckDiagonalDown() {
        for (int x = 0, y = height - 1; x < width && y >= 0; x++, y--) {
            if (!cells[x, y].Marked) return false;
        }

        diagBingos[1] = true;
        return true;
    }

    /// <summary>
    ///     Logic for bingo announcements (and possibly rewards?) in case of double or triple bingos.
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
    }
    #endregion
}
