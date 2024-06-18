using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn; // Reference to the prefab to be spawned
    [SerializeField] private Transform spawnAnchor; // The transform reference for the spawning object's position
    [SerializeField] private float spawnChance = 0.5f; // Chance of spawning the prefab (0 to 1)
    [SerializeField] private float spawnInterval = 8f; // Interval in seconds between each spawn check

    private void Start()
    {
        // Ensure the spawnAnchor is assigned
        if (spawnAnchor == null)
        {
            Debug.LogError("No spawn anchor object assigned!");
            return;
        }

        // Start a coroutine that handles repeated spawning checks
        StartCoroutine(SpawnAtIntervals());
    }

    private IEnumerator SpawnAtIntervals()
    {
        // Run indefinitely to spawn periodically
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Generate a random number between 0 and 1
            float randomValue = Random.value;

            // Compare the random value with the specified spawn chance
            if (randomValue <= spawnChance)
            {
                // Use the position of the anchor object for spawning
                Instantiate(prefabToSpawn, spawnAnchor.position, Quaternion.identity);
                //Debug.Log("Object spawned successfully at anchor position.");
            }
            else
            {
                //Debug.Log("Object not spawned due to low random value.");
            }
        }
    }
}
