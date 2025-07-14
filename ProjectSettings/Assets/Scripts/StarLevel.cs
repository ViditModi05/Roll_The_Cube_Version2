using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLevel : Star
{
    [Header("Refs")]
    private Level level;
    [Header("Settings")]
    [SerializeField] private bool triggerDrop;

    private void Awake()
    {
        level = FindFirstObjectByType<Level>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        level.EnableDrop(triggerDrop);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
