using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoCell
{
    public bool marked;
    public int value;

    private int gridX;
    private int gridY;
    private TextMesh textMesh;

    private bool free;

    public BingoCell(int x, int y, int value, TextMesh textMesh, bool free) {
        this.gridX = x;
        this.gridY = y;
        this.value = value;
        this.textMesh = textMesh;
        this.free = free;

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
    public void CheckCell(IList<int> values) {
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
