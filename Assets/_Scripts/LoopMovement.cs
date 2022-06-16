using System.Collections;
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
    ///     If the loop should start slowly.
    /// </summary>
    [Tooltip("If the movement loop should slowly start up.")]
    public bool slowStart;
    
    /// <summary>
    ///     Starting variables.
    /// </summary>
    private float lengthX, startPosX;

    // Start is called before the first frame update
    void Start() {
        startPosX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;

        if (slowStart) {
            StartCoroutine(SlowStartUp(movementSpeed));
            movementSpeed = 0f;
        }
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

    /// <summary>
    ///     Simulate a slow start-up by increasing the speed over time.
    /// </summary>
    /// <param name="maxSpeed">The maximum speed this coroutine is increasing towards.</param>
    /// <returns></returns>
    IEnumerator SlowStartUp(float maxSpeed) {
        yield return new WaitForSeconds(5f);

        // Gradually increase speed.
        for (float i = 0; i < maxSpeed; i += 0.01f) {
            movementSpeed = i;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
