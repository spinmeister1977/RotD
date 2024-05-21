using System.Collections; // Required for IEnumerator
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal movement speed
    public float dashSpeed = 15f; // Speed during dash
    public float dashTime = 0.2f; // Duration of the dash
    public float dashCooldown = 1.5f; // Cooldown time between dashes

    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private float nextDashTime = 0f;

    private PlayerShooting playerShooting; // Reference to the PlayerShooting script

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerShooting = GetComponent<PlayerShooting>(); // Ensure the shooting script is on the same GameObject
    }

    private void Update()
    {
        if (!isDashing)
        {
            movement.x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
            movement.y = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= nextDashTime && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = movement.normalized * moveSpeed;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown + dashTime;

        // Calculate dash direction based on current movement, even if it's zero (standing still)
        Vector2 dashDirection = movement.normalized;
        rb.velocity = dashDirection * dashSpeed;

        // Dash duration
        yield return new WaitForSeconds(dashTime);

        // Reset movement to prevent continuous dashing
        isDashing = false;

        // Automatically reload the gun after dashing
        if (playerShooting != null)
        {
            playerShooting.ReloadGunAfterDash();
        }
    }
}
