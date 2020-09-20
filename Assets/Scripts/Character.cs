using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool TimeToGo;
    private GameObject _nextIsland;
    private Vector3 _nextIslandTarget;

    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();

        TimeToGo = false;
        Island.LogDown += WalkToNextIsland;
    }

    public void WalkToNextIsland(GameObject nextIsland)
    {
        _nextIslandTarget = new Vector3(_nextIsland.transform.position.x, 0.5065f, _nextIsland.transform.position.z);
        print(_nextIsland.name);
        TimeToGo = true;
    }
    private void FixedUpdate()
    {
        if (TimeToGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextIslandTarget,0.01f);
        }
        if(transform.position == _nextIslandTarget && TimeToGo == true)
        {
            TimeToGo = false;
            transform.LookAt(_nextIsland.transform.position);
            print("dada");
        }

        _animator.SetBool("IsWalking", TimeToGo);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Island island))
        {
            _nextIsland = island.NextIsland;
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
