using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CompleteLevel();
        }
    }
}
