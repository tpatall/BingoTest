using UnityEngine;

/// <summary>
///     This class has the functionality for spawning a ball.
/// </summary>
public class BallSpawner : MonoBehaviour
{
    /// <summary>
    ///     The pool that contains the objects.
    /// </summary>
    BallObjectPooler ballObjectPooler;

    /// <summary>
    ///     The specific position, rotation and scale for this ball.
    /// </summary>
    public Transform ballTransform;

    /// <summary>
    ///     Spawn ball.
    /// </summary>
    public void SpawnBall()
    {
        // Don't spawn a particle-system when a particle-system is still active.
        //if (!DynamicEnvironment.DoDelay)
        //{
        //    var particleSystem = particleSystemObjectPool.Get();
        //    particleSystem.transform.position = particleTransform.transform.position;
        //    particleSystem.transform.rotation = particleTransform.transform.rotation;
        //    particleSystem.transform.localScale = particleTransform.localScale;
        //    particleSystem.gameObject.SetActive(true);
        //}
        ballObjectPooler.AddObject();
    }

    /// <summary>
    ///     Create an instance of the pooler for optimization.
    /// </summary>
    private void Start()
    {
        ballObjectPooler = BallObjectPooler.Instance;
    }
}