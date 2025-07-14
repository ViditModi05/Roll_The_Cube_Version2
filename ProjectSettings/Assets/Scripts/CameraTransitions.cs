using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Vector3 spiderEnemy;
    [SerializeField] private Quaternion spiderRotation;
    private Camera cam;

    [Header("Settings")]
    [SerializeField] private float zoomInSize;
    [SerializeField] private float zoomOutSize;        
    [SerializeField] private float followDuration = 1f;       
    [SerializeField] private float followSpeed = 5f;          
    private bool isFocusing = false;
    private Vector3 originalPosition;
    private float originalSize;
    private Quaternion originalRotation;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalPosition = transform.position;
        originalSize = cam.orthographicSize;
        originalRotation = transform.rotation;
    }

    public void FocusOnSpider()
    {
        if (!isFocusing)
            StartCoroutine(FocusRoutine());
    }

    IEnumerator FocusRoutine()
    {
        isFocusing = true;

        float elapsedTime = 0f;
        float startSize = cam.orthographicSize;

        while (elapsedTime < followDuration)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, zoomInSize, elapsedTime / followDuration);
            Vector3 targetPos = new Vector3(spiderEnemy.x, spiderEnemy.y, originalPosition.z);
            Quaternion targetRotation = spiderRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.orthographicSize = zoomInSize;
        float elapsedReturn = 0f;
        float returnDuration = 0.5f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsedReturn < returnDuration)
        {
            cam.orthographicSize = Mathf.Lerp(zoomInSize, originalSize, elapsedReturn / returnDuration);
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsedReturn / returnDuration);
            transform.rotation = Quaternion.Lerp(startRot, originalRotation, elapsedReturn/returnDuration);
            elapsedReturn += Time.deltaTime;
            yield return null;
        }

        cam.orthographicSize = originalSize;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        isFocusing = false;
    }
}
