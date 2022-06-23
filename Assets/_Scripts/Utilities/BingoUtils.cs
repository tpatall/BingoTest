using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Shared functions or variables.
/// </summary>
public static class BingoUtils
{
    private static readonly System.Random random = new System.Random();

    /// <summary>
    ///     Multi-dimensional array of objects representing column info.
    /// </summary>
    private static object[][] columnInfo = new object[5][] {
        new object[2] { "B", Color.red },
        new object[2] { "I", Color.yellow },
        new object[2] { "N", Color.magenta },
        new object[2] { "G", Color.green },
        new object[2] { "O", Color.blue }
    };

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
        if (number > 0 && number <= 75) {
            object[] arr = columnInfo[(number - 1) / 15];
            letter = (string)arr[0];
            return (Color)arr[1];
        }
        else 
            throw new IndexOutOfRangeException();
    }
}
