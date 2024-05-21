using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Optional: Prefab for explosion effect
    [SerializeField] private float explosionDuration = 2f; // Duration for which the explosion effect will be shown

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with this one has the tag "Projectile"
        if (other.CompareTag("Projectile"))
        {
            DestroyObject();
            // Destroy the projectile as well
            Destroy(other.gameObject);
        }
    }

    private void DestroyObject()
    {
        // Instantiate explosion effect if it is assigned
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, explosionDuration); // Destroy explosion effect after specified duration
        }

        // Destroy the destructible object
        Destroy(gameObject);
    }
}
