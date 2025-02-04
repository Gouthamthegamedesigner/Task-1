using UnityEngine;

public class BallControlTouch : MonoBehaviour
{
    private Vector3 offset; // To calculate the touch offset
    private bool isDragging = false;

    void Update()
    {
        if (Input.touchCount > 0) // Check if there's any touch input
        {
            Touch touch = Input.GetTouch(0); // Get the first touch input

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // Convert screen to world position
            touchPosition.z = 0; // Ensure the z-coordinate remains 0 for 2D

            if (touch.phase == TouchPhase.Began) // When the touch starts
            {
                if (Vector2.Distance(transform.position, touchPosition) < 0.5f) // Check if touch is near the ball
                {
                    isDragging = true;
                    offset = transform.position - touchPosition;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging) // During dragging
            {
                transform.position = touchPosition + offset; // Move the ball along with the touch
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) // When the touch ends
            {
                isDragging = false;
            }
        }
    }
}
