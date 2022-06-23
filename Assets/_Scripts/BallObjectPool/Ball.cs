using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

/// <summary>
///     This class has the functionality of a ball GameObject for after it has been spawned in.
/// </summary>
public class Ball : MonoBehaviour, IBall
{
    /// <summary>
    ///     Bingo ball sprites to choose from.
    /// </summary>
    [SerializeField]
    private List<Sprite> bingoSprites = new List<Sprite>(5);

    /// <summary>
    ///     Where the respective bingo ball will be displayed.
    /// </summary>
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    /// <summary>
    ///     Number on the ball.
    /// </summary>
    private int number;

    /// <summary>
    ///     Starting position to reference when moving.
    /// </summary>
    private Vector3 startPos;

    /// <summary>
    ///     The pool that manages these objects.
    /// </summary>
    private BallObjectPooler pool;

    /// <summary>
    ///     Upon instantiation, if a pool has not been created, create a new pool.
    /// </summary>
    public BallObjectPooler Pool
    {
        get { return pool; }
        set
        {
            if (pool == null)
                pool = value;
            else
                throw new System.Exception("This should only get set once.");
        }
    }

    /// <summary>
    ///     Setup the ball.
    /// </summary>
    /// <param name="number">Number to be shown on the ball.</param>
    public void Setup(int number) {
        this.number = number;

        startPos = transform.position;
        spriteRenderer.sprite = GetSprite();
    }

    /// <summary>
    ///     Set the color and number.
    /// </summary>
    private void OnEnable() {
        GetComponent<TextMeshPro>().text = number.ToString();
    }

    /// <summary>
    ///     These balls will have to roll farther to get to their intended position.
    /// </summary>
    /// <param name="reversePoolPosition">Reverse position in the UI pool for moving distances.</param>
    public void Spawn(int reversePoolPosition) {
        gameObject.LeanMoveY(28f, 0.5f).setOnComplete(delegate () { Roll(); });

        void Roll() {
            float centerOffset = 90f * reversePoolPosition;
            float randomRotation = Random.Range(
                centerOffset - 45f,
                centerOffset + 45f);

            gameObject.LeanMoveX(startPos.x - (10 * reversePoolPosition),
                0.5f * reversePoolPosition);
            gameObject.LeanRotateAround(Vector3.forward,
                randomRotation,
                0.5f * reversePoolPosition);
        }
    }

    /// <summary>
    ///     Moves the ball based on its position in the UI pool.
    /// </summary>
    /// <param name="reversePoolPosition">Reverse position in the UI pool for moving distances.</param>
    /// <param name="isAdded">True when this ball is newly added to the pool.</param>
    public void ChangePosition(int reversePoolPosition, bool isAdded) {
        if (isAdded) {
            gameObject.LeanMoveY(28f, 0.5f);
        } else {
            gameObject.LeanMoveX(startPos.x - (10 * reversePoolPosition), 0.5f);
            gameObject.LeanRotateAround(Vector3.forward, Random.Range(80f, 100f), 0.5f);
        }
    }

    /// <summary>
    ///     Moves ball out-of-view and returns it to the pool.
    /// </summary>
    /// <param name="reversePoolPosition">Reverse position in the UI pool for moving distances.</param>
    public void RemoveBall(int reversePoolPosition) {
        gameObject.LeanMoveX(startPos.x - (10 * reversePoolPosition), 0.5f).setOnComplete(delegate () { Return(); });
        gameObject.LeanRotateAround(Vector3.forward, Random.Range(80f, 100f), 0.5f);

        void Return() {
            pool.ReturnToPool(gameObject);
        }
    }

    /// <summary>
    ///     Get the bingo ball sprite from the number.
    /// </summary>
    /// <returns>Bingoball sprite belonging to number.</returns>
    private Sprite GetSprite() {
        if (number > 0 && number <= 75) {
            return bingoSprites[(number - 1)/ 15];
        }
        else {
            throw new System.IndexOutOfRangeException("The number has to be above 0 and below 76.");
        }
    }
}