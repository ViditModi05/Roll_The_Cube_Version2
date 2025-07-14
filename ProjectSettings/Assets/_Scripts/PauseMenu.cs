using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool Gameispaused = false;
    public GameObject pausemenuui;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Gameispaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void resume()
    {
        pausemenuui.SetActive(false);
        Time.timeScale = 1f;
        Gameispaused = false;
    }
    void pause()
    {
        pausemenuui.SetActive(true);
        Time.timeScale = 0f;
        Gameispaused = true;
    }

    public void LoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");

    }
    public void Quitgame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

