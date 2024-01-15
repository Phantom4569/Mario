using System.Collections;
using UnityEngine;

public class BrokedBrick : MonoBehaviour
{
    public GameObject block;
    public GameObject bricR;
    public GameObject bricL;
    public GameObject bricRU;
    public GameObject bricLU;
    public GameObject BricModul;
    public GameObject Coin;
    public Rigidbody2D rbR;
    public Rigidbody2D rbL;
    public Animator an;
    private int rnd;
    public bool easy;

    void Start()
    {
        block.SetActive(true);
        BricModul.SetActive(false);
        an = BricModul.GetComponent<Animator>();
        if (!easy)
        {
            rbR.mass = 0;
            rbL.mass = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "head" && PlayerPrefs.GetInt("Big") >= 1)
        {
            rnd = Random.Range(1, 20);
            if (!easy)
            {
                StartCoroutine(elka1());
            }
            else
            {
                StartCoroutine(elka());
            }
        }
    }
    IEnumerator elka()
    {
        block.SetActive(false);
        bricR.SetActive(true);
        bricL.SetActive(true);
        an.StopPlayback();
        rbL.AddForce(Vector2.up * 2 / 3 * 50);
        rbR.AddForce(Vector2.up * 2 / 3 * 50);
        yield return new WaitForSeconds(0.03f);
        rbR.AddForce(Vector2.right * 2 * 50);
        rbL.AddForce(Vector2.left * 2 * 50);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
    IEnumerator elka1()
    {
        block.SetActive(false);
        BricModul.SetActive(true);
        an.StopPlayback();
        an.Play("BrockenBrick");
        yield return new WaitForSeconds(0.55f);
        if (rnd > 17)
        {
            Instantiate(Coin, transform.parent);
        }
        gameObject.SetActive(false);
    }
}
