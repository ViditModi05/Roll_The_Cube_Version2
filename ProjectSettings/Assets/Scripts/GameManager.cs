using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject completeLevelUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Animator unlockAnimator;
    [SerializeField] private GameObject[] starsUI;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI levelText;
    public float stars {  get; private set; }

    [Header("LeveltoLoad")]
    [SerializeField] private int levelNumber;
    private int currentLevel;
    private int nextLevel;
    private string currentLevelname;
    private string nextLevelname;
    public bool levelCompleted { get; private set; }

    [Header("Actions")]
    public System.Action starsCollected;
    public System.Action cubeMovement;

    [Header("Settings")]
    private int totalStars;
    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 150, 40), "FPS: " + (1f / Time.unscaledDeltaTime).ToString("F1"), style);
    }

    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        currentLevelname = SceneManager.GetActiveScene().name;
        nextLevel = currentLevel + 1;
        nextLevelname = SceneManager.GetActiveScene().name;
        if(warningText != null) 
            warningText.gameObject.SetActive(false);
        levelText.text = "Level " + levelNumber;
    }   
    public void CompleteLevel()
    {
        Debug.Log("Level Completed");
        completeLevelUI.SetActive(true);

        for (int i = 0; i < stars; i++)
        {
            starsUI[i].SetActive(true);
        }

        statusText.text = "Victory!";
        levelCompleted = true;

        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        if ((stars == 3 || levelReached >= levelNumber + 1) && nextLevelButton != null)
        {
            nextLevelButton.interactable = true;
        }
        else if (nextLevelButton != null)
        {
            //nextLevelButton.interactable = false;
        }

    }

    private void SavePartialProgress()
    {
        string key = $"Stars_Level_{currentLevelname}";
        int previousStars = PlayerPrefs.GetInt(key, 0);

        if (stars > previousStars)
        {
            PlayerPrefs.SetInt(key, (int)stars);

            int totalStars = PlayerPrefs.GetInt("TotalStars", 0);
            totalStars += (int)(stars - previousStars);
            PlayerPrefs.SetInt("TotalStars", totalStars);

            Debug.Log($"[GameOver] Partial progress saved: {stars} stars");
        }

        PlayerPrefs.Save();
    }

    public void AddStars()
    {
        stars++;

        if (starsCollected != null)
        {
            starsCollected();
        }

        if (stars == 3)
        {
            LevelUnlock();
            CompleteLevel();
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        statusText.text = "Game Over!";
        levelCompleted = true;
        for (int i = 0; i < stars; i++)
        {
            starsUI[i].SetActive(true);
        }
        SavePartialProgress();

    }

    public void PLayFX(GameObject _go)
    {
        Level level = GetComponent<Level>();
        level.UnlockLevel(_go);

    }

    private void LevelUnlock()
    {
        Level level = GetComponent<Level>();
        if(unlockAnimator != null)
        {
            unlockAnimator.SetBool("GroundAnim", true);
        }
        SaveLevel();
    }

    public void PlayeMovementAudio()
    {
        if(cubeMovement != null)
        {
            cubeMovement();
        }
    }


    private void SaveLevel()
    {
        Debug.Log($"Saving Level. Stars: {stars}, levelNumber: {levelNumber}");
        // Only unlock the next level if player collected all 3 stars
        if (stars == 3 && (levelNumber + 1) > PlayerPrefs.GetInt("LevelReached", 1))
        {
            PlayerPrefs.SetInt("LevelReached", levelNumber + 1);
            Debug.Log($"LevelReached set to {levelNumber + 1}");
        }

        // Save stars for this level regardless
        string key = $"Stars_Level_{currentLevelname}";
        int previousStars = PlayerPrefs.GetInt(key, 0);

        if (stars > previousStars)
        {
            PlayerPrefs.SetInt(key, (int)stars);

            // Update total stars properly
            int totalStars = PlayerPrefs.GetInt("TotalStars", 0);
            totalStars += (int)(stars - previousStars); // Only add the difference
            PlayerPrefs.SetInt("TotalStars", totalStars);

            Debug.Log($"Stars updated for {currentLevelname}: {stars}");
        }

        PlayerPrefs.Save();
    }

    public int GetTotalStars()
    {
        return PlayerPrefs.GetInt("TotalStars", 0);
    }

    public void LoadNextScene()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (stars == 3 || levelReached > levelNumber)
        {
            Debug.Log(levelReached);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            ShowText();
        }
    }

    private void ShowText()
    {
        if(warningText != null && !warningText.gameObject.activeInHierarchy)
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "You Don't Have Enough Stars";
            Invoke(nameof(HideText), 2f);
        }

    }

    private void HideText()
    {
        warningText.gameObject.SetActive(false);
    }
}
