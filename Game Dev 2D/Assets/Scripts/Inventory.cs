using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int coins;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }
}
