using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform ShootPos;
    public GameObject Bullet;
    public Text bulletCount;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F) && PlayerPrefs.GetInt("bullets") > 0)
        {
            Instantiate(Bullet, ShootPos.transform.position, transform.rotation);
            PlayerPrefs.SetInt("bullets", PlayerPrefs.GetInt("bullets")-1);
        }
        bulletCount.text = PlayerPrefs.GetInt("bullets").ToString();
    }
}
