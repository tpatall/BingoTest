using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
    }

    /// <summary>
    ///     Set the color and number.
    /// </summary>
    private void OnEnable() {
        startPos = transform.position;

        spriteRenderer.sprite = GetSprite();
        GetComponent<TextMeshPro>().text = number.ToString();
    }

    /// <summary>
    ///     Moves the ball based on its position in the UI pool.
    /// </summary>
    /// <param name="poolPosition">Position in the UI pool.</param>
    public void ChangePosition(int poolPosition) {
        Vector3 newPos = new Vector3(startPos.x - (10 * poolPosition), startPos.y);
        gameObject.transform.position = newPos;
    }

    /// <summary>
    ///     Get the bingo ball sprite from the number.
    /// </summary>
    /// <returns>Bingoball sprite belonging to number.</returns>
    private Sprite GetSprite() {
        if (number > 0 && number <= 75) {
            return bingoSprites[number / 15];
        }
        else {
            throw new System.IndexOutOfRangeException("The number exceeds the maximum allowed number.");
        }
    }
}