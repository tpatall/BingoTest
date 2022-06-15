using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Control the max value of the 'Total Bingos'-slider by the value of this slider.
/// </summary>
[RequireComponent(typeof(Slider))]
public class SliderExtension : MonoBehaviour
{
    /// <summary>
    ///     The 'Total Bingos'-slider.
    /// </summary>
    [SerializeField]
    private Slider bingosSlider;

    /// <summary>
    ///     The slider attached to this object.
    /// </summary>
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // The max amount of bingos you can get with 1 card is 12 (5 rows, 5 columns, 2 diagonals).
        // Extend this for 2 and 3 cards.
        if (slider.value == 1) {
            bingosSlider.maxValue = 12;
        } else if (slider.value == 2) {
            bingosSlider.maxValue = 24;
        } else {
            bingosSlider.maxValue = 36;
        }
    }
}
