using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] protected List<Spider> spiders; 
    protected PlayerMovement player;
    [Header("Settings")]
    [SerializeField] protected float triggerRadius = 3f;
    [SerializeField] protected Transform[] triggerPoint;

    protected virtual void Start()
    {
        player = PlayerManager.instance.playerMovement;
    }
    public virtual void Update()
    {
        if (triggerPoint != null && spiders != null)
        {
            foreach (Transform t in triggerPoint)
            {
                if (Vector3.Distance(t.position, player.transform.position) < triggerRadius)
                {
                    TriggerDrop();
                }
            }
        }


    }

    public virtual void EnableDrop(bool _enable)
    {

    }

    protected virtual void TriggerDrop()
    {
        //trigger the drop of the spider first in the list then remove it from the list

        if (spiders != null && spiders.Count > 0)
        {
            spiders[0].DropSpider(true); 
            spiders.RemoveAt(0);
        }
    }

    public virtual void UnlockLevel(GameObject _go)
    {
        if (_go != null)
        {
            _go.SetActive(true);
        }
        StartCoroutine(StopSparkle(_go));
    }

    protected virtual IEnumerator StopSparkle(GameObject _go)
    {
        yield return new WaitForSeconds(1f);
        Destroy(_go);
    }

    protected virtual void OnDrawGizmos()
    {
        foreach(Transform t in triggerPoint)
        {
            if (triggerPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(t.position, triggerRadius);
            }
        }

    }
}
