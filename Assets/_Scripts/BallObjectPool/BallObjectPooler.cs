using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     This class maintains the object pool of balls (the called bingo numbers).
/// </summary>
public class BallObjectPooler : Singleton<BallObjectPooler>
{
    /// <summary>
    ///     Size of the shown pool of balls.
    /// </summary>
    [SerializeField]
    [Tooltip("How much balls can maximally be shown in the pool.")]
    private int poolSize;

    /// <summary>
    ///     Prefab of a ball.
    /// </summary>
    [SerializeField]
    [Tooltip("The ball prefab which will be instantiated.")]
    private GameObject ballPrefab;

    /// <summary>
    ///     Array for keeping track of (previously) pooled balls.
    /// </summary>
    private GameObject[] poolPosition;

    private int currentPoolCount;

    /// <summary>
    ///     Queue that manages the enabling/disabling of objects in the pool.
    /// </summary>
    public Queue<GameObject> Objects { get; private set; }

    /// <summary>
    ///     Assign the instance and pool.
    /// </summary>
    private void Start() {
        Objects = new Queue<GameObject>();
        poolPosition = new GameObject[poolSize];
    }

    /// <summary>
    ///     Retrieve an object from the pool. If it currently does not have any, then add more to the pool.
    /// </summary>
    /// <returns>A GameObject.</returns>
    public GameObject Get() {
        if (Objects.Count == 0) {
            AddObject();
            // Increment until pool is full.
            if (currentPoolCount <= poolSize) currentPoolCount++;
        }
        GameObject gameObject = Objects.Dequeue();
        MoveBalls(gameObject);
        return gameObject;
    }

    /// <summary>
    ///     Adds a new object to the pool.
    /// </summary>
    public void AddObject() {
        var newObject = Instantiate(ballPrefab);
        newObject.transform.SetParent(gameObject.transform);
        newObject.SetActive(false);
        Objects.Enqueue(newObject);

        Ball ballPooled = newObject.GetComponent<Ball>();
        ballPooled.Pool = this;
    }

    /// <summary>
    ///     Put an object back into the pool.
    /// </summary>
    /// <param name="objectToReturn">A ball GameObject.</param>
    public void ReturnToPool(GameObject objectToReturn) {
        // Reset rotation before disabling.
        objectToReturn.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        objectToReturn.SetActive(false);
        Objects.Enqueue(objectToReturn);
    }

    /// <summary>
    ///     Move the balls in the pool based on their position in the pool.
    /// </summary>
    /// <param name="newBall">Newly spawned object.</param>
    private void MoveBalls(GameObject newBall) {
        // If the pool is not yet full, put the ball on the last position.
        if (currentPoolCount <= poolSize) {
            int pos = currentPoolCount - 1;
            poolPosition[pos] = newBall;

            if (currentPoolCount == poolSize) {
                poolPosition[pos].GetComponent<Ball>().ChangePosition(poolSize - currentPoolCount, true);
            } else {
                poolPosition[pos].GetComponent<Ball>().Spawn(poolSize - currentPoolCount);
            }
            return;
        }

        // Otherwise move every ball down the array.
        for (int i = 0; i < poolSize; i++) {
            // Return bottom ball back to pool.
            if (i == 0) {
                poolPosition[i].GetComponent<Ball>().RemoveBall(poolSize - i);

                poolPosition[i] = poolPosition[i + 1];
            }
            // Set ball in next position to this position.
            else if (i < poolSize - 1) {
                poolPosition[i].GetComponent<Ball>().ChangePosition(poolSize - i, false);
                
                poolPosition[i] = poolPosition[i + 1];
            // Set new ball to last position.
            } else {
                poolPosition[i].GetComponent<Ball>().ChangePosition(poolSize - i, false);
                
                poolPosition[i] = newBall;
                poolPosition[i].GetComponent<Ball>().ChangePosition(0, true);
            }
        }
    }

    public void DestroyPool() {
        for (int i = poolPosition.Length - 1; i >= 0; i--) {
            poolPosition[i].LeanMoveX(-75f - 10 * i, 1f);
            poolPosition[i].LeanRotateZ(-180f, 1f);
        }
    }
}