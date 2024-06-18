using UnityEngine;

public class Rock : MonoBehaviour
{
    public int health = 50; // Health of the destructible object
    public int scoreValue = 10;


    // Method to apply damage to the object
    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy takes damage: " + damage);
        health -= damage;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Die();
        }

        // Method to handle the object's destruction
        if (health <= 0)
        {
            Die();
        }

       
    } 
    public void Die()
        {
            Debug.Log("Enemy died.");
            // Add score when the enemy dies
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(scoreValue);
            }
            Destroy(gameObject);
        }





}

