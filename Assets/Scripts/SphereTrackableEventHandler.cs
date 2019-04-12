using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTrackableEventHandler : DefaultTrackableEventHandler
{
    public static event Action<SphereTrackableEventHandler> OnInstantiate;
    public event Action OnFound = delegate { };
    public event Action OnLost = delegate { };
    
    protected override void Start()
    {
        base.Start();
        if (!gameObject.name.Equals("UserDefinedTarget"))
        OnInstantiate.Invoke(this);
    }

    protected override void OnTrackingFound()
    {        
        base.OnTrackingFound();
        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (meshRenderer != null)
            meshRenderer.material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        OnFound();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        OnLost();
    }
}
