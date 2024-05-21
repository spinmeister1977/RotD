using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text scoreText; // Reference to the Text UI element
    private int score = 0;

    // Update the score display
    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Public method to increase the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    // Optional: Reset the score to zero
    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }
}
