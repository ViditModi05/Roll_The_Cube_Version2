using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameManager gamemanager;

    public int nextSceneload;
    void Start()
    {
        nextSceneload = SceneManager.GetActiveScene().buildIndex + 1;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gamemanager.CompleteLevel();

        }
        else
        {
            Debug.Log("Not Collided " + other.gameObject.name);
        }
    }
}
