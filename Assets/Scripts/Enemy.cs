using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float knockbackDuration = 2f;
    public AudioClip hitSFX; // Reference to the hit sound effect
    public float speed = 3f; // Speed at which the enemy moves towards the player
    public int scoreValue = 10; // Amount of score to add when this enemy is killed
    public int damage = 10; // Damage dealt to the player

    private AudioSource audioSource;
    private bool isKnockedBack = false;
    private Vector2 knockbackDirection;
    private float knockbackEndTime;
    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from enemy.");
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player object not found. Make sure your player has the tag 'Player'.");
        }
        else
        {
            Debug.Log("Player found.");
        }

        // Ignore collisions between enemies
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);

        // Set Rigidbody2D constraints
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            if (Time.time > knockbackEndTime)
            {
                isKnockedBack = false;
                rb.velocity = Vector2.zero; // Stop movement after knockback
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy takes damage: " + damage);
        health -= damage;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Die();
        }

        // Play hit sound effect
        if (hitSFX != null)
        {
            audioSource.PlayOneShot(hitSFX);
        }

        // Apply knockback effect
        StartCoroutine(Knockback());
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null && !isKnockedBack)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * speed;
            Debug.Log("Moving towards player with velocity: " + rb.velocity);
        }
        else
        {
            Debug.Log("Player transform is null or enemy is knocked back.");
        }
    }

    private IEnumerator Knockback()
    {
        isKnockedBack = true;
        knockbackEndTime = Time.time + knockbackDuration;
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        knockbackDirection = -directionToPlayer * speed;

        rb.velocity = knockbackDirection; // Apply knockback force
        Debug.Log("Applying knockback with velocity: " + rb.velocity);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockedBack = false;
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        // Add score when the enemy dies
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Apply knockback when colliding with the player
            StartCoroutine(Knockback());
        }
    }
}
