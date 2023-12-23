using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour
{
    private bool movingLeft;
    [SerializeField] private float moveDistance = 3.1f;
    [SerializeField] private float speed = 5f;
    public bool EasyMode;
    public Vector3[] points = new Vector3[5];
    public int position = 0;
    private int i;
    private float leftEdge;
    private float rightEdge;
    private Vector3 nextPos;
    public Transform startPos;


    private void Awake()
    {
        nextPos = startPos.position;
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
            //transform.position = Vector3.MoveTowards(transform.position, points[position], speed * Time.deltaTime);

            //if (position == points.Length - 1)
            //{
            //    position = 0;
            //}
            //else
            //{
            //    position++;
            //}

            foreach (var point in points)
            {
                transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
                Debug.Log(transform.position + " " + point);

                while (transform.position == point)
                {
                    StartCoroutine(timeDelt());
                }
            }
        }
    }
}