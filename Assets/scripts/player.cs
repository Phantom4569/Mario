using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    public float horizontal;
    private bool flipRight = true;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
       horizontal = Input.GetAxis("Horizontal")*speed;
       rb.velocity = new Vector2(horizontal,rb.velocity.y);
       if(horizontal > 0 && !flipRight)
       {
        Flip();
       }else if(horizontal < 0 && flipRight)
       {
        Flip();
       }
    }
    void Flip(
    )
    {
       flipRight = !flipRight;
       Vector3 theScale = transform.localScale;
       theScale.x = theScale.x * (-1);
       transform.localScale = theScale; 
    }
}

