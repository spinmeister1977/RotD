using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // The maximum health the player can have
    private int currentHealth;

    public delegate void PlayerDeathHandler(); // Delegate for player death event
    public event PlayerDeathHandler OnPlayerDeath; // Event fired when the player dies

    private void Start()
    {
        // Initialize the player's health to max at the start of the game
        currentHealth = maxHealth;
        Debug.Log("Player starting health: " + currentHealth);
    }

    // Method to deal damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage + " - Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to heal the player up to the maximum health
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Player healed: " + amount + " - Current health: " + currentHealth);
    }

    // Method called when the player's health reaches zero
    private void Die()
    {
        Debug.Log("Player has died.");
        OnPlayerDeath?.Invoke(); // Invoke any subscribers to the death event
        // Optionally, handle player death (disable movement, show game over screen, etc.)
    }

    // Optional: Method to reset health back to full (could be called on respawn or level restart)
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Debug.Log("Player health reset to maximum: " + maxHealth);
    }

    // Accessor for current health
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // Accessor for maximum health
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
