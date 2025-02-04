using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Prefab to spawn
    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float spawnOffset = 2f; // Distance outside the screen to spawn
    [SerializeField] private Vector2 sizeRange = new Vector2(0.5f, 2f); // Min and max size
    [SerializeField] private float minSpeed = 2f; // Minimum movement speed
    [SerializeField] private float maxSpeed = 5f; // Maximum movement speed
    [SerializeField] private float spawnInterval = 2f; // Time between spawns

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // Reference to the main camera
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval); // Repeatedly spawn objects
    }

    private void SpawnObject()
    {
        // Generate a random spawn position outside the screen bounds
        Vector3 spawnPosition = GetSpawnPositionOutsideScreen();

        // Instantiate the prefab
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // Assign random size
        float randomSize = Random.Range(sizeRange.x, sizeRange.y);
        newObject.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        // Add movement script to the object
        RandomMover mover = newObject.AddComponent<RandomMover>();
        mover.AssignPlayer(player); // Assign the player for direction
        mover.SetMovement(Random.Range(minSpeed, maxSpeed)); // Set the movement speed
    }

    private Vector3 GetSpawnPositionOutsideScreen()
    {
        // Get screen bounds in world space
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        // Expand bounds by spawn offset
        float minX = screenBottomLeft.x - spawnOffset;
        float maxX = screenTopRight.x + spawnOffset;
        float minY = screenBottomLeft.y - spawnOffset;
        float maxY = screenTopRight.y + spawnOffset;

        // Randomly choose an edge (0 = left, 1 = right, 2 = top, 3 = bottom)
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Left
                return new Vector3(minX, Random.Range(minY, maxY), 0);
            case 1: // Right
                return new Vector3(maxX, Random.Range(minY, maxY), 0);
            case 2: // Top
                return new Vector3(Random.Range(minX, maxX), maxY, 0);
            case 3: // Bottom
                return new Vector3(Random.Range(minX, maxX), minY, 0);
            default:
                return Vector3.zero; // Fallback, shouldn't happen
        }
    }
}
