using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    public int magazineCapacity = 6;
    public float reloadTime = 2f;
    public GameObject cursorPrefab;
    public float cursorSpeed = 5f;
    public float projectileLifetime = 3f;
    public bool secondFiringModeUnlocked = false;

    private float nextFireTime = 0f;
    private int currentAmmo;
    private bool isReloading = false;
    private bool useCursorFiringMode = false;
    private GameObject cursor;

    private void Start()
    {
        currentAmmo = magazineCapacity;

        if (secondFiringModeUnlocked)
        {
            cursor = Instantiate(cursorPrefab, transform.position, Quaternion.identity);
            cursor.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && secondFiringModeUnlocked)
        {
            useCursorFiringMode = !useCursorFiringMode;

            if (useCursorFiringMode)
            {
                cursor.transform.position = transform.position;
                cursor.SetActive(true);
            }
            else
            {
                cursor.SetActive(false);
            }
        }

        if (useCursorFiringMode)
        {
            HandleCursorMovement();
            if (!isReloading)
            {
                HandleCursorFiringMode();
            }
        }
        else
        {
            HandleIJKLFiringMode();
        }
    }

    private void HandleIJKLFiringMode()
    {
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Time.time >= nextFireTime)
        {
            Vector2 direction = Vector2.zero;

            if ((Input.GetKey(KeyCode.I) && Input.GetKeyDown(KeyCode.L)) || (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.L)))
                direction = new Vector2(1, 1).normalized;
            else if ((Input.GetKey(KeyCode.I) && Input.GetKeyDown(KeyCode.J)) || (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.J)))
                direction = new Vector2(-1, 1).normalized;
            else if ((Input.GetKey(KeyCode.K) && Input.GetKeyDown(KeyCode.L)) || (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.L)))
                direction = new Vector2(1, -1).normalized;
            else if ((Input.GetKey(KeyCode.K) && Input.GetKeyDown(KeyCode.J)) || (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.J)))
                direction = new Vector2(-1, -1).normalized;
            else if (Input.GetKeyDown(KeyCode.I))
                direction = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.K))
                direction = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.J))
                direction = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.L))
                direction = Vector2.right;

            if (direction != Vector2.zero)
            {
                Shoot(direction);
                nextFireTime = Time.time + fireRate;
                currentAmmo--;
            }
        }
    }

    private void HandleCursorMovement()
    {
        if (cursor == null) return;

        Vector3 cursorMovement = Vector3.zero;
        if (Input.GetKey(KeyCode.I)) cursorMovement.y += 1;
        if (Input.GetKey(KeyCode.K)) cursorMovement.y -= 1;
        if (Input.GetKey(KeyCode.J)) cursorMovement.x -= 1;
        if (Input.GetKey(KeyCode.L)) cursorMovement.x += 1;

        cursor.transform.position += cursorMovement * Time.deltaTime * cursorSpeed;
    }

    private void HandleCursorFiringMode()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentAmmo > 0 && Time.time >= nextFireTime)
        {
            Vector2 direction = (cursor.transform.position - firePoint.position).normalized;
            Shoot(direction);
            nextFireTime = Time.time + fireRate;
            currentAmmo--;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
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

    public void ReloadGunAfterDash()
    {
        currentAmmo = magazineCapacity;
        Debug.Log("Gun reloaded after dash.");
        Debug.Log("heljo");
    }

    public void UnlockSecondFiringMode()
    {
        secondFiringModeUnlocked = true;
        cursor = Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity);
        cursor.SetActive(false);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo; // Return the value of the currentAmmo field
        
    }


}
