using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20; // Damage amount
    public float lifetime = 5f; // Lifetime of the projectile

    private void Start()
    {
        // Destroy the projectile after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile collided with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Get the Enemy component from the collided object
            Enemy enemy = collision.GetComponent<Enemy>();

            // If the enemy component is found, apply damage
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Destroy the projectile after hitting the enemy
            Destroy(gameObject);
        }
    }
}
