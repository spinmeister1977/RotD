using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element

    private void Start()
    {
        // Optionally, find the ScoreManager instance if not assigned
        if (ScoreManager.instance == null)
        {
            Debug.LogError("ScoreManager instance not found. Ensure there is a ScoreManager in the scene.");
            return;
        }

        // Update the score display at the start
        UpdateScoreDisplay();
    }

    private void Update()
    {
        UpdateScoreDisplay();
    }

    // Update the score display
    private void UpdateScoreDisplay()
    {
        if (ScoreManager.instance != null)
        {
            int currentScore = ScoreManager.instance.GetScore();
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }
}
