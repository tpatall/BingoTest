using System;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    /// <summary>
    ///     Reference to the Text field where the stopwatch is shown.
    /// </summary>
    [SerializeField]
    private Text currentTimeText;

    /// <summary>
    ///     Whether the stopwatch is active.
    /// </summary>
    private bool stopwatchActive = false;

    /// <summary>
    ///     Public property to see the time.
    /// </summary>
    public float CurrentTime { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive) {
            CurrentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(CurrentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    /// <summary>
    ///     Resume the stopwatch.
    /// </summary>
    public void StartStopwatch() {
        stopwatchActive = true;
    }

    /// <summary>
    ///     Pause the stopwatch.
    /// </summary>
    public void StopStopwatch() {
        stopwatchActive = false;
    }
}
