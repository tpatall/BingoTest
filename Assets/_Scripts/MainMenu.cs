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
    [SerializeField] private Button[] buttons = new Button[4]; 

    public void Play() {
        Loader.Load(Scene.GameScene);
    }

    public void Options() {
        StartCoroutine(AnimateSideMenu(true, 40f, 0.2f, -1200f, 0.6f, optionsPanel.gameObject, 0f));
    }

    public void Credits() {
        StartCoroutine(AnimateSideMenu(true, -40f, 0.2f, 1200f, 0.6f, creditsPanel.gameObject, 0f));
    }

    public void Quit() {
        Application.Quit();
    }

    /// <summary>
    ///     Play an animation for the entire set of buttons and the respective side menu when switching.
    /// </summary>
    /// <param name="startFromMain">If this animation is started from the main menu. This determines switch direction.</param>
    /// <param name="via">Point midway through the animation.</param>
    /// <param name="viaTime">Time until via is reached.</param>
    /// <param name="end">End point of animation.</param>
    /// <param name="endTime">Time between via and end.</param>
    /// <param name="sideMenu">Side menu object.</param>
    /// <param name="objEnd">End point of animation for objects.</param>
    /// <returns></returns>
    IEnumerator AnimateSideMenu(bool startFromMain, float via, float viaTime, float end, float endTime, GameObject sideMenu, float objEnd) {
        if (!startFromMain) {
            Move(sideMenu, -via, endTime, objEnd, viaTime);
            yield return new WaitForSeconds(0.2f);
        } else {
            InteractableButtons(false);
        }

        Move(buttons[0].gameObject, via, viaTime, end, endTime);
        yield return new WaitForSeconds(0.05f);
        Move(buttons[1].gameObject, via, viaTime, end, endTime);
        yield return new WaitForSeconds(0.05f);
        Move(buttons[2].gameObject, via, viaTime, end, endTime);
        yield return new WaitForSeconds(0.05f);
        Move(buttons[3].gameObject, via, viaTime, end, endTime);

        if (startFromMain) {
            yield return new WaitForSeconds(0.2f);
            Move(sideMenu, -via, endTime, objEnd, viaTime);
        } else {
            yield return new WaitForSeconds(viaTime + endTime);
            InteractableButtons(true);
        }
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
        obj.LeanMoveLocalX(via, viaTime).setEaseInBack().setOnComplete(delegate () { MoveVia(); });

        void MoveVia() {
            obj.LeanMoveLocalX(end, endTime).setEaseInBack();
        }
    }

    /// <summary>
    ///     Called when returned to menu from a sidescreen.
    /// </summary>
    /// <param name="fromOptions">If returns from "Options" or from "Credits".</param>
    public void BackToMenu(bool fromOptions) {
        if (fromOptions) {
            StartCoroutine(AnimateSideMenu(false, 40f, 0.6f, 0f, 0.2f, optionsPanel.gameObject, 1920f));
            
        } else {
            StartCoroutine(AnimateSideMenu(false, -40f, 0.6f, 0f, 0.2f, creditsPanel.gameObject, -1920f));
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
