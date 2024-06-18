using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element displaying the score
    private int score = 0;

    private void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void CheckGameEnd()
    {
        if (score >= 1000)
        {
            SceneManager.LoadScene("VictoryScene");
        }
        else
        {
            SceneManager.LoadScene("LossScene");
        }
    }
}
