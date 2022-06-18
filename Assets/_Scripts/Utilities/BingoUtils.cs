using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BingoUtils
{
    private static readonly System.Random random = new System.Random();

    /// <summary>
    ///     Get a random item from a list of integers and then remove it, to prevent duplicate numbers.
    /// </summary>
    /// <returns>An integer.</returns>
    public static int GetRandomItemAndRemoveIt(List<int> list) {
        int randomItem = list[random.Next(list.Count)];
        list.Remove(randomItem);
        return randomItem;
    }

    /// <summary>
    ///     Get the color and color code (of the respective BINGO-letter) from an integer.
    /// </summary>
    /// <param name="number">Given number.</param>
    /// <param name="colorCode">Respective color code belonging to number.</param>
    /// <returns>Color belonging to number.</returns>
    public static Color GetBingoColor(int number, out string letter) {
        if (number <= 15) {
            letter = "B";
            return Color.red;
        }
        else if (number <= 30) {
            letter = "I";
            return Color.yellow;
        }
        else if (number <= 45) {
            letter = "N";
            return Color.magenta;
        }
        else if (number <= 60) {
            letter = "G";
            return Color.green;
        }
        else {
            letter = "O";
            return Color.blue;
        }
    }
}
