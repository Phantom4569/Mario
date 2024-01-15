using UnityEngine;
using System.Collections;

public class brocken_block : MonoBehaviour
{
    public GameObject block;
    public GameObject bric1;
    public GameObject bric2;
    public Rigidbody2D rb1;
    public Rigidbody2D rb2;
    public GameObject bricMod;
    public Animator an;
    public bool easy;

    void Start()
    {
        bricMod.SetActive(false);
        an = GetComponent<Animator>();
        if (!easy)
        {
            rb1.mass = 0;
            rb2.mass = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "head" && easy)
        {
            block.SetActive(false);
            bric1.SetActive(true);
            bric2.SetActive(true);
            StartCoroutine(elka());
        }else if (collision.tag == "head" && !easy)
        {
            block.SetActive(false);
            bricMod.SetActive(true);
            an.StartPlayback();
            //StartCoroutine(elka1());
            bric1.SetActive(false);
            bric1.SetActive(false);

        }
    }
    IEnumerator elka()
    {
        rb2.AddForce(Vector2.up * 2 / 3 * 50);
        rb1.AddForce(Vector2.up * 2 / 3 * 50);
        yield return new WaitForSeconds(0.03f);
        rb1.AddForce(Vector2.right * 2 * 50);
        rb2.AddForce(Vector2.left * 2 * 50);
        yield return new WaitForSeconds(1f);
        bric1.SetActive(false);
        bric2.SetActive(false);
    }
    IEnumerator elka1()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
