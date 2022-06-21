using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button musicButton, soundButton, quitButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void OnPlay() {
        StartCoroutine(AnimateMenu(140f, 0.4f, 100f, 0.2f));
        InteractableButtons(true);
    }

    public void UpdateScoreText(int found, int total) {
        scoreText.text = found.ToString() + "/" + total.ToString();
    }

    public void OnEnd() {
        InteractableButtons(false);
        StartCoroutine(AnimateMenu(140f, 0.2f, -100f, 0.4f));
    }

    /// <summary>
    ///     Play an animation for the entire set of buttons and the respective side menu when switching.
    /// </summary>
    /// <param name="via">Point midway through the animation.</param>
    /// <param name="viaTime">Time until via is reached.</param>
    /// <param name="end">End point of animation.</param>
    /// <param name="endTime">Time between via and end.</param>
    /// <returns></returns>
    IEnumerator AnimateMenu(float via, float viaTime, float end, float endTime) {
        Move(scoreText.gameObject, via, viaTime, end, endTime);
        yield return new WaitForSeconds(0.05f);
        Move(soundButton.gameObject, via, viaTime, end, endTime);
        Move(quitButton.gameObject, via, viaTime, end, endTime);
        yield return new WaitForSeconds(0.05f);
        Move(musicButton.gameObject, via, viaTime, end, endTime);
    }

    /// <summary>
    ///     Moves an object through a point to an end location.
    /// </summary>
    /// <param name="obj">The affected object.</param>
    /// <param name="via">Point midway through the animation.</param>
    /// <param name="viaTime">Time until via is reached.</param>
    /// <param name="end">End point of animation.</param>
    /// <param name="endTime">Time between via and end.</param>
    private void Move(GameObject obj, float via, float viaTime, float end, float endTime) {
        obj.LeanMoveY(via, viaTime).setEaseInBack().setOnComplete(delegate () { MoveVia(); });

        void MoveVia() {
            obj.LeanMoveY(end, endTime).setEaseInBack();
        }
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
