using UnityEngine;

public class Rock : MonoBehaviour
{
    public int health = 50; // Health of the destructible object

    // Method to apply damage to the object
    public void IncomingDamage(int damage)
    {
        health -= damage; // Reduce the health by the damage amount

        if (health <= 0)
        {
            Despawn(); // Destroy the object if health is less than or equal to zero
        }
    }

    // Method to handle the object's destruction
    private void Despawn()
    {
        // You can add any destruction effects or animations here

        Destroy(gameObject); // Destroy the object
    }
}
