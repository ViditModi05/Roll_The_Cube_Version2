using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    #region REFERENCES

    [Header("Refs")]
    [SerializeField] protected Transform starVisual;
    [SerializeField] protected GameObject sparkleFX;

    protected GameManager gameManager;

    #endregion


    #region SETTINGS

    [Header("Settings")]
    protected bool isCorrectCollider;
    protected bool collected;

    #endregion


    #region ROTATION SETTINGS

    [Header("Rotation Settings")]
    [SerializeField] protected Vector3 rotation;
    [SerializeField] protected float rotationSpeed;

    #endregion


    #region UNITY METHODS

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
    }

    protected virtual void Update()
    {
        RotateStar();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectStar();
        }
    }

    #endregion


    #region STAR LOGIC

    protected void RotateStar()
    {
        float newRotationSpeed = rotationSpeed * 100f;

        starVisual.Rotate(rotation * newRotationSpeed * Time.deltaTime);
    }

    protected virtual void CollectStar()
    {
        gameManager.AddStars();

        Destroy(gameObject);
    }

    #endregion
}