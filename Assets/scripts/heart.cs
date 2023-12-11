using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    [SerializeField] private float heartValue;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collision.GetComponent<Health>().isHealthMax())
        {
            collision.GetComponent<Health>().addHeath(heartValue);
            gameObject.SetActive(false);
        }
    }
}