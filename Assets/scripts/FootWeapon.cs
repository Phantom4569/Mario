using UnityEngine;

public class FootWeapon : MonoBehaviour
{
    public player player;
    public bool OnTube;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WeakPoint")
        {
            player.rb.AddForce(Vector2.up * 15 * 50);
        }
        if (collision.gameObject.tag == "tubecoll")
        {
            OnTube = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "tubecoll")
        {
            OnTube = false;
        }
    }
}