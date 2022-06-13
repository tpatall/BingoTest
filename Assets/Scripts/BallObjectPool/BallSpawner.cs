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
    ///     Spawn a new ball from either the pool or by creating a new one.
    /// </summary>
    public void SpawnBall(int value, Color color)
    {
        var ballObject = ballObjectPooler.Get();
        ballObject.transform.position = ballTransform.transform.position;
        ballObject.transform.localScale = ballTransform.localScale;

        // Set the value of the ball.
        BallPooled ballPooled = ballObject.gameObject.GetComponent<BallPooled>();
        ballPooled.number = value;
        ballPooled.color = color;

        ballObject.gameObject.SetActive(true);
    }

    /// <summary>
    ///     Create an instance of the pooler for optimization.
    /// </summary>
    private void Start()
    {
        ballObjectPooler = BallObjectPooler.Instance;
    }
}