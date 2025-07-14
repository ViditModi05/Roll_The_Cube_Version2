using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform.parent); // Parent to the main platform
            PlatformGroupMover mover = GetComponentInParent<PlatformGroupMover>();
            mover.StartMovement();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // Unparent when leaving
        }
    }
}
