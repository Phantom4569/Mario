using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loading : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public float horizontal;
    private bool flip = true;
    private Animator animator;
    public float jumpforse;
    public LayerMask Ground;
    public Transform gch;
    private float gchr;
    public bool onGround;
    public float vertical;
    private Vector3 respawnPoint;
    public GameObject DeadZone;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gchr = gch.GetComponent<CircleCollider2D>().radius;

    }
    private void Update()
    {
        walking();
        CheckingGround();
        DeadZoner();
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpforse * 50000);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
        {
            transform.position = respawnPoint;
            SceneManager.LoadScene("menu");
        }
        else if (collision.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }
        else if (collision.tag == "jpoint")
        {
            Jump();
        }
    }
    void walking()
    {
        rb.velocity = new Vector2(4,0);

    }
    void DeadZoner()
    {
        DeadZone.transform.position = new Vector2(transform.position.x, DeadZone.transform.position.y);
    }
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(gch.position, gchr, Ground);
        animator.SetBool("onGround1", onGround);
    }
}
