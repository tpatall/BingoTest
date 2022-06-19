using TMPro;
using UnityEngine;

public class DisplayExplanation : MonoBehaviour
{
    /// <summary>
    ///     The text that will be displayed when this slider is interacted with.
    /// </summary>
    [SerializeField]
    [TextArea]
    [Tooltip("Set an explanation for the related object.")]
    private string explanation;

    [SerializeField] private TextMeshProUGUI textField;

    /// <summary>
    ///     Set new explanation text.
    /// </summary>
    public void SetExplanationText() {
        textField.text = explanation;
    }
}
