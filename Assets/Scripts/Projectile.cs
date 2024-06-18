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
        // Check if the object has an Enemy component
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Projectile hit enemy. Enemy health: " + enemy.health);
            // Destroy the projectile after hitting the enemy
            Destroy(gameObject);
            return;
        }

    }
}
