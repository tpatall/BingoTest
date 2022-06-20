using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Handle the various buttons on the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel, creditsPanel;
    [SerializeField] private Button[] buttons; 

    [SerializeField] private Animator animator;

    public void Play() {
        Loader.Load(Scene.GameScene);
    }

    public void Options() {
        InteractableButtons(false);

        animator.SetBool("options", true);
    }

    public void Credits() {
        InteractableButtons(false);

        animator.SetBool("credits", true);
    }

    public void Quit() {
        Application.Quit();
    }

    /// <summary>
    ///     Called when returned to menu from a sidescreen.
    /// </summary>
    /// <param name="fromOptions">If returns from "Options" or from "Credits".</param>
    public void BackToMenu(bool fromOptions) {
        if (fromOptions) {
            animator.SetBool("options", false);
        } else {
            animator.SetBool("credits", false);
        }

        StartCoroutine(WaitForDefaultState());
        // Get state info to make sure the button cant be clicked mid-animation.
        IEnumerator WaitForDefaultState() {
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("default") == true);
            InteractableButtons(true);
        }
    }

    /// <summary>
    ///     Change button interactability.
    /// </summary>
    /// <param name="interactable">Whether the buttons should be interactable or not.</param>
    private void InteractableButtons(bool interactable) {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = interactable;
        }
    }
}
