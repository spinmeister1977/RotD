using UnityEngine;

public class DestructibleBarrel : MonoBehaviour
{
    public int health = 50; // Health of the destructible object
    public int scoreValue = 10;

    // Method to apply damage to the object
    public void TakeDamage(int damage)
    {
        Debug.Log("Rock takes damage: " + damage);
        health -= damage;
        Debug.Log("Rock health: " + health);

        if (health <= 0)
        {
            despawnDie();
        }
    }

    // Method to handle the object's destruction
    public void despawnDie()
    {
        Debug.Log("Rock destroyed.");
        // Add score when the rock is destroyed
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
        }
        Destroy(gameObject);
    }
}
