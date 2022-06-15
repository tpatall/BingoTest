using TMPro;
using UnityEngine;

public class DisplayExplanation : MonoBehaviour
{
    /// <summary>
    ///     The text that will be displayed when this slider is interacted with.
    /// </summary>
    [SerializeField]
    [TextArea]
    private string explanation;

    /// <summary>
    ///     Explanation text field.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI text;

    /// <summary>
    ///     Set new explanation text.
    /// </summary>
    public void SetExplanationText() {
        text.text = explanation;
    }
}
