using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     This class maintains the object pool of balls (the called bingo numbers).
/// </summary>
public class BallObjectPooler : MonoBehaviour
{
    /// <summary>
    ///     Size of the shown pool of balls.
    /// </summary>
    [Tooltip("How much balls can maximally be shown in the pool.")]
    public int poolSize;

    /// <summary>
    ///     Prefab of a ball.
    /// </summary>
    [Tooltip("The ball prefab which will be instantiated.")]
    public GameObject ballPrefab;

    /// <summary>
    ///     Array for keeping track of (previously) pooled balls.
    /// </summary>
    private GameObject[] poolPosition;

    /// <summary>
    ///     Singleton instance of the pooler.
    /// </summary>
    public static BallObjectPooler Instance;

    /// <summary>
    ///     Queue that manages the enabling/disabling of objects in the pool.
    /// </summary>
    public Queue<GameObject> Objects { get; private set; }

    /// <summary>
    ///     Retrieve an object from the pool. If it currently does not have any, then add more to the pool.
    /// </summary>
    /// <returns>A GameObject.</returns>
    public GameObject Get() {
        if (Objects.Count == 0)
            AddObject();
        GameObject gameObject = Objects.Dequeue();
        MoveBalls(gameObject);
        return gameObject;
    }

    /// <summary>
    ///     Put an object back into the pool.
    /// </summary>
    /// <param name="objectToReturn">A ball GameObject.</param>
    public void ReturnToPool(GameObject objectToReturn) {
        objectToReturn.SetActive(false);
        Objects.Enqueue(objectToReturn);
    }

    /// <summary>
    ///     Adds a new object to the pool.
    /// </summary>
    public void AddObject() {
        var newObject = Instantiate(ballPrefab);
        newObject.transform.parent = gameObject.transform;
        newObject.SetActive(false);
        Objects.Enqueue(newObject);

        BallPooled ballPooled = newObject.GetComponent<BallPooled>();
        ballPooled.Pool = this;
    }

    /// <summary>
    ///     Assign the instance and pool.
    /// </summary>
    private void Awake() {
        Instance = this;
        
        Objects = new Queue<GameObject>();
        poolPosition = new GameObject[poolSize];
    }

    /// <summary>
    ///     Move the balls in the pool based on their position in the pool.
    /// </summary>
    /// <param name="newBall">Newly spawned object.</param>
    private void MoveBalls(GameObject newBall) {
        GameObject movedBall = newBall;
        GameObject prevBall;
        for (int i = 0; i < poolSize; i++) {
            prevBall = poolPosition[i];

            poolPosition[i] = movedBall;
            poolPosition[i].GetComponent<BallPooled>().ChangePosition(i);

            if (prevBall == null) {
                break;
            } else if (i == poolSize - 1) {
                ReturnToPool(prevBall);
                break;
            } else {
                movedBall = prevBall;
            }
        }
    }
}