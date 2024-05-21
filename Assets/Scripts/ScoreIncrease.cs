using UnityEngine;

public class ScoreAdder : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10; // The score value to add
    [SerializeField] private string playerTag = "Player"; // The tag identifying the player
    private ScoreCounter scoreCounter; // Reference to the ScoreCounter component

    private void Start()
    {
        // Find the ScoreCounter object in the scene (ensure it's present)
        scoreCounter = FindObjectOfType<ScoreCounter>();
        if (scoreCounter == null)
        {
            Debug.LogError("ScoreCounter component not found in the scene!");
        }
    }

    // Trigger detection for collision with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag(playerTag) && scoreCounter != null)
        {
            // Add the score to the player's score counter
            scoreCounter.AddScore(scoreValue);

            // Optionally, destroy this object after collecting
            Destroy(gameObject);
        }
    }
}
