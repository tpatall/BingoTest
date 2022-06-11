using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BingoCard
{
    /// <summary>
    ///     Width of the grid.
    /// </summary>
    private int width;

    /// <summary>
    ///     Height of the grid.
    /// </summary>
    private int height;

    /// <summary>
    ///     Size of the cell.
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
    ///     Origin position of the cell.
    /// </summary>
    private Vector3 originPosition;

    /// <summary>
    ///     Store references to BingoCell objects.
    /// </summary>
    private BingoCell[,] cells;

    private static readonly System.Random random = new System.Random(3);

    public BingoCard(int width, int height, float cellSize, Color color, int fontSize, Vector3 originPosition) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.color = color;
        this.fontSize = fontSize;
        this.originPosition = originPosition;

        cells = new BingoCell[width, height];

        GameObject card = new GameObject("Card");

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
                    value = GetRandomItemAndRemoveIt(BValues);
                }
                else if (x == 1) {
                    value = GetRandomItemAndRemoveIt(IValues);
                }
                else if (x == 2) {
                    value = GetRandomItemAndRemoveIt(NValues);
                }
                else if (x == 3) {
                    value = GetRandomItemAndRemoveIt(GValues);
                }
                else {
                    value = GetRandomItemAndRemoveIt(OValues);
                }

                // The center cell is always free.
                bool free = false;
                if (x == 2 && y == 2) {
                    free = true;
                }

                TextMesh textMesh = CreateWorldText(card.transform, new Vector3(x, y), value);
                BingoCell bingoCell = new BingoCell(x, y, value, textMesh, free);
                cells[x,y] = bingoCell;
            }
        }
    }

    /// <summary>
    ///     Get the TextMesh from a grid cell based on the world position.
    /// </summary>
    /// <param name="worldPosition">Position in the world.</param>
    /// <returns>TextMesh from the cell that is located on this position in the world.</returns>
    //public TextMesh GetValue(Vector3 worldPosition) {
    //    int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
    //    int y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

    //    if (x >= 0 && y >= 0 && x < width && y < height) {
    //        return cells[x * width + y];
    //    } else {
    //        return null;
    //    }
    //}

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

    public void CheckForBingo() {
        // Loop diagonally up from origin.
        if (cells[0, 0].marked) {
            CheckDiagonalUp();
        }

        // Loop down first (horizontal) row.
        for (int x = 0; x < width; x++) {
            if (cells[x, 0].marked) {
                CheckColumn(x);
            }
        }

        // Loop diagonally down from top.
        if (cells[0, height - 1].marked) {
            CheckDiagonalDown();
        }

        // Loop down first (vertical) column.
        for (int y = 0; y < height; y++) {
            if (cells[0, y].marked) {
                CheckRow(y);
            }
        }
    }

    private void CheckRow(int col) {
        for (int x = 0; x < width; x++) {
            if (!cells[x, col].marked) return;
        }
        
        string text = "";
        for (int x = 0; x < width; x++) {
            text += cells[x, col].value + " ";
        }
        Debug.Log("horizontal BINGO! With the numbers: " + text);
    }

    private void CheckColumn(int row) {
        for (int y = 0; y < height; y++) {
            if (!cells[row, y].marked) return;
        }

        string text = "";
        for (int y = 0; y < height; y++) {
            text += cells[row, y].value + " ";
        }
        Debug.Log("vertical BINGO! With the numbers: " + text);
    }

    private void CheckDiagonalUp() {
        for (int x = 0, y = 0; x < width && y < height; x++, y++) {
            if (!cells[x, y].marked) return;
        }

        string text = "";
        for (int x = 0, y = 0; x < width && y < height; x++, y++) {
            text += cells[x, y].value + " ";
        }
        Debug.Log("diagonal BINGO! With the numbers: " + text);
    }

    private void CheckDiagonalDown() {
        for (int x = 0, y = height - 1; x < width && y >= 0; x++, y--) {
            if (!cells[x, y].marked) return;
        }

        string text = "";
        for (int x = 0, y = height - 1; x < width && y >= 0; x++, y--) {
            text += cells[x, y].value + " ";
        }
        Debug.Log("diagonal BINGO! With the numbers: " + text);
    }

    /// <summary>
    ///     Get a random item from a list and then remove it, to prevent duplicate numbers.
    /// </summary>
    /// <param name="items"></param>
    /// <returns>The value to be displayed in the TextMesh.</returns>
    private int GetRandomItemAndRemoveIt(IList<int> items) {
        int randomItem = items[random.Next(items.Count)];
        items.Remove(randomItem);
        return randomItem;
    }

    /// <summary>
    ///     Create a TextMesh GameObject to place the text in the world space.
    /// </summary>
    /// <param name="parent">Parent gameObject in hierarchy for order.</param>
    /// <param name="gridCoordinates">World position of this cell.</param>
    /// <param name="text">Cell text.</param>
    /// <returns></returns>
    private TextMesh CreateWorldText(Transform parent, Vector3 gridCoordinates, int value) {
        GameObject gameObject = new GameObject("CellTextMesh", typeof(TextMesh));

        Vector3 centralizeOffset = new Vector3(cellSize, cellSize) * 0.5f;
        Vector3 localPosition = gridCoordinates * cellSize + originPosition + centralizeOffset;

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = value.ToString();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        //textMesh.GetComponent<MeshRenderer>().sortingOrder = 10;

        return textMesh;
    }
}
