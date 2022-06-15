using UnityEngine;

public class LoopMovement : MonoBehaviour
{
    /// <summary>
    ///     Scrolling speed of the image.
    /// </summary>
    [Tooltip("Speed at which the parallax effect moves.")]
    public float movementSpeed;

    /// <summary>
    ///     If the movement direction is left-to-right.
    /// </summary>
    [Tooltip("If the movement direction is left-to-right, or right-to-left if false.")]
    public bool rightMovementDirection;
    
    /// <summary>
    ///     Starting variables.
    /// </summary>
    private float lengthX, startPosX;

    // Start is called before the first frame update
    void Start() {
        startPosX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate() {
        float speed = rightMovementDirection ? movementSpeed : -movementSpeed;

        // Move the background.
        transform.position = new Vector3(transform.position.x + speed, transform.position.y);
        
        // If the background moved too far beyond the screen, reset it.
        if (transform.position.x > startPosX + lengthX) {
            transform.position = new Vector3(startPosX - lengthX, transform.position.y);
        } else if (transform.position.x < startPosX - lengthX) {
            transform.position = new Vector3(startPosX + lengthX, transform.position.y);
        }
    }
}
