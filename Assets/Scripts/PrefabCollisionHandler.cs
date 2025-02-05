using UnityEngine;

public class PrefabCollisionHandler : MonoBehaviour
{
    [SerializeField] private float bounceForce = 5f; // Force to apply for bouncing back

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply bounce force
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calculate bounce direction (away from the player)
                Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
