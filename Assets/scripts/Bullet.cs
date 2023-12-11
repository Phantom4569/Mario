using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("monster") || collision.CompareTag("ground") || collision.CompareTag("BDZ"))
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {

    }
}
