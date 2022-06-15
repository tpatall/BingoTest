using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void Quit() {
        Loader.Load(Scene.MainMenu);
    }
}
