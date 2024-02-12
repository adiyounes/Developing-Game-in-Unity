using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMoving : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private int index = 0; // Initialize index to the first point.
    [SerializeField] private Vector2[] points;
    private bool startMoving;

    void Start()
    {
        startMoving = false;
    }

    void Update()
    {
        if (startMoving && points != null && index < points.Length)
        {
            MoveTowardsPoint();
        }
    }

    private void MoveTowardsPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[index], moveSpeed * Time.deltaTime);

        // Check if the boat has reached the current point.
        if (Vector2.Distance(transform.position, points[index]) < 0.1f)
        {
            // Move to the next point.
            index++;

            // Check if there are more points, otherwise stop moving.
            if (index >= points.Length)
            {
                startMoving = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Luffy"))
        {
            collision.transform.SetParent(transform);
            startMoving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Luffy"))
        {
            collision.transform.SetParent(null);
        }
    }
}
