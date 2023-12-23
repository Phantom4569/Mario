using UnityEngine;

public class DroppedCoin : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _JumpAngle = new Vector2(2f, 4);
    void Start()
    {

        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(transform.localScale.x * _JumpAngle.x, _JumpAngle.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 2);
            Destroy(gameObject);
        }
    }
}