using UnityEngine;

public class Spider : MonoBehaviour
{
    [Header("Refs")]

    [SerializeField] private LayerMask lavaLayer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private PlayerManager playerManager;
    private PlayerMovement player;
    private Rigidbody rb;

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
    [SerializeField] private int remainingNumberOfRotations; // number of times spider can rotate when ground not ahead
    private Vector3 startPosition;
    private Vector3 dropTarget;
    private bool isDropping = false;
    private bool canChase = false;

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

        if (lineRenderer != null)
        {
            lineRenderer.material = lineRendererMat;
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = .05f;
            lineRenderer.endWidth = .05f;
        }
    }

    private void Update()
    {
        if (OnLava() || GameManager.Instance.levelCompleted)
        {
            Destroy(gameObject);
        }


        UpdateThread();
    }

    private bool isGroundAhead() => Physics.Raycast(groundCheck.position, Vector3.down, 1f, groundLayer);

    private bool FindNewDirection()
    {
        //Debug.Log("Entering New Direction Finding Method");
        if (remainingNumberOfRotations <= 0)
        {
            //Debug.Log("Returning");
            return false; // No more allowed rotations
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
                //Debug.Log(" New Direction: "+ moveDirections[i] + " New Rotation: " + moveRotations[i] + " Number Of Rotations Remaining: " + remainingNumberOfRotations);
                return true;
            }
        }
        //Debug.Log("No Ground Detected in Any Direction");
        return false; // No ground in any direction
    }
    public void DropSpider(bool _enable)
    {
        isDropping = _enable;
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


    private void DropDown()
    {
        lineRenderer.enabled = true;
        Vector3 newPos = Vector3.MoveTowards(rb.position, dropTarget, dropSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector3.Distance(newPos, dropTarget) < 0.01f)
        {
            canChase = true;
            rb.isKinematic = false;
            RotateSpider(rotation);
            lineRenderer.enabled = false;
        }
    }

    private void RotateSpider(Quaternion _rotation)
    {
        transform.rotation = _rotation;
    }

    private void ChasePlayer()
    {
        if (player == null) return;
        if (!isGroundAhead())
        {
            //Debug.Log("No Ground Ahead");
            bool changed = FindNewDirection();
            // If it couldn't change direction, just continue moving in current direction
        }

        rb.MovePosition(rb.position + direction * chaseSpeed * Time.fixedDeltaTime);

    }


    private void UpdateThread()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, lineRenderer.transform.position);
        }
    }

    private bool OnLava()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, 1.5f, lavaLayer);
    }

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
}

