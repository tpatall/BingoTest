using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoCell
{
    /// <summary>
    ///     Determines if this cell has been marked.
    /// </summary>
    public bool marked;

    /// <summary>
    ///     Value that is shown in the cell on the board.
    /// </summary>
    public int value;

    private int gridX;
    private int gridY;
    private TextMesh textMesh;

    public BingoCell(int x, int y, int value, TextMesh textMesh, bool free) {
        this.gridX = x;
        this.gridY = y;
        this.value = value;
        this.textMesh = textMesh;

        if (free) {
            marked = true;
            textMesh.color = Color.green;
        } else {
            marked = false;
        }
    }

    /// <summary>
    ///     Check if this cell contains an announced number.
    /// </summary>
    /// <param name="value"></param>
    public void MarkCell(IList<int> values) {
        // If this cell is already completed, return.
        if (marked) {
            Debug.Log("This number is already marked!");
            return;
        }

        if (values.Contains(value)) {
            Debug.Log(value + " is correct!");
            marked = true;

            values.Remove(value);
            textMesh.color = Color.green;
        } else {
            Debug.Log("This number has not been called yet!");
        }
    }
}
