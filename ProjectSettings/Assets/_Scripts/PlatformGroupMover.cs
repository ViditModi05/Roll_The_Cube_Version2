using UnityEngine;

public class PlatformGroupMover : MonoBehaviour
{
    [Header("Target Transform")]
    public Vector3 targetPosition;
    public Vector3 targetEulerRotation;

    [Header("Timing")]
    public float duration = 2f;
    public AnimationCurve easingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 startPos;
    private Quaternion startRot;
    private Quaternion targetRot;
    private float timer = 0f;
    private bool isMoving = false;

    void Start()
    {
    }

    public void StartMovement()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        targetRot = Quaternion.Euler(targetEulerRotation);
        timer = 0f;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);
        float easedT = easingCurve.Evaluate(t);

        transform.position = Vector3.Lerp(startPos, targetPosition, easedT);
        transform.rotation = Quaternion.Slerp(startRot, targetRot, easedT);

        if (t >= 1f) isMoving = false;
    }
}
