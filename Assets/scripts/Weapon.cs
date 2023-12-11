using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Transform ShootPos;
    public GameObject Bullet;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(Bullet, ShootPos.transform.position, transform.rotation);
        } 
    }
}
