using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [SerializeField] // This attribute will make the field editable in the Inspector
    private int coins = 0; // Private with SerializeField to protect access while allowing editor changes

    public int Coins
    {
        get => coins;
        private set
        {
            coins = value;
            Debug.Log("Coins updated: " + coins);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Ensures this object isn't destroyed when loading a new scene.
        }
        else
        {
            Destroy(gameObject);  // Ensures only one instance exists.
        }
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public void SpendCoins(int amount)
    {
        if (amount <= Coins)
        {
            Coins -= amount;
        }
        else
        {
            Debug.Log("Not enough coins to spend.");
        }
    }

    public void ResetCoins()
    {
        Coins = 0;
    }
}
