using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceans : MonoBehaviour
{
   public void Playgame()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   public void Quitgame()
   {
       Debug.Log("Quit!");
       Application.Quit();
   }
     public void LoadScene()
       {
           Time.timeScale = 1f;
           SceneManager.LoadScene("Menu");

       }
          public void Select (string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
}

