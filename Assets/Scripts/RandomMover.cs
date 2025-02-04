using UnityEngine;

public class RandomMover : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player
    private Vector3 direction; // Movement direction
    private float speed; // Movement speed
    private bool hasDirectionSet = false;

    public void SetMovement(float spd)
    {
        if (player != null)
        {
            // Calculate the direction toward the player
            direction = (player.position - transform.position).normalized;
            speed = spd;
            hasDirectionSet = true;
        }
        else
        {
            Debug.LogError("Player is not assigned to RandomMover script!");
        }
    }

    private void Update()
    {
        if (!hasDirectionSet) return; // Ensure direction is set before moving

        // Move in the assigned direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Destroy if the object goes too far from the center
        if (Vector3.Distance(transform.position, Vector3.zero) > 20f)
        {
            Destroy(gameObject);
        }
    }

    // Allow assignment of the player in case it changes dynamically
    public void AssignPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }
}
