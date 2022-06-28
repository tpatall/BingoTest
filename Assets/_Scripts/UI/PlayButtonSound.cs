using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButtonSound : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        AudioSystem audioSystem = AudioSystem.Instance;
        audioSystem.PlaySound(audioSystem.buttonClick);
    }
}
