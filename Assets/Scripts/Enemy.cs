using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float knockbackDuration = 2f;
    public AudioClip hitSFX; // Reference to the hit sound effect
    public float speed = 3f; // Speed at which the enemy moves towards the player
    public int scoreValue = 10; // Amount of score to add when this enemy is killed

    private AudioSource audioSource;
    private bool isKnockedBack = false;
    private Vector3 knockbackDirection;
    private float knockbackEndTime;
    private Transform playerTransform;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Ignore collisions between enemies
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            if (Time.time > knockbackEndTime)
            {
                isKnockedBack = false;
            }
            else
            {
                transform.position += knockbackDirection * Time.deltaTime;
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
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private IEnumerator Knockback()
    {
        isKnockedBack = true;
        knockbackEndTime = Time.time + knockbackDuration;
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        knockbackDirection = -directionToPlayer * speed; // Adjust knockback direction and force

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
}
