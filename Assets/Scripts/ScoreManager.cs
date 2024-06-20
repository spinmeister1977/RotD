using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance
    private int score = 0;
    private int finalScore = 0; // To store the score to display on the end screen

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
        PlayerPrefs.SetInt("PlayerScore", score);
    }

    public int GetScore()
    {
        return score;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }

    public void CheckGameEnd()
    {
        finalScore = score; // Save the score to display on the end screen
        PlayerPrefs.SetInt("FinalPlayerScore", finalScore);
        PlayerPrefs.Save(); // Ensure the score is saved

        if (score >= 1000)
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
            SceneManager.LoadScene("Defeat");
        }
    }

    public void ResetScore()
    {
        score = 0;
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.Save();
    }

    // Call this method to start a new game and reset the score
    public void StartNewGame()
    {
        ResetScore();
        SceneManager.LoadScene("MainLevel"); // Load your main game scene here
    }

    // Call this method to go to the main menu and reset the score
    public void GoToMainMenu()
    {
        ResetScore();
        SceneManager.LoadScene("MainMenu"); // Load your main menu scene here
    }
}
