using System.Collections;
using UnityEngine;

public class EnemyWeakPoint : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FootWeapon")
        {
            enemy.damaged();
        }
    }
}