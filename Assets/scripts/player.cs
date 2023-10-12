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
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); 
      animator = GetComponent<Animator>();
    }
    void Update()
    {
       horizontal = Input.GetAxis("Horizontal")*speed;

       rb.velocity = new Vector2(horizontal,rb.velocity.y);
       animator.SetFloat("moveX",Mathf.Abs(horizontal));
       if ((horizontal > 0 && !flip)||(horizontal < 0 && flip))
       {
        transform.localScale *= new Vector2(-1,1);
         flip = !flip;
       }
    }
}

