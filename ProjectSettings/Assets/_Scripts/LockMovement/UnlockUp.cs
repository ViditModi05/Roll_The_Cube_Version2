using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockUp : MonoBehaviour
{
                  public Move move;
      private void OnTriggerEnter(Collider  other)
        {
            if (other.CompareTag("Player"))
            {
              move.UnlockUp = true;
            }
}
}
