using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour
{
    private bool movingLeft;
    [SerializeField] private float moveDistance = 3.1f;
    [SerializeField] private float speed = 3f;
    public bool EasyMode;
    public Vector3[] points = new Vector3[5];
    private int i = 0;
    private float leftEdge;
    private float rightEdge;
    public Transform startPos;
    private bool pbl;

    private void Awake()
    {
        leftEdge = transform.position.x - moveDistance;
        rightEdge = transform.position.x + moveDistance;
    }

    IEnumerator timeDelt()
    {
        yield return new WaitForSeconds(0.001f);
    }

    private void Update()
    {
        if (EasyMode)
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                }

                else
                {
                    movingLeft = false;
                }
            }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                }

                else
                {
                    movingLeft = true;
                }
            }
        }
        else if (!EasyMode)
        {
            //transform.position = Vector3.MoveTowards(transform.position, points[i], speed * Time.deltaTime);
            //if (i == points.Length - 1)
            //{
            //    i = 0;
            //}
            //else
            //{
            //    i++;
            //}
            transform.position = Vector3.MoveTowards(transform.position, points[i], speed * Time.deltaTime);
            if (transform.position == points[i])
            {
                i++;
            }
            if (i >= points.Length)
            {
                i = 0;
            }
        }
    }
}