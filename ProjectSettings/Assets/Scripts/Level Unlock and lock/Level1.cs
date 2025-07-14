using System;
using System.Collections;
using UnityEngine;

public class Level1 : Level
{
    [Header("Settings")]
    [SerializeField] private float pauseTime;
    protected override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();

    }


    public override void UnlockLevel(GameObject _go)
    {
        base.UnlockLevel(_go);

    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected override IEnumerator StopSparkle(GameObject _go)
    {
        return base.StopSparkle(_go);
    }

    protected override void TriggerDrop()
    {
        if (spiders != null && spiders.Count > 0)
        {
            spiders[0].DropSpider(true);
            spiders.RemoveAt(0);
            StartCoroutine(PauseLevel());
            CameraTransitions cameraTransitions = Camera.main.GetComponent<CameraTransitions>();
            cameraTransitions.FocusOnSpider();
        }
    }
    private IEnumerator PauseLevel()
    {
        PlayerManager.instance.playerMovement.IsMovementPaused(true);
        yield return new WaitForSeconds(pauseTime);
        PlayerManager.instance.playerMovement.IsMovementPaused(false);
    }
}
