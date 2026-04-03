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

        // Touch Input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();
            }
        }

        // Mouse (Editor testing)
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

        if (Camera.main == null)
            return;

        swipe.Normalize();

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * swipe.y + camRight * swipe.x;

        Vector3 finalDir;

        if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.z))
        {
            finalDir = (moveDir.x > 0) ? Vector3.right : Vector3.left;
        }
        else
        {
            finalDir = (moveDir.z > 0) ? Vector3.forward : Vector3.back;
        }

        TryMove(finalDir);
    }

    void TryMove(Vector3 direction)
    {
        Vector3 checkPosition = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z + direction.z)
        );

        RaycastHit hit;

        if (Physics.Raycast(checkPosition + Vector3.up, Vector3.down, out hit, rayDistance, groundLayer))
        {
            StartCoroutine(Roll(direction));
        }
        else
        {
            StartCoroutine(Fall(direction));
        }
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        float duration = 0.25f;
        float elapsed = 0f;

        Vector3 pivot = transform.position + (direction + Vector3.down) * 0.5f;
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        while (elapsed < duration)
        {
            float step = (90f / duration) * Time.deltaTime;

            transform.RotateAround(pivot, axis, step);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap to perfect grid after movement
        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );

        transform.rotation = Quaternion.Euler(
            Mathf.Round(transform.rotation.eulerAngles.x / 90) * 90,
            Mathf.Round(transform.rotation.eulerAngles.y / 90) * 90,
            Mathf.Round(transform.rotation.eulerAngles.z / 90) * 90
        );

        isMoving = false;
    }

    IEnumerator Fall(Vector3 direction)
    {
        isMoving = true;

        yield return StartCoroutine(Roll(direction));

        float fallSpeed = 5f;

        while (transform.position.y > -10f)
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

        StopAllCoroutines();
        isMoving = false;
        movementPaused = true;

        GameManager.Instance.GameOver();
    }
}