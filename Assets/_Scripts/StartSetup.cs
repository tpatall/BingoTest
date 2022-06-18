using UnityEngine;
using UnityEngine.UI;

public class StartSetup : MonoBehaviour
{
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
        GameManager.Instance.SetUp(
            (int)cardAmount.value, 
            (int)bingoAmount.value);
    }
}
