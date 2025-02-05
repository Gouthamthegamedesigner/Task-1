using UnityEngine;

public class BallControlTouch : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed at which the ball moves towards the touch position

    private Vector3 targetPosition; // Position to move towards
    private bool isTouching = false;

    void Update()
    {
        if (Input.touchCount > 0) // Check if there's any touch input
        {
            Touch touch = Input.GetTouch(0); // Get the first touch input

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // Convert screen to world position
            touchPosition.z = 0; // Ensure the z-coordinate remains 0 for 2D

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) // On touch or drag
            {
                targetPosition = touchPosition; // Update target position
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) // When touch ends
            {
                isTouching = false;
            }
        }

        // Smoothly move the ball to the target position if there's an active touch
        if (isTouching)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
