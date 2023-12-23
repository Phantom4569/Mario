
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public float horizontal;
    private Animator animator;
    public int jumpforse;
    public LayerMask Ground;
    public Transform gch;
    private float gchr;
    public bool onGround;
    public float vertical;
    private Vector3 respawnPoint;
    public GameObject DeadZone;
    private Vector3 RespawnPoint;
    public float JumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gchr = gch.GetComponent<CircleCollider2D>().radius;
        RespawnPoint = transform.position;
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);


        CheckingGround();
        animator.SetFloat("moveX", Mathf.Abs(rb.velocity.x));
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(gch.position, gchr, Ground);
        animator.SetBool("onGround", onGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
        {
            transform.position = RespawnPoint;
            //SceneManager.LoadScene("menu");

        }
        else if (collision.tag == "jpoint")
        {
            Jump();
        }
    }
}