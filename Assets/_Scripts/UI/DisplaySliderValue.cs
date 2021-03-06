using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySliderValue : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textField;

    private void Start() {
        textField.text = slider.value.ToString();
    }

    /// <summary>
    ///     Updates slider text to current slider value.
    /// </summary>
    public void UpdateSliderText() {
        textField.text = slider.value.ToString();
    }
}
