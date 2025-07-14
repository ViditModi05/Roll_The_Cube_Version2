using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [Header("Star Sprites")]
    [SerializeField] private Sprite[] starSprites;
    [SerializeField] private Image starImage;

    public void SetStars(int count)
    {
        count = Mathf.Clamp(count, 0, 3); // safety check
        if (starImage != null && starSprites.Length >= 4)
        {
            Debug.Log($"{gameObject.name} → Setting {count} stars");
            starImage.sprite = starSprites[count];
            Debug.Log("Count: " + count);
        }
        else
        {
            Debug.LogWarning("StarSprites or StarImage not assigned in LevelButtonUI.");
        }
    }
}
