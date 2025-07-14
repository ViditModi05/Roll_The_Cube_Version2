using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Star : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] protected Transform starVisual;
    [SerializeField] protected GameObject sparkleFX;
    protected GameManager gameManager;

    [Header("Settings")]
    protected bool isCorrectCollider;
    protected bool collected;

    [Header("Rotation Settings")]
    [SerializeField] protected Vector3 rotation;
    [SerializeField] protected float rotationSpeed;


    protected virtual void Start()
    {
         gameManager = GameManager.Instance;
    }
    protected virtual void Update()
    {
        float newRotationSpeed = rotationSpeed * 100;
        starVisual.Rotate(rotation * newRotationSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.AddStars();
            Destroy(gameObject);
        }
    }

}
