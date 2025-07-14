using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : Level
{
    [Header("Settings")]
    [SerializeField] private float requiredStarCount;
    private bool canDrop;
    private GameManager gameManager;

    public override void UnlockLevel(GameObject _go)
    {
        base.UnlockLevel(_go);
    }

    public override void EnableDrop(bool _enable)
    {
        canDrop = _enable;
    }

    public override void Update()
    {
        CheckStarCount();
        if (!canDrop)
        {
            return;
        }
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected override void Start()
    {
        base.Start();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    protected override IEnumerator StopSparkle(GameObject _go)
    {
        return base.StopSparkle(_go);
    }

    protected override void TriggerDrop()
    {
        base.TriggerDrop();
    }

    private void CheckStarCount()
    {
        if(gameManager.stars >= requiredStarCount)
        {
            Invoke(nameof(DelayLevelCompleted), .5f);
        }
    }

    private void DelayLevelCompleted()
    {
        gameManager.CompleteLevel();
    }
}
