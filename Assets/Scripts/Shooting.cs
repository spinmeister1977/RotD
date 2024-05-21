using UnityEngine;
using System.Collections;

public enum FiringMode
{
    Primary = 0,    // 8-directional shooting with arrow keys
    Secondary = 1,  // Cursor-based shooting controlled by arrow keys
    Shotgun = 2     // 8-directional shooting with arrow keys
}

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] projectilePrefabs;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    public int magazineCapacity = 6;
    public float reloadTime = 2f;
    public GameObject cursorPrefab;
    public float cursorSpeed = 5f;
    public float projectileLifetime = 3f;

    [SerializeField] private bool isPrimaryModeUnlocked = true;  // Initialized as unlocked
    [SerializeField] private bool isSecondaryModeUnlocked = false;
    [SerializeField] private bool isShotgunModeUnlocked = false;

    private FiringMode currentMode = FiringMode.Primary;
    private float nextFireTime = 0f;
    private int currentAmmo;
    private bool isReloading = false;
    private GameObject cursor;

    private void Start()
    {
        currentAmmo = magazineCapacity;
        if (isSecondaryModeUnlocked && cursorPrefab)
        {
            cursor = Instantiate(cursorPrefab, transform.position, Quaternion.identity);
            cursor.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isReloading)
        {
            if (Input.GetKeyDown(KeyCode.I) && isPrimaryModeUnlocked)
                currentMode = FiringMode.Primary;
            if (Input.GetKeyDown(KeyCode.O) && isSecondaryModeUnlocked)
                currentMode = FiringMode.Secondary;
            if (Input.GetKeyDown(KeyCode.P) && isShotgunModeUnlocked)
                currentMode = FiringMode.Shotgun;

            if (currentMode == FiringMode.Secondary)
            {
                HandleCursorMovement();
                if (Input.GetKeyDown(KeyCode.J))
                    TryShoot();
            }
            else
            {
                HandleArrowKeyShooting();
            }
        }
    }

    private void HandleCursorMovement()
    {
        if (cursor == null) return;
        Vector3 cursorMovement = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            cursorMovement.y += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            cursorMovement.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            cursorMovement.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            cursorMovement.x += 1;

        cursor.transform.position += cursorMovement * cursorSpeed * Time.deltaTime;
    }

    private void HandleArrowKeyShooting()
    {
        Vector2 shootDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            shootDirection.y += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            shootDirection.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            shootDirection.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            shootDirection.x += 1;

        if (shootDirection != Vector2.zero && Time.time >= nextFireTime && currentAmmo > 0)
        {
            shootDirection.Normalize();
            Shoot(shootDirection);
            nextFireTime = Time.time + fireRate;
            currentAmmo--;
        }
    }

    private void TryShoot()
    {
        if (currentAmmo > 0 && Time.time >= nextFireTime)
        {
            Vector2 direction = (cursor.transform.position - firePoint.position).normalized;
            Shoot(direction);
            nextFireTime = Time.time + fireRate;
            currentAmmo--;
        }
    }

    private void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefabs[(int)currentMode], firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
        Destroy(projectile, projectileLifetime);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineCapacity;
        isReloading = false;
        Debug.Log("Reload complete.");
    }


public int GetCurrentAmmo()
    {
        return currentAmmo;

    }
    public void ReloadGunAfterDash()
    {
        currentAmmo = magazineCapacity;
        Debug.Log("Gun reloaded after dash.");
    }
}

