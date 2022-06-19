using UnityEngine;

/// <summary>
///     Wait one frame update before switch to give a scene time to load.
/// </summary>
public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    void Update()
    {
        if (isFirstUpdate) {
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
