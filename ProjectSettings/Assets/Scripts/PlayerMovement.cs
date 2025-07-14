using System.Collections;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform blockCheck;
    [SerializeField] private Transform playerVisual;
    private Ground ground;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distance;
    [SerializeField] private float multiplicationFactor;
    private bool movementPaused;
    private bool isMoving;
    private bool isGroundThere;
    private Vector3 direction;
    private Vector2 swipeStart;
    private Vector3 boxCenter;
    private Vector3 boxSize;

    //Raycast to detect if there is anyblock there 
    //If there and swipe detected in the direction of the block then move
    //Roll the cube

    public void IsMovementPaused(bool _enable)
    {
        movementPaused = _enable;
    }
    private void Update()
    {
        if (GameManager.Instance.levelCompleted || isMoving || movementPaused)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Swipe");
            swipeStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipeEnd = Input.mousePosition;
            //Debug.Log("SwipeEnd: " + swipeEnd);
            Vector2 swipe = swipeEnd - swipeStart;
            //Debug.Log("Swipe: " + swipe);
            if (swipe.magnitude < 50f)
            {
                return;
            }
            swipe.Normalize();
            Vector3 dir = Vector3.zero;
            if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
            {
                dir = swipe.x > 0 ? Vector3.right : Vector3.left;
            }
            else
            {
                dir = swipe.y > 0 ? Vector3.forward : Vector3.back;
            }
            direction = dir;
            TryMove();
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void safetyMovementCheck(bool _enable)
    {
        isGroundThere = _enable;
    }
    private void TryMove()
    {
        StartCoroutine(Roll());
    }


    private IEnumerator Roll()
    {
        isMoving = true;
        GameManager.Instance.PlayeMovementAudio();

        float remainingAngle = 90f;
        Vector3 rotationCenter = transform.position + direction * multiplicationFactor + Vector3.down * multiplicationFactor;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0f)
        {
            float rotationStep = Mathf.Min(Time.deltaTime * moveSpeed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationStep);
            remainingAngle -= rotationStep;
            yield return null;
        }

        // Snap position and rotation to grid to fix any floating point drift
        transform.position = new Vector3(
            Mathf.Round(transform.position.x * 1000f) / 1000f,
            Mathf.Round(transform.position.y * 1000f) / 1000f,
            Mathf.Round(transform.position.z * 1000f) / 1000f
        );
        transform.rotation = Quaternion.Euler(
            Mathf.Round(transform.rotation.eulerAngles.x / 90f) * 90f,
            Mathf.Round(transform.rotation.eulerAngles.y / 90f) * 90f,
            Mathf.Round(transform.rotation.eulerAngles.z / 90f) * 90f
        );

        isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spider"))
        {
            GameManager.Instance.GameOver();
        }
        if(collision.gameObject.CompareTag("Lava"))
        {
            GameManager.Instance.GameOver();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 nextPosition = transform.position + direction;
        Vector3 rayOrigin = nextPosition + Vector3.up * .5f;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * 3f);
    }
}