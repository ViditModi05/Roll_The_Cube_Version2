using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAnim : MonoBehaviour
{
    [SerializeField] private Animator MyAnimationController;

        private void OnTriggerEnter(Collider  other)
        {
            if (other.CompareTag("Collider 6"))
            {
                MyAnimationController.SetBool("GroundAnim", true);
            }
            if (other.CompareTag("Collider 4"))
            {
                MyAnimationController.Play("02AnimGro");
            }
             if (other.CompareTag("Collider 5"))
            {
                MyAnimationController.Play("03AnimGro");
            }
             if (other.CompareTag("Collider 3"))
            {
                MyAnimationController.Play("04AnimGro");
            }
              if (other.CompareTag("Collider 1"))
            {
                MyAnimationController.Play("06AnimGro");
            }
             if (other.CompareTag("Collider 2"))
            {
                MyAnimationController.Play("05AnimGro");
            }

        }
}
