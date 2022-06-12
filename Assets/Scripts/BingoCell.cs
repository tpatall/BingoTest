using System.Collections.Generic;
using UnityEngine;

public class BingoCell
{
    /// <summary>
    ///     TextMesh used to display the contents of this cell.
    /// </summary>
    private TextMesh textMesh;

    /// <summary>
    ///     Reference to the bingo card this cell belongs to.
    /// </summary>
    public BingoCard BingoCard { get; private set; }

    /// <summary>
    ///     Value that is shown in the cell on the board.
    /// </summary>
    public int Value { get; private set; }
    
    /// <summary>
    ///     Determines if this cell has been marked.
    /// </summary>
    public bool Marked { get; private set; }

    public BingoCell(BingoCard bingoCard, int x, int y, int value, TextMesh textMesh, bool free) {
        this.textMesh = textMesh;
        
        BingoCard = bingoCard;
        Value = value;

        if (free) {
            Marked = true;
            textMesh.color = Color.green;
        } else {
            Marked = false;
        }
    }

    /// <summary>
    ///     Check if this cell contains an announced number.
    /// </summary>
    /// <param name="values">List of announced numbers.</param>
    /// <returns>If this cell got marked.</returns>
    public bool MarkCell(IList<int> values) {
        // If this cell is already completed, return.
        if (Marked) {
            Debug.Log("This number is already marked!");
            return false;
        }

        if (values.Contains(Value)) {
            Debug.Log(Value + " is correct!");
            Marked = true;

            values.Remove(Value);
            textMesh.color = Color.green;
            return true;
        } else {
            Debug.Log("This number has not been called yet!");
            return true;
        }
    }
}
