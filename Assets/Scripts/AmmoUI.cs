using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Image[] bulletImages; // Array of Image components representing the chambers
    public Sprite fullBulletSprite; // Sprite for a full chamber
    public Sprite emptyBulletSprite; // Sprite for an empty chamber
    public PlayerShooting playerShooting; // Reference to the PlayerShooting script

    private void Update()
    {
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        int currentAmmo = playerShooting.GetCurrentAmmo();

        for (int i = 0; i < bulletImages.Length; i++)
        {
            if (i < currentAmmo)
            {
                bulletImages[i].sprite = fullBulletSprite;
            }
            else
            {
                bulletImages[i].sprite = emptyBulletSprite;
            }
        }
    }
}
