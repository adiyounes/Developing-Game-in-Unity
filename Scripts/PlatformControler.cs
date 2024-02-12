using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControler : MonoBehaviour
{
    private int startingPoint;
    private int index;
    [SerializeField]
    private float platformSpeed;
    [SerializeField]
    private Vector2[] points;
    void Start()
    {
        index = 0;
        transform.position = points[startingPoint];
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, points[index]) < 0.02f)
        {
            index++;
            if (index == points.Length)
            {
                index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[index], platformSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Luffy")
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Luffy")
        {
            collision.transform.SetParent(null);
        }
    }
}
