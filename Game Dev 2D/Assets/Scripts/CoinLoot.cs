using UnityEngine;

public class CoinLoot : MonoBehaviour
{
    public int coinValue = 1;
    public Inventory inventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory = collision.GetComponent<Inventory>();
            Destroy(gameObject);
            inventory.AddCoins(coinValue);
        }
    }

}
