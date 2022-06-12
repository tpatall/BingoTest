using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BingoGame : MonoBehaviour
{
    /// <summary>
    ///     Amount of bingos left to be called.
    /// </summary>
    public int BingosLeft { get; private set; }

    /// <summary>
    ///     List of all numbers that have not been called yet.
    /// </summary>
    public List<int> AvailableNumbers { get; private set; }

    private static readonly System.Random random = new System.Random(3);

    /// <summary>
    ///     Initialize the list of called numbers.
    /// </summary>
    void Start() {
        BingosLeft = 5;
        AvailableNumbers = Enumerable.Range(1, 76).ToList();
    }

    /// <summary>
    ///     Get a random item from a list and then remove it, to prevent duplicate numbers.
    /// </summary>
    /// <param name="items"></param>
    /// <returns>The value to be displayed in the TextMesh.</returns>
    public int GetRandomItemAndRemoveIt() {
        int randomItem = AvailableNumbers[random.Next(AvailableNumbers.Count)];
        AvailableNumbers.Remove(randomItem);
        return randomItem;
    }
}
