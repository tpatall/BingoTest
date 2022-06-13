using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void Play() {
        Loader.Load(Scene.GameScene);
    }
}
