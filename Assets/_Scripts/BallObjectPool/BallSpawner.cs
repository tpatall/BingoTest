using UnityEngine;

/// <summary>
///     This class has the functionality for spawning a ball.
/// </summary>
public class BallSpawner : MonoBehaviour
{
    /// <summary>
    ///     Sound effect to play when a ball is spawned.
    /// </summary>
    [SerializeField]
    [Tooltip("Sound effect to play when a ball is spawned.")]
    private AudioClip soundEffect;

    /// <summary>
    ///     Volume of the sound effect.
    /// </summary>
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float soundEffectVolume = 0.5f;

    /// <summary>
    ///     The specific position, rotation and scale to spawn this ball at.
    /// </summary>
    [SerializeField]
    private Transform ballTransform;

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
    public void SpawnBall(int value)
    {
        var ballObject = ballObjectPooler.Get();
        ballObject.transform.position = ballTransform.transform.position;
        ballObject.transform.localScale = ballTransform.localScale;

        // Set the value of the ball.
        Ball ballPooled = ballObject.gameObject.GetComponent<Ball>();
        ballPooled.Setup(value);

        AudioSystem.Instance.PlaySound(soundEffect, soundEffectVolume);

        ballObject.gameObject.SetActive(true);
    }

    public void DestroyPool() => ballObjectPooler.DestroyPool();
}