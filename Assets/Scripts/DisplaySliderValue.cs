using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySliderValue : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI text;

    /// <summary>
    ///     Updates slider text to current slider value.
    /// </summary>
    public void UpdateSliderText() {
        text.text = slider.value.ToString();
    }
}
