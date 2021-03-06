using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button musicButton, soundButton, quitButton;
    [SerializeField] private Slider musicSlider, soundSlider;
    [SerializeField] private TextMeshProUGUI scoreText, stopwatch;

    private bool musicSliderVisible = false;
    private bool soundSliderVisible = false;

    private void Start() {
        AudioSystem audioSystem = AudioSystem.Instance;

        musicSlider.value = audioSystem.MusicSource.volume * 100f;
        soundSlider.value = audioSystem.SoundSource.volume * 100f;

        musicSlider.onValueChanged.AddListener(value =>
        audioSystem.MusicSource.volume = value / 100f);

        soundSlider.onValueChanged.AddListener(value =>
        audioSystem.SoundSource.volume = value / 100f);

        // Position the button off-screen.
        musicButton.gameObject.transform.position = new Vector2(musicButton.gameObject.transform.position.x, -100f);
        soundButton.gameObject.transform.position = new Vector2(soundButton.gameObject.transform.position.x, -100f);
        quitButton.gameObject.transform.position = new Vector2(quitButton.gameObject.transform.position.x, -100f);
        scoreText.gameObject.transform.position = new Vector2(scoreText.gameObject.transform.position.x, -100f);
    }

    public void OnPlay() {
        StartCoroutine(AnimateMenu(musicSlider.gameObject.transform.position.y, 0.6f));
        InteractableButtons(true);
    }

    public void UpdateScoreText(int found, int total) {
        scoreText.text = found.ToString() + "/" + total.ToString();
    }

    public void HandleMusicSlider() {
        musicButton.interactable = false;
        soundButton.interactable = false;
        
        if (!musicSliderVisible) {
            if (soundSliderVisible) {
                HandleSoundSlider();
            }

            musicButton.gameObject.LeanMoveLocalX(-830f, 0.3f);
            musicSlider.gameObject.LeanScale(new Vector3(2f, 2f, 1f), 0.3f);
            musicSliderVisible = true;

        } else {
            musicSlider.gameObject.LeanScale(new Vector3(0f, 0f, 1f), 0.3f);
            musicButton.gameObject.LeanMoveLocalX(-390f, 0.3f);
            musicSliderVisible = false;
        }
        
        musicButton.interactable = true;
        soundButton.interactable = true;
    }

    public void HandleSoundSlider() {
        musicButton.interactable = false;
        soundButton.interactable = false;


        if (!soundSliderVisible) {
            if (musicSliderVisible) {
                HandleMusicSlider();
            }

            musicButton.gameObject.LeanMoveLocalX(-830f, 0.3f);
            soundButton.gameObject.LeanMoveLocalX(-670f, 0.3f);
            soundSlider.gameObject.LeanScale(new Vector3(2f, 2f, 1f), 0.3f);
            soundSliderVisible = true;

        }
        else {
            soundSlider.gameObject.LeanScale(new Vector3(0f, 0f, 1f), 0.3f);
            soundButton.gameObject.LeanMoveLocalX(-230f, 0.3f);
            musicButton.gameObject.LeanMoveLocalX(-390f, 0.3f);
            soundSliderVisible = false;
        }

        musicButton.interactable = true;
        soundButton.interactable = true;
    }

    public void OnTearDown() {
        InteractableButtons(false);
        if (musicSliderVisible) HandleMusicSlider();
        if (soundSliderVisible) HandleSoundSlider();

        StartCoroutine(AnimateMenu(-100f, 0.6f));

        stopwatch.gameObject.LeanMoveLocalY(595f, 0.3f).setEaseInBack();
    }

    /// <summary>
    ///     Play an animation for the entire set of buttons and the respective side menu when switching.
    /// </summary>
    /// <param name="via">Point midway through the animation.</param>
    /// <param name="viaTime">Time until via is reached.</param>
    IEnumerator AnimateMenu(float via, float viaTime) {
        scoreText.gameObject.LeanMoveY(via, viaTime).setEaseInBack();
        yield return new WaitForSeconds(0.05f);
        soundButton.gameObject.LeanMoveY(via, viaTime).setEaseInBack();
        quitButton.gameObject.LeanMoveY(via, viaTime).setEaseInBack();
        yield return new WaitForSeconds(0.05f);
        musicButton.gameObject.LeanMoveY(via, viaTime).setEaseInBack();
    }

    /// <summary>
    ///     Change button interactability.
    /// </summary>
    /// <param name="interactable">Whether the buttons should be interactable or not.</param>
    private void InteractableButtons(bool interactable) {
        musicButton.interactable = interactable;
        soundButton.interactable = interactable;
        quitButton.interactable = interactable;
    }
}
