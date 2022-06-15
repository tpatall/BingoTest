using UnityEngine;
using TMPro;
using System.Collections.Generic;

/// <summary>
///     This class has the functionality of a ball GameObject for after it has been spawned in.
/// </summary>
public class BallPooled : MonoBehaviour, IBallPooled
{
    /// <summary>
    ///     Number on the ball.
    /// </summary>
    public int number;

    /// <summary>
    ///     Color of the ball.
    /// </summary>
    public Color color;

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
    ///     Text object that displays the number and color.
    /// </summary>
    private TextMeshPro textMeshPro;

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
    ///     Initialize the text mesh component.
    /// </summary>
    private void Awake() {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    /// <summary>
    ///     Set the color and number.
    /// </summary>
    private void OnEnable() {
        startPos = transform.position;

        spriteRenderer.sprite = GetSprite();
        textMeshPro.text = number.ToString();
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
        if (number <= 15) {
            return bingoSprites[0];
        }
        else if (number <= 30) {
            return bingoSprites[1];
        }
        else if (number <= 45) {
            return bingoSprites[2];
        }
        else if (number <= 60) {
            return bingoSprites[3];
        }
        else
            return bingoSprites[4];
    }
}