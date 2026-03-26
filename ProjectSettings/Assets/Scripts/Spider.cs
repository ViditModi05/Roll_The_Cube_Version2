using UnityEngine;

public class Spider : MonoBehaviour
{
    #region REFERENCES

    [Header("Refs")]

    [SerializeField] private LayerMask lavaLayer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private PlayerManager playerManager;
    private PlayerMovement player;
    private Rigidbody rb;

    #endregion


    #region SETTINGS

    [Header("Settings")]

    [SerializeField] private float dropSpeed = 2f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private float dropDistance = 5f;

    [Space]
    [SerializeField] private Vector3 direction = Vector3.right;

    [Space]
    [SerializeField] private Vector3[] moveDirections;
    [SerializeField] private Quaternion[] moveRotations;

    [Space]
    [SerializeField] private Quaternion rotation = Quaternion.Euler(0, 180, 0);
    [SerializeField] private Material lineRendererMat;

    [Space]
    [SerializeField] private int remainingNumberOfRotations;

    #endregion


    #region PRIVATE VARIABLES

    private Vector3 startPosition;
    private Vector3 dropTarget;

    private bool isDropping = false;
    private bool canChase = false;

    #endregion


    #region UNITY METHODS

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;
        player = playerManager?.playerMovement;

        if (player == null)
        {
            Debug.LogError("PlayerMovement reference not found!");
        }

        startPosition = lineRenderer.transform.position;
        dropTarget = startPosition + Vector3.down * dropDistance;

        rb.isKinematic = true;

        SetupLineRenderer();
    }

    private void Update()
    {
        if (OnLava() || GameManager.Instance.levelCompleted)
        {
            Destroy(gameObject);
        }

        UpdateThread();
    }

    private void FixedUpdate()
    {
        if (isDropping && !canChase)
        {
            lineRenderer.enabled = true;
            DropDown();
        }
        else if (canChase)
        {
            ChasePlayer();
        }
    }

    #endregion


    #region LINE RENDERER

    private void SetupLineRenderer()
    {
        if (lineRenderer == null) return;

        lineRenderer.material = lineRendererMat;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = .05f;
        lineRenderer.endWidth = .05f;
    }

    private void UpdateThread()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, lineRenderer.transform.position);
        }
    }

    #endregion


    #region DROP LOGIC

    public void DropSpider(bool _enable)
    {
        isDropping = _enable;
    }

    private void DropDown()
    {
        lineRenderer.enabled = true;

        Vector3 newPos = Vector3.MoveTowards(
            rb.position,
            dropTarget,
            dropSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);

        if (Vector3.Distance(newPos, dropTarget) < 0.01f)
        {
            canChase = true;

            rb.isKinematic = false;

            RotateSpider(rotation);

            lineRenderer.enabled = false;
        }
    }

    #endregion


    #region CHASE LOGIC

    private void ChasePlayer()
    {
        if (player == null) return;

        if (!isGroundAhead())
        {
            bool changed = FindNewDirection();
        }

        rb.MovePosition(
            rb.position +
            direction * chaseSpeed * Time.fixedDeltaTime
        );
    }

    #endregion


    #region MOVEMENT LOGIC

    private bool isGroundAhead()
    {
        return Physics.Raycast(
            groundCheck.position,
            Vector3.down,
            1f,
            groundLayer
        );
    }

    private bool FindNewDirection()
    {
        if (remainingNumberOfRotations <= 0)
        {
            return false;
        }

        for (int i = 0; i < moveDirections.Length; i++)
        {
            Vector3 origin = transform.position;
            Vector3 dir = moveDirections[i];

            if (Physics.Raycast(origin, dir, out RaycastHit hit, 1f, groundLayer))
            {
                direction = moveDirections[i];

                RotateSpider(moveRotations[i]);

                remainingNumberOfRotations--;

                return true;
            }
        }

        return false;
    }

    private void RotateSpider(Quaternion _rotation)
    {
        transform.rotation = _rotation;
    }

    #endregion


    #region ENVIRONMENT CHECKS

    private bool OnLava()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.red);

        return Physics.Raycast(
            transform.position,
            Vector3.down,
            1.5f,
            lavaLayer
        );
    }

    #endregion


    #region GIZMOS

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.cyan;

        foreach (Vector3 dir in moveDirections)
        {
            Gizmos.DrawRay(transform.position, dir);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawRay(groundCheck.position, Vector3.down);
    }

    #endregion
    private void HitPlayer()
    {
        Debug.Log("Spider hit player!");

        // Stop spider
        canChase = false;
        isDropping = false;
        rb.linearVelocity = Vector3.zero;

        // Stop player movement
        if (player != null)
        {
            player.IsMovementPaused(true);
        }

        // 🔥 CALL GAME OVER
        GameManager.Instance.GameOver();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HitPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HitPlayer();
        }
    }
}