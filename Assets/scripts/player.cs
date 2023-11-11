using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float speed = 5f;
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
    public Transform wchRight;
    public bool onwallRight;
    private float wchrRight;
    public LayerMask walls;
    private bool blockMoveX;
    private float JumpWallTime = 0.3f;
    private float timerWallJump;
    public Vector2 JumpAngle = new Vector2(2f, 4);
    private float timeronGroun;
    public float JumpTime = 4;
    private bool onG;

    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); 
      animator = GetComponent<Animator>();
      gchr = gch.GetComponent<CircleCollider2D>().radius;
      wchrRight = wchRight.GetComponent<CircleCollider2D>().radius;
    }
    void Update()
    {
        DeadZone.transform.position = new Vector2(transform.position.x, DeadZone.transform.position.y);
        if ((horizontal > 0 && !flip)||(horizontal < 0 && flip))
        {
         transform.localScale *= new Vector2(-1,1);
          flip = !flip;
        }
        Jump();
        CheckingGround();
        checkingwallsRight();
        JumpOnWall();
        walking();
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
            //rb.velocity = new Vector2(rb.velocity.x,jumpforse);
            rb.AddForce(Vector2.up * jumpforse * 50);
        }
    }
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(gch.position,gchr,Ground);
        animator.SetBool("onGround", onGround);
        if (timeronGroun >= JumpTime)
        {
            onG = false;
            animator.SetBool("onG", onG);
            timeronGroun = 0;
        }
        else
        {
            onG = true;
            animator.SetBool("onG", onG);
        }
    }
    void walking()
    {
        if(!blockMoveX)
        {
            horizontal = Input.GetAxis("Horizontal") * speed;
            rb.velocity = new Vector2(horizontal, rb.velocity.y);
            animator.SetFloat("moveX", Mathf.Abs(horizontal));
        }
    }
    void checkingwallsRight()
    {
        onwallRight = Physics2D.OverlapCircle(wchRight.position, wchrRight, walls);
    }
    void JumpOnWall()
    {
        if (onwallRight && !onGround && Input.GetKeyDown(KeyCode.Space))
        {
            blockMoveX = true;
            transform.localScale *= new Vector2(-1, 1);
            flip = !flip;
            rb.velocity = new Vector2(transform.localScale.x * JumpAngle.x, JumpAngle.y);
        }
        if (blockMoveX && (timerWallJump += Time.deltaTime) >= JumpWallTime)
        {
            blockMoveX = false;
            timerWallJump = 0;
        }
    }
}

