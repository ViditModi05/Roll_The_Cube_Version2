using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement {  get; private set; }

    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }
}
