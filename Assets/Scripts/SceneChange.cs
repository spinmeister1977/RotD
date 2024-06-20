using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string targetScene; // Ensure the variable name is properly cased to avoid conflicts

    public void ChangeScene()
    {
        // Reset the score before changing the scene
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetScore();
        }

        // When ChangeScene is run, the scene specified in targetScene will be loaded
        SceneManager.LoadScene(targetScene);
        Debug.Log("Scene change initiated.");
    }
}
