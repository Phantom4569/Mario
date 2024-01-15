using UnityEngine;
using System.Collections;

public class LuckyBlock : MonoBehaviour
{
    public Transform tr;
    public GameObject lBl;
    public GameObject coin;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject mushroom;
    public GameObject mushroom2;
    public GameObject star;
    private int rnd;
    private bool oneTime = true;
    private Animator an;
    private Vector3 pos;
    private void Start()
    {
        an = GetComponent<Animator>();
        an.StopPlayback();
        pos = lBl.transform.position;
        pos.y += 0.3f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "head" && oneTime)
        {
            lBl.SetActive(false);
            an.SetBool("pl",true);
            StartCoroutine(elka());
            oneTime = false;
            rnd = Random.Range(1, 3);
            switch (rnd)
            {
                case 1:
                    Instantiate(coin, pos, transform.rotation);
                    break;
                 //case 3:
                    //    Instantiate(star, pos, transform.rotation);
                    //    break;
                 case 2:
                    Instantiate(mushroom, pos, transform.rotation);
                    break;
                case 3:
                    Instantiate(mushroom2, pos, transform.rotation);
                    break;
                case 4:
                    Instantiate(monster1, pos, transform.rotation);
                    break;
                 //case 0:
                    //    Instantiate(monster2, pos, transform.rotation);
                    //    break;
            }
        }
    }
    IEnumerator elka()
    {
        yield return new WaitForSeconds(0.2f);
        an.SetBool("pl", false);
    }
}