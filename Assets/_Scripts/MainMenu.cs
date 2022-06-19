using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Handle the various buttons on the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel, creditsPanel;

    public void Play() {
        Loader.Load(Scene.GameScene);
    }

    public void Options() {

    }

    public void Credits() {

    }

    public void Quit() {
        Application.Quit();
    }
}
