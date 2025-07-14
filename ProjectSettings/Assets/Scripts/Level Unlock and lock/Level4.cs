using System.Collections;
using UnityEngine;

public class Level4 : Level
{
    [Header("Mid Game Unlocker")]
    [SerializeField] private Animator animator;
    private bool canUnlock;
    public override void UnlockLevel(GameObject _go)
    {
        base.UnlockLevel(_go);
    }

    public override void Update()
    {
        base.Update();

        if(canUnlock)
        {
            AnimatorPlay();
        }
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

    public void AnimatorPlay()
    {
        if(animator != null)
        {
            animator.SetBool("GroundAnim", true);   

        }
    }

    public override void EnableDrop(bool _enable)
    {
        canUnlock = _enable;
    }
}
