using System.Linq;
using UnityEngine;

/// <summary>
///     This class contains the information of a single bingo card with references to all its cells.
/// </summary>
public class BingoCard
{
    #region variables
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
    ///     Origin position of the grid.
    /// </summary>
    private Vector3 originPosition;

    /// <summary>
    ///     Grid array of BingoCell objects.
    /// </summary>
    public BingoCell[,] Cells { get; private set; }
    #endregion

    public BingoCard(int width, int height, float cellSize, Vector3 originPosition) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        Cells = new BingoCell[width, height];
    }

    /// <summary>
    ///     Create and populate grid with BingoCell's.
    /// </summary>
    public void Setup(Transform parentTransform, Color color, int fontSize, Sprite cellBackground) {
        // Available numbers for the bingo card.
        var BValues = Enumerable.Range(1, 15).ToList();
        var IValues = Enumerable.Range(16, 15).ToList();
        var NValues = Enumerable.Range(31, 15).ToList();
        var GValues = Enumerable.Range(46, 15).ToList();
        var OValues = Enumerable.Range(61, 15).ToList();

        int freeX = (int)Mathf.Floor(width / 2);
        int freeY = (int)Mathf.Floor(height / 2);

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
                if (x == freeX && y == freeY) {
                    free = true;
                }

                GameObject newObject = CreateWorldCell(parentTransform, new Vector3(x, y), value, fontSize, color, cellBackground);
                TextMesh textMesh = newObject.GetComponent<TextMesh>();
                SpriteRenderer spriteRenderer = newObject.GetComponentInChildren<SpriteRenderer>();

                // Create cell with a reference to this card, in case of multiple cards.
                BingoCell bingoCell = new BingoCell(this, x, y, value, textMesh, spriteRenderer);
                bingoCell.Setup(free);
                Cells[x, y] = bingoCell;
            }

            string letter;
            Color letterColor = BingoUtils.GetBingoColor((x + 1) * 15, out letter);
            CreateWorldCell(parentTransform, new Vector3(x, height), letter, fontSize, letterColor);
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
            return Cells[x, y];
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
    private GameObject CreateWorldCell(Transform parent, Vector3 gridCoordinates, int value, int fontSize, Color color, Sprite cellBackground) {
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
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.text = value.ToString();
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
    private void CreateWorldCell(Transform parent, Vector3 gridCoordinates, string letter, int fontSize, Color letterColor) {
        GameObject gameObject = new GameObject("CellTextMesh", typeof(TextMesh));

        Vector3 centralizeOffset = new Vector3(cellSize, cellSize) * 0.5f;
        Vector3 localPosition = gridCoordinates * cellSize + originPosition + centralizeOffset;

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.text = letter.ToString();
        textMesh.fontSize = fontSize;
        textMesh.color = letterColor;
    }
    #endregion
}
