using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1);
            Destroy(gameObject);
        }
    }
}
