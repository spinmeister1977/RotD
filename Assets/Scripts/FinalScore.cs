using UnityEngine;
using TMPro;

public class DisplayVictoryScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element

    private void Start()
    {
        int finalScore = PlayerPrefs.GetInt("PlayerScore", 0);
        scoreText.text = "Final Score: " + finalScore.ToString();
    }
}
