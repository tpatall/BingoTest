using System.Collections;
using UnityEngine;

/// <summary>
///     Creates a looping 'infinitely moving' animation for backgrounds.
/// </summary>
public class LoopMovement : MonoBehaviour
{
    /// <summary>
    ///     Scrolling speed of the image.
    /// </summary>
    [SerializeField]
    [Tooltip("Speed at which the parallax effect moves.")]
    [Range(0.01f, 1f)]
    private float movementSpeed;

    /// <summary>
    ///     If the movement direction is left-to-right.
    /// </summary>
    [SerializeField]
    [Tooltip("If the movement direction is left-to-right, or right-to-left if false.")]
    private bool rightMovementDirection;

    /// <summary>
    ///     If the loop animation should start from a delay or instantly.
    /// </summary>
    [SerializeField]
    [Tooltip("If the animation should start when called, or instantly.")]
    private bool timedStart = false;

    /// <summary>
    ///     If the loop should start slowly.
    /// </summary>
    [SerializeField]
    [Tooltip("If the movement loop should slowly start up.")]
    private bool slowStart = false;

    /// <summary>
    ///     If the animation is currently looping or not.
    /// </summary>
    private bool isActive = false;
    
    /// <summary>
    ///     Starting variables.
    /// </summary>
    private float lengthX, startPosX;

    // Start is called before the first frame update
    void Start() {
        startPosX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;

        if (!timedStart) isActive = true;
    }

    public void StartAnimation() {
        isActive = true;

        if (slowStart) {
            StartCoroutine(SlowStartUp(movementSpeed));
            movementSpeed = 0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (isActive) {
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

    /// <summary>
    ///     Simulate a slow start-up by increasing the speed over time.
    /// </summary>
    /// <param name="maxSpeed">The maximum speed this coroutine is increasing towards.</param>
    IEnumerator SlowStartUp(float maxSpeed) {
        // Gradually increase speed.
        for (float i = 0; i < maxSpeed; i += 0.01f) {
            movementSpeed = i;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
