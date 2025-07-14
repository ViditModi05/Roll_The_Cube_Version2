using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform[] waypoints; 

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTime = 1f;

    private int index = 0;
    [SerializeField] private bool waiting = false;

    private void Start()
    {
        if (waypoints.Length > 0)
            transform.position = waypoints[0].position;

        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            if (!waiting && waypoints.Length > 0)
            {
                Transform target = waypoints[index];
                while (Vector3.Distance(transform.position, target.position) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                // Snap to position exactly
                transform.position = target.position;

                // Wait before moving to next waypoint
                waiting = true;
                yield return new WaitForSeconds(waitTime);
                waiting = false;
                index++;
                if (index >= waypoints.Length)
                {
                    index = 0;
                }
            }

            yield return null;
        }
    }
}
