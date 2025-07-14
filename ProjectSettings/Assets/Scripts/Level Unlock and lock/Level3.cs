using System.Collections;
using UnityEngine;

public class Level3 : Level
{
    [Header("Settings")]
    private bool canDrop;

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
        if(!canDrop)
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
    }

    protected override IEnumerator StopSparkle(GameObject _go)
    {
        return base.StopSparkle(_go);
    }

    protected override void TriggerDrop()
    {
        base.TriggerDrop();
    }
}
