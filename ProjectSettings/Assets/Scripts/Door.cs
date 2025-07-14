using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour
{
    public GameObject other;
     private void OnTriggerEnter(Collider  Other){
     if (Other.CompareTag("Collider 1"))
            {
                Destroy(gameObject);
            }
             if (Other.CompareTag("Collider 2"))
            {
                Destroy(other);
            }
    
}
}
