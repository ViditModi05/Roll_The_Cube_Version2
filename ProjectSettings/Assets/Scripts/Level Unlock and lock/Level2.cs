using System.Collections;
using UnityEngine;

public class Level2 : Level
{
    public override void UnlockLevel(GameObject _go)
    {
        base.UnlockLevel(_go);
    }

    public override void Update()
    {
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
