using UnityEngine;

public class LuckyBlock : MonoBehaviour
{
    public Transform tr;
    public GameObject coin;
    public GameObject mushroom;
    public GameObject star;
    private int rnd;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "head")
        {
            gameObject.SetActive(false);
            rnd = Random.Range(0, 3);
            switch (rnd)
            {
                case 1:
                    Instantiate(coin,transform.position,transform.rotation);
                    break;
                case 2:
                    Instantiate(star, transform.parent);
                    break;
                case 3:
                    Instantiate(mushroom, transform.parent);
                    break;
            }
        }
    }
}