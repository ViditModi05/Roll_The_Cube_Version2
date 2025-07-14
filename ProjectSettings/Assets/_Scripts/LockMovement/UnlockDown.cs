using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDown : MonoBehaviour
{
                   public Move move;
      private void OnTriggerEnter(Collider  other)
        {
            if (other.CompareTag("Player"))
            {
              move.UnlockDown = true;
            }
}
}
