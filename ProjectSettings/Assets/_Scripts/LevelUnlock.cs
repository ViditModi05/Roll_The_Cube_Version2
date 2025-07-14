using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlock : MonoBehaviour
{
    public int nextSceneload;
    void Start()
    {
        nextSceneload = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nextSceneload);

            if(nextSceneload > PlayerPrefs.GetInt("LevelReached"))
            {
                PlayerPrefs.SetInt("LevelReached", nextSceneload);
            }

        }
    }
}
