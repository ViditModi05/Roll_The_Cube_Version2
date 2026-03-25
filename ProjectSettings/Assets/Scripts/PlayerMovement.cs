using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region REFERENCES

    [Header("References")]
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Transform blockCheck;
    [SerializeField] private LayerMask groundLayer;

    #endregion


    #region SETTINGS

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float multiplicationFactor = 0.5f;
    [SerializeField] private float swipeThreshold = 50f;

    #endregion


    #region PRIVATE VARIABLES

    private bool isMoving;
    private bool movementPaused;

    private Vector3 moveDirection;
    private Vector2 swipeStart;

    #endregion


    #region UNITY METHODS

    private void Update()
    {
        if (CanProcessInput())
        {
            DetectSwipe();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spider") || collision.gameObject.CompareTag("Lava"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 nextPosition = transform.position + moveDirection;
        Vector3 rayOrigin = nextPosition + Vector3.up * 0.5f;

        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * 3f);
    }

    #endregion


    #region INPUT

    private void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipeEnd = Input.mousePosition;
            Vector2 swipe = swipeEnd - swipeStart;

            if (swipe.magnitude < swipeThreshold)
                return;

            moveDirection = GetSwipeDirection(swipe.normalized);

            TryMove();
        }
    }

    private Vector3 GetSwipeDirection(Vector2 swipe)
    {
        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            return swipe.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            return swipe.y > 0 ? Vector3.forward : Vector3.back;
        }
    }

    #endregion


    #region MOVEMENT

    private void TryMove()
    {
        StartCoroutine(Roll());
    }

    private IEnumerator Roll()
    {
        isMoving = true;

        GameManager.Instance.PlayeMovementAudio();

        float remainingAngle = 90f;

        Vector3 rotationCenter =
            transform.position +
            moveDirection * multiplicationFactor +
            Vector3.down * multiplicationFactor;

        Vector3 rotationAxis = Vector3.Cross(Vector3.up, moveDirection);

        while (remainingAngle > 0f)
        {
            float rotationStep = Mathf.Min(Time.deltaTime * moveSpeed, remainingAngle);

            transform.RotateAround(rotationCenter, rotationAxis, rotationStep);

            remainingAngle -= rotationStep;

            yield return null;
        }

        SnapToGrid();

        isMoving = false;
    }

    #endregion


    #region HELPERS

    private bool CanProcessInput()
    {
        return !(GameManager.Instance.levelCompleted || isMoving || movementPaused);
    }

    private void SnapToGrid()
    {
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
    }

    #endregion


    #region PUBLIC METHODS

    public void IsMovementPaused(bool _enable)
    {
        movementPaused = _enable;
    }

    

    #endregion
}