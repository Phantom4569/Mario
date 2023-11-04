using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    public float horizontal;
    private bool flip = true;
    private Animator animator;
    public int jumpforse;
    public LayerMask Ground;
    public Transform gch;
    private float gchr;
    public bool onGround;
    public float vertical;
    private Vector3 respawnPoint;
    public GameObject DeadZone;
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); 
      animator = GetComponent<Animator>();
      gchr = gch.GetComponent<CircleCollider2D>().radius;
    }
    void Update()
    {
        DeadZone.transform.position = new Vector2(transform.position.x, DeadZone.transform.position.y);
        horizontal = Input.GetAxis("Horizontal")*speed;

       rb.velocity = new Vector2(horizontal,rb.velocity.y);
       animator.SetFloat("moveX",Mathf.Abs(horizontal));
       if ((horizontal > 0 && !flip)||(horizontal < 0 && flip))
       {
        transform.localScale *= new Vector2(-1,1);
         flip = !flip;
       }
       Jump();
       CheckingGround();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpforse);
        }
    }
    void CheckingGround()
    {
      vertical = Input.GetAxis("Jump")*jumpforse;
      onGround = Physics2D.OverlapCircle(gch.position,gchr,Ground);
      animator.SetFloat("Jumping",Mathf.Abs(vertical));
    }
}

