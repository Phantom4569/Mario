using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    private float speed = 5f;
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
    public GameObject groundch;
    private Vector3 respawnPoint;
    public GameObject DeadZone;
    public GameObject BulletDieZone;
    public Transform wch;
    public bool onwall;
    private float wchR;
    public LayerMask walls;
    private bool blockMoveX;
    private float JumpWallTime = 0.3f;
    private float timerWallJump;
    public Vector2 JumpAngle = new Vector2(2f, 4);
    public float JumpTime = 4;
    private float GravityDef;
    private bool onW;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        gchr = gch.GetComponent<CircleCollider2D>().radius;
        wchR = wch.GetComponent<CircleCollider2D>().radius;
        GravityDef = rb.gravityScale;
    }
    void Update()
    {
        Zones();
        Jump();
        //CheckingGround();
        checkingwalls();
        JumpOnWall();
        walking();
        onwallgr();
    }
    void Zones()
    {
        DeadZone.transform.position = new Vector2(transform.position.x, DeadZone.transform.position.y);
        BulletDieZone.transform.position = new Vector2(transform.position.x,transform.position.y);
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
        else if (collision.tag == "winplace")
        {
            SceneManager.LoadScene("win");
        }
        //else if (collision.tag == "monster")
        //{
        //    rb.AddForce(Vector2.up * 2 * 50);
        //    if (flip)
        //    {
        //        rb.AddForce(Vector2.right * -100);
        //    }
        //    if (!flip)
        //    {
        //        rb.AddForce(Vector2.left * -100);
        //    }
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "plane")
        {
            onGround = true;
            animator.SetBool("onGround", onGround);
        }
        if(collision.gameObject.tag == "plane")
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "plane")
        {
            onGround = false;
            animator.SetBool("onGround", onGround);
        }
        if (collision.gameObject.tag == "plane")
        {
            this.transform.parent = null;
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround && !onW)
        {
            rb.AddForce(Vector2.up * jumpforse * 50);
        }
    }
    //void CheckingGround()
    //{
    //    onGround = Physics2D.OverlapCircle(gch.position,gchr,Ground);
    //    animator.SetBool("onGround", onGround);
    //}
    void walking()
    {
        if(!blockMoveX)
        {
            horizontal = Input.GetAxis("Horizontal") * speed;
            rb.velocity = new Vector2(horizontal, rb.velocity.y);
            animator.SetFloat("moveX", Mathf.Abs(horizontal));
        }
        if ((horizontal > 0 && !flip) || (horizontal < 0 && flip))
        {
            //transform.localScale *= new Vector2(-1, 1);
            transform.Rotate(0f, 180f, 0f);
            flip = !flip;
        }
    }
    void onwallgr()
    {
        if (!blockMoveX)
        {
            if (onwall && !onGround)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, 0);
                animator.StopPlayback();
                animator.Play("onwall");
                animator.SetBool("onW", onW);
            }
            else if (!onwall && !onGround)
            {
                rb.gravityScale = GravityDef;
                animator.SetBool("onW", onW);
            }
        }  
    }
    void checkingwalls()
    {
        onwall = Physics2D.OverlapCircle(wch.position, wchR, walls);
    }
    void JumpOnWall()
    {
        if (onwall && !onGround && Input.GetKeyDown(KeyCode.Space))
        {
            blockMoveX = true;
            transform.localScale *= new Vector2(-1, 1);
            flip = !flip;
            rb.velocity = new Vector2(transform.localScale.x * JumpAngle.x, JumpAngle.y);
            Debug.Log("Jump");
        }
        if (blockMoveX && (timerWallJump += Time.deltaTime) >= JumpWallTime)
        {
            blockMoveX = false;
            timerWallJump = 0;
        }
    }

}