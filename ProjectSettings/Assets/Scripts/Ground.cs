using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ground : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Star star;
    [Header("Settings")]
    [SerializeField] private string colliderName;
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(colliderName))
        {
            Debug.Log("Collided");   
        }
        else
        {
            Debug.Log("Not the right Collider" + other.gameObject.name);
        }
    }
}
