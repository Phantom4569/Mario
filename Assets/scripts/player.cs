using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    //GameObjects
    public GameObject DeadZone;
    public GameObject BulletDieZone;
    public GameObject GroundCheck;

    //LayerMasks
    public LayerMask Ground;
    public LayerMask walls;

    //Bools
    private bool flip = true;
    private bool blockMoveX;
    private bool onwall;
    private bool onGround;
    private bool KnockFromRight;

    //UnityComponents
    public Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Collider2D ccr;

    //Float
    public float horizontal;
    private float gchr;
    private float wchR;
    private float JumpWallTime = 0.3f;
    private float timerWallJump;
    private float GravityDef;
    private float KBForce = 6;
    private float KBCounter;
    private float KBTotalTime = 0.2f;


    //text
    public Text coinCount;

    //Integer
    private int jumpforse = 9;
    private int kf = 25;
    private int speed = 5;

    //Transforms
    public Transform GroundCheckTransform;
    public Transform WallCheck;

    //c# scripts
    public Health health;
    public FootWeapon FootWeapon;

    //Vector(2,3)
    private Vector3 respawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gchr = GroundCheckTransform.GetComponent<CircleCollider2D>().radius;
        wchR = WallCheck.GetComponent<CircleCollider2D>().radius;
        GravityDef = rb.gravityScale;
        sr = GetComponent<SpriteRenderer>();
        ccr = GetComponent<Collider2D>();
        ccr.isTrigger = false;
        PlayerPrefs.SetInt("Big",0);
    }

    void Update()
    {
        if (health.live)
        {
            Zones();
            Jump();
            Checkings();
            JumpOnWall();
            Move();
            OnWall();
            AnimSet();
            CoinCounter();
            jumpInTube();
            BigMario();
        }
    }
    void CoinCounter()
    {
        coinCount.text = PlayerPrefs.GetInt("coins").ToString();
    }

    void BigMario()
    {
        animator.SetInteger("BigM", PlayerPrefs.GetInt("Big"));
    }

    void Zones()
    {
        BulletDieZone.transform.position = new Vector2(transform.position.x, transform.position.y);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "plane")
        {
            this.transform.parent = collision.transform;
        }
        else if (collision.gameObject.tag == "monster" && collision.gameObject.GetComponent<Enemy>().CanTDam)
        {
            health.currentHealth-=1;
            KBCounter = KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                KnockFromRight = false;
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                KnockFromRight = true;
            }

        }

    }

    void AnimSet()
    {
        animator.SetBool("onGround", onGround);
        animator.SetBool("onW", onwall);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "plane" && !Physics2D.OverlapCircle(GroundCheckTransform.position, gchr, Ground))
        {
            this.transform.parent = null;
            onGround = false;
        }else if (collision.gameObject.tag == "plane")
        {
            this.transform.parent = null;
        }

    }

    void jumpInTube()
    {
        if (FootWeapon.OnTube && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            rb.AddForce(Vector2.up * jumpforse * 50);
            ccr.isTrigger = true;
        }
        if (ccr.isTrigger)
        {
            StartCoroutine(Timer1s());
        }
    }

    void Jump()
    {

         if (Input.GetKeyDown(KeyCode.Space) && onGround && !onwall)
         {
             rb.AddForce(Vector2.up * jumpforse * 50);
         }

    }

    IEnumerator Timer1s()
    {
        animator.StopPlayback();
        animator.Play("fall");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("lvl1UndG");
    }

    void Move()
    {
        if (KBCounter <= 0)
        {
            sr.color = Color.white;
            if (!blockMoveX)
            {

                horizontal = Input.GetAxis("Horizontal") * speed;
                rb.velocity = new Vector2(horizontal, rb.velocity.y);
                animator.SetFloat("moveX", Mathf.Abs(horizontal));

            }

            if ((horizontal > 0 && !flip) || (horizontal < 0 && flip))
            {

                transform.Rotate(0f, 180f, 0f);
                flip = !flip;

            }
        }
        else if (KBCounter > 0)
        {
            sr.color = Color.red;
            if (KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            else if (!KnockFromRight)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }
            KBCounter -= Time.deltaTime;
        }
    }


    void Checkings()
    {

        onwall = Physics2D.OverlapCircle(WallCheck.position, wchR, walls);
        onGround = Physics2D.OverlapCircle(GroundCheckTransform.position, gchr, Ground);

    }

    void OnWall()
    {

        if (!blockMoveX)
        {

            if (onwall && !onGround)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector3(0,-0.5f,0);
                animator.StopPlayback();
                animator.Play("onwall");
            }
            else if (!onwall && !onGround)
            {
                rb.gravityScale = GravityDef;
            }

        }

    }

    void JumpOnWall()
    {

        if (onwall && !onGround && Input.GetKeyDown(KeyCode.Space))
        {

            blockMoveX = true;
            transform.Rotate(0f, 180f, 0f);
            flip = !flip;

            if (flip)
            {
                rb.AddForce(Vector2.up * jumpforse * kf);
                rb.AddForce(Vector2.right * jumpforse * kf);

            }
            else if (!flip)
            {
                rb.AddForce(Vector2.up * jumpforse * kf);
                rb.AddForce(Vector2.right * -jumpforse * kf);

            }

        }

        if (blockMoveX && (timerWallJump += Time.deltaTime) >= JumpWallTime)
        {
            blockMoveX = false;
            timerWallJump = 0;
        }

    }

}