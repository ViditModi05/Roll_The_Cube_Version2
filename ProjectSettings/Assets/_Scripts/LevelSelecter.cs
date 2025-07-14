using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private string[] levelSceneNames;
    public Button[] LevelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        Debug.Log($"Level Reached: {levelReached})");

        for (int i = 0; i < LevelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            string sceneName = levelSceneNames[i];
            int stars = PlayerPrefs.GetInt($"Stars_Level_{sceneName}", 0);
            Debug.Log(stars);
            bool isUnlocked = (i == 0) || (PlayerPrefs.GetInt($"Stars_Level_{levelSceneNames[i - 1]}", 0) == 3);    

            LevelButtons[i].interactable = isUnlocked;

            Lock lockIcon = LevelButtons[i].GetComponentInChildren<Lock>(true);
            if (lockIcon != null)
            {
                lockIcon.gameObject.SetActive(!isUnlocked); 
            }

            LevelButtonUI levelUI = LevelButtons[i].GetComponent<LevelButtonUI>();
            if (levelUI != null)
                levelUI.SetStars(stars);
            else
                Debug.LogWarning("No Level Button UI");
        }
    }

    public void Select(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
}
