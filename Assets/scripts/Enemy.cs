using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    [SerializeField] private GameObject DroppedCoin;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    public int hilka = 3;
    private Animator animator;
    public SpriteRenderer sr;
    private float yy;
    public bool CanTDam;


    private void Awake()
    {
        leftEdge = transform.position.x - moveDistance;
        rightEdge = transform.position.x + moveDistance;
        animator = GetComponent<Animator>();
        yy = transform.position.y - 0.11f;
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        move();
        health();
    }
    
    void move()
    {
        if (hilka > 0)
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    movingLeft = false;
                }
            }
            else
            {
                if (transform.position.x < rightEdge)
                {

                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

                }
                else
                {
                    movingLeft = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Player" && hilka > 0)
        //{
        //    collision.GetComponent<Health>().TakeDamage(damage);
        //}
        if (collision.tag == "Bullet")
        {
            damaged();
        }
    }
    IEnumerator Die()
    {
        animator.Play("enemy_die");
        transform.position = new Vector2(transform.position.x , yy);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        Instantiate(DroppedCoin, transform.position, transform.rotation);
    }
    void health()
    {
        if (hilka <= 0)
        {
            StartCoroutine(Die());
        }
    }
    public IEnumerator Damage()
    {
        hilka -= 1;
        CanTDam = false;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        CanTDam = true;
        sr.color = Color.white;
    }
    public void damaged()
    {
        StartCoroutine(Damage());
    }
}