using UnityEngine;

using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 300;
    bool isMoving = false;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayDistance = 1.1f;

    void Start()
    {
        SnapToGrid();
    }

    void SnapToGrid()
    {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );
    }
    void Update()
    {
        if (isMoving || movementPaused) return;

        // Touch Input (Mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();
            }
        }

        // Optional: Mouse (for testing in editor)
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        Vector2 swipe = endTouchPosition - startTouchPosition;

        if (swipe.magnitude < swipeThreshold)
            return;

        swipe.Normalize();

        // Horizontal swipe
        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            if (swipe.x > 0)
                TryMove(Vector3.right);
            else
                TryMove(Vector3.left);
        }
        // Vertical swipe
        else
        {
            if (swipe.y > 0)
                TryMove(Vector3.forward);
            else
                TryMove(Vector3.back);
        }
    }

    void TryMove(Vector3 direction)
    {
        Vector3 checkPosition = transform.position + direction;

        // Raycast downward from next position
        if (Physics.Raycast(checkPosition + Vector3.up, Vector3.down, rayDistance, groundLayer))
        {
            // Ground exists → move
            StartCoroutine(Roll(direction));
        }
        else
        {
            // No ground → fall
            StartCoroutine(Fall(direction));
        }
    }




    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;



        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + (direction + Vector3.down) * 0.5f;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;

        transform.position = new Vector3(
    Mathf.Round(transform.position.x),
    Mathf.Round(transform.position.y),
    Mathf.Round(transform.position.z)
);

    }


    IEnumerator Fall(Vector3 direction)
    {
        isMoving = true;

        // Small forward tilt before falling (optional polish)
        yield return StartCoroutine(Roll(direction));

        // Fall down
        float fallSpeed = 5f;

        while (transform.position.y > -10f) // limit to avoid infinite fall
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        isMoving = false;

    }
    private bool movementPaused = false;

    public void IsMovementPaused(bool _enable)
    {
        movementPaused = _enable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            HitLava();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            HitLava();
        }
    }

    private void HitLava()
    {
        Debug.Log("Player hit Lava!");

        // Stop all movement immediately
        StopAllCoroutines();
        isMoving = false;
        movementPaused = true;

        // Trigger Game Over
        GameManager.Instance.GameOver();
    }
}