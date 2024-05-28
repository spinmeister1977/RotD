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
    public int damage = 20;


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
            HandleModeSwitch();

            if (currentMode == FiringMode.Secondary && isSecondaryModeUnlocked)
            {
                HandleCursorMovement();
                if (Input.GetKeyDown(KeyCode.J))
                    TryShoot();
            }
            else
            {
                HandleArrowKeyShooting();
            }

            CheckAmmoAndReload();
        
    

            if (currentMode == FiringMode.Secondary && isSecondaryModeUnlocked)
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
            CheckAmmoAndReload();
    }
    private void HandleModeSwitch()
    {
        // Check for mode switching to Primary
        if (Input.GetKeyDown(KeyCode.I) && isPrimaryModeUnlocked)
        {
            if (currentMode == FiringMode.Secondary && cursor != null)
            {
                cursor.SetActive(false);  // Deactivate the cursor when switching away
            }
            currentMode = FiringMode.Primary;
        }

        // Check for mode switching to Secondary
        if (Input.GetKeyDown(KeyCode.O) && isSecondaryModeUnlocked)
        {
            currentMode = FiringMode.Secondary;
            if (cursor == null)
            {
                InitializeCursor(); // Create the cursor if it does not exist
            }
            else
            {
                cursor.transform.position = transform.position; // Update cursor's position to player's position
                cursor.SetActive(true); // Make sure the cursor is active
            }
        }

        // Check for mode switching to Shotgun
        if (Input.GetKeyDown(KeyCode.P) && isShotgunModeUnlocked)
        {
            if (currentMode == FiringMode.Secondary && cursor != null)
            {
                cursor.SetActive(false);  // Deactivate the cursor when switching away
            }
            currentMode = FiringMode.Shotgun;
        }
    }
    private void CheckAmmoAndReload()
    {
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
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
    private void DestroyCursor()
    {
        if (cursor != null) // Destroy cursor if switching away from secondary mode
        {
            Destroy(cursor);
            cursor = null; // Set cursor reference to null after destroying it
        }
    }
    private void InitializeCursor()
    {
        // Only instantiate if cursorPrefab is assigned and cursor does not already exist
        if (cursorPrefab && cursor == null)
        {
            cursor = Instantiate(cursorPrefab, transform.position, Quaternion.identity);
        }
        cursor.transform.position = transform.position;  // Ensure the cursor spawns at the player's position
        cursor.SetActive(true);
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

        // Destroy the projectile after a certain time
        Destroy(projectile, projectileLifetime);

        // Check for collisions with enemies
        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, direction, projectileLifetime * projectileSpeed);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
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

