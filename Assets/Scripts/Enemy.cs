using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Movement speed of the enemy
    public float knockbackDuration = 2f; // Duration for which the enemy moves away after collision
    public int damage = 10; // Damage dealt to the player on collision
    public int maxHealth = 100; // Maximum health of the enemy

    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb;
    private int currentHealth; // Current health of the enemy
    private bool isKnockedBack = false; // Flag to check if the enemy is in knockback state

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes the player has the "Player" tag
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isKnockedBack)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage); // Assumes the player has a PlayerHealth script
            StartCoroutine(Knockback());
        }
    }

    private IEnumerator Knockback()
    {
        isKnockedBack = true;
        Vector2 knockbackDirection = (transform.position - player.position).normalized;
        rb.velocity = knockbackDirection * moveSpeed;

        yield return new WaitForSeconds(knockbackDuration);

        isKnockedBack = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add death logic here (e.g., play animation, destroy object)
        Destroy(gameObject);
    }
}
