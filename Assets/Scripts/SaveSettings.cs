using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSettings : MonoBehaviour
{
    /// <summary>
    ///     Reference to bingo game information.
    /// </summary>
    [SerializeField]
    private BingoGame bingoGame;

    /// <summary>
    ///     Input field where the player can set an amount of cards.
    /// </summary>
    [SerializeField]
    private Slider cardAmount;

    /// <summary>
    ///     Input field where the player can set the amount of bingos.
    /// </summary>
    [SerializeField]
    private Slider bingoAmount;

    /// <summary>
    ///     Send the values from the sliders to the bingo game information script.
    /// </summary>
    public void SendInput() {
        bingoGame.SetUp((int)cardAmount.value, (int)bingoAmount.value);
    }
}
