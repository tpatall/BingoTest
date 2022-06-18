using UnityEngine;

/// <summary>
///     This class has the functionality for spawning a ball.
/// </summary>
public class BallSpawner : MonoBehaviour
{
    /// <summary>
    ///     Sound effect to play when a ball is spawned.
    /// </summary>
    [Tooltip("Sound effect to play when a ball is spawned.")]
    public AudioClip soundEffect;

    [Range(0.0f, 1.0f)]
    public float soundEffectVolume = 0.5f;

    /// <summary>
    ///     The specific position, rotation and scale for this ball.
    /// </summary>
    public Transform ballTransform;

    /// <summary>
    ///     The pool that contains the objects.
    /// </summary>
    BallObjectPooler ballObjectPooler;

    /// <summary>
    ///     Create an instance of the pooler for optimization.
    /// </summary>
    private void Start() {
        ballObjectPooler = BallObjectPooler.Instance;
    }

    /// <summary>
    ///     Spawn a new ball from either the pool or by creating a new one.
    /// </summary>
    public void SpawnBall(int value, Color color)
    {
        var ballObject = ballObjectPooler.Get();
        ballObject.transform.position = ballTransform.transform.position;
        ballObject.transform.localScale = ballTransform.localScale;

        // Set the value of the ball.
        BallPooled ballPooled = ballObject.gameObject.GetComponent<BallPooled>();
        ballPooled.Setup(value);

        AudioSystem.Instance.PlaySound(soundEffect, soundEffectVolume);

        ballObject.gameObject.SetActive(true);
    }
}