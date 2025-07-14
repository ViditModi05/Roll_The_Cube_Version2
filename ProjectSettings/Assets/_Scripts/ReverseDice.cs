using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReverseDice : MonoBehaviour
{
     [SerializeField] private Animator MyAnimationController;
     private void OnTriggerEnter(Collider  other){
              if (other.CompareTag("Collider 6"))
            {
                MyAnimationController.Play("Reverse01");
            }
              if (other.CompareTag("Collider 3"))
            {
                MyAnimationController.Play("Reverse04");
            }
                if (other.CompareTag("Collider 5"))
            {
                MyAnimationController.Play("Reverse02");
            }
              if ( SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level07"))
            {
                if (other.CompareTag("Collider 3"))
            {
                 MyAnimationController.Play("Rev04Level07");
            }
            }
               if ( SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level08"))
            {
                if (other.CompareTag("Collider 2"))
            {
                 MyAnimationController.Play("Rev5Rotate");
            }
            }
     }
}
