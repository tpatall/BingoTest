using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        //int inputCards, inputBingos;
        //if (cardAmount.text.Length == 0) {
        //    inputCards = 0;
        //} else {
        //    inputCards = int.Parse(cardAmount.text);
        //}

        //if (bingoAmount.text.Length == 0) {
        //    inputBingos = 0;
        //} else {
        //    inputBingos = int.Parse(bingoAmount.text);
        //}

        //bingoGame.SetUp(inputCards, inputBingos);
        Loader.Load(Scene.GameScene);
    }
}
