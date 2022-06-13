using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BallSpawner))]
public class BingoGame : MonoBehaviour
{
    private BallSpawner ballSpawner;

    /// <summary>
    ///     Amount of bingos left to be called.
    /// </summary>
    public int BingosLeft { get; private set; }

    /// <summary>
    ///     List of all numbers that have not been called yet.
    /// </summary>
    public List<int> AvailableNumbers { get; private set; }

    /// <summary>
    ///     List of all numbers that have been called.
    /// </summary>
    public List<int> CalledNumbers { get; private set; }

    private static readonly System.Random random = new System.Random(3);

    /// <summary>
    ///     Initialize the list of called numbers.
    /// </summary>
    void Start() {
        ballSpawner = GetComponent<BallSpawner>();

        BingosLeft = 5;
        AvailableNumbers = Enumerable.Range(1, 76).ToList();
        CalledNumbers = new List<int>();
    }

    /// <summary>
    ///     Call a new number and remove it from the available numbers list. Then spawn a ball with that number.
    /// </summary>
    public void CallNewNumber() {
        int number = GetRandomItemAndRemoveIt();
        Color color = GetColor(number, out char colorCode);
        Debug.Log("Number: " + colorCode + "-" + number + ".");

        CalledNumbers.Add(number);

        ballSpawner.SpawnBall(number, color);
    }

    /// <summary>
    ///     Get a random item from a list and then remove it, to prevent duplicate numbers.
    /// </summary>
    /// <param name="items"></param>
    /// <returns>The value to be displayed in the TextMesh.</returns>
    private int GetRandomItemAndRemoveIt() {
        int randomItem = AvailableNumbers[random.Next(AvailableNumbers.Count)];
        AvailableNumbers.Remove(randomItem);
        return randomItem;
    }

    /// <summary>
    ///     Get the color and color code (respective BINGO-letter) from the number.
    /// </summary>
    /// <param name="number">Given number.</param>
    /// <param name="colorCode">Respective color code to number.</param>
    /// <returns>Color belonging to number.</returns>
    private Color GetColor(int number, out char colorCode) {
        if (number <= 15) {
            colorCode = 'B';
            return Color.red;
        }
        else if (number <= 30) {
            colorCode = 'I';
            return Color.yellow;
        }
        else if (number <= 45) {
            colorCode = 'N';
            return Color.magenta;
        }
        else if (number <= 60) {
            colorCode = 'G';
            return Color.green;
        }
        else
            colorCode = 'O';
            return Color.blue;
    }
}
