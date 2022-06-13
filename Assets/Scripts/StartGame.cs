using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    /// <summary>
    ///     Reference to bingo game information.
    /// </summary>
    public BingoGame bingoGame;

    /// <summary>
    ///     Input field where the player can set an amount of cards.
    /// </summary>
    public Text cardAmount;

    /// <summary>
    ///     Input field where the player can set the amount of bingos.
    /// </summary>
    public Text bingoAmount;

    /// <summary>
    ///     Send the text from the input fields to the bingo game information script.
    /// </summary>
    public void SendInput() {
        string inputCards = cardAmount.text;
        string inputBingos = bingoAmount.text;

        bingoGame.SetUp(inputCards, inputBingos);
    }
}
