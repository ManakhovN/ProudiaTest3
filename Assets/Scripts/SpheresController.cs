using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresController : MonoBehaviour {
    List<SphereTrackableEventHandler> spheresTrackers = new List<SphereTrackableEventHandler>();
    List<SphereTrackableEventHandler> activeSpheres = new List<SphereTrackableEventHandler>();
	void Awake () {
        SphereTrackableEventHandler.OnInstantiate += SphereAdded;	
	}

    private void SphereAdded(SphereTrackableEventHandler sphere)
    {
        spheresTrackers.Add(sphere);
        sphere.OnFound += () =>activeSpheres.Add(sphere);
        sphere.OnLost += () => activeSpheres.Remove(sphere);
    }

    private float timer = 0f;
    [SerializeField] float speed = 10f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            timer -= 2f;
            MoveSpheres();
        }
    }

    private void MoveSpheres() {
        for (int i = 0; i < activeSpheres.Count; i++)
        {
            Transform go = activeSpheres[i].transform.GetChild(0);
            go.SetParent(activeSpheres[(i + 1) % activeSpheres.Count].transform);
            StartCoroutine(MovePositionTo(go, Vector3.zero, speed));
        }
    }

    IEnumerator MovePositionTo(Transform obj, Vector3 destination, float speed)
    {
        while (!obj.localPosition.Equals(destination))
        {
            obj.localPosition = Vector3.MoveTowards(obj.localPosition, destination, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
