using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reverse2 : MonoBehaviour
{
     [SerializeField] private Animator MyAnimationController;
     private void OnTriggerEnter(Collider  other){
                  if ( SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level08"))
            {
                if (other.CompareTag("Collider 1"))
            {
                 MyAnimationController.Play("Rev6Rotate");
            }
            }
                   if ( SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level05"))
            {
                if (other.CompareTag("Collider 5"))
            {
                 MyAnimationController.Play("Reverse02");
            }
            }
              if ( SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level07"))
            {
                if (other.CompareTag("Collider 4"))
            {
                 MyAnimationController.Play("Rev03Level07");
            }
            }
              if ( SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level06"))
            {
                if (other.CompareTag("Collider 5"))
            {
                 MyAnimationController.Play("Reverse2lvl6");
            }
            }
}
}
