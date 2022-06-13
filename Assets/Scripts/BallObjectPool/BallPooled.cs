using UnityEngine;

/// <summary>
///     This class has the functionality of a ball GameObject for after it has been spawned in.
/// </summary>
[RequireComponent(typeof(TextMesh))]
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
    ///     Text object that displays the number and color.
    /// </summary>
    private TextMesh textMesh;

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
    ///     Moves the ball based on its position in the UI pool.
    /// </summary>
    /// <param name="poolPosition">Position in the UI pool.</param>
    public void ChangePosition(int poolPosition) {
        Vector3 newPos = new Vector3(startPos.x - (8 * poolPosition), startPos.y);
        gameObject.transform.position = newPos;
    }

    /// <summary>
    ///     Initialize the text mesh component.
    /// </summary>
    private void Awake() {
        textMesh = GetComponent<TextMesh>();
    }

    /// <summary>
    ///     Set the color and number.
    /// </summary>
    private void OnEnable()
    {
        startPos = transform.position;
        textMesh.color = color;
        textMesh.text = number.ToString();
    }
}