using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float knockbackDuration = 2f;
    public AudioClip hitSFX; // Reference to the hit sound effect
    public float speed = 3f; // Speed at which the enemy moves towards the player

    private AudioSource audioSource;
    private bool isKnockedBack = false;
    private Vector3 knockbackDirection;
    private float knockbackEndTime;
    private Transform playerTransform;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Ensure AudioSource is present
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the enemy.");
        }
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
        health -= damage;
        Debug.Log("Enemy took damage: " + damage);

        if (health <= 0)
        {
            Die();
        }

        // Play hit sound effect
        if (hitSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSFX);
            Debug.Log("Playing hit sound effect.");
        }
        else
        {
            Debug.LogWarning("Hit sound effect or AudioSource is not set.");
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
        // Implement enemy death logic
        Destroy(gameObject);
    }
}
