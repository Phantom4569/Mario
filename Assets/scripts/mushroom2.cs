using UnityEngine;

public class mushroom2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PlayerPrefs.GetInt("Big") >= 1)
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("Big", PlayerPrefs.GetInt("Big")-1);
        }
    }
}

