using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool TimeToGo;
    private GameObject _nextIsland;
    private void Start()
    {
        TimeToGo = false;
        Island.LogDown += WalkToNextIsland;
    }

    public void WalkToNextIsland(GameObject nextIsland)
    {
        transform.LookAt(nextIsland.transform.position);
        _nextIsland = nextIsland;
        print(_nextIsland.name);
        TimeToGo = true;
    }
    private void Update()
    {
        if (TimeToGo)
        {
            transform.TransformDirection(Vector3.forward);
            transform.localPosition += Vector3.left * Time.deltaTime * 0.5f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out TriggerScore triggerScore))
        {
            TimeToGo = false;
        }
        if(collision.collider.TryGetComponent(out Island island))
        {
            island.enabled = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Island island))
        {
            island.enabled = false;
        }
    }
}
