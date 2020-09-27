using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector3 _nextIsland;
    private Vector3 _recentIsland;

    public Animator _animator;
    public bool IsGeneral;
    public GameObject camera;
    float localTime;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsGeneral = false;
        localTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Island island))
        {
            _recentIsland = new Vector3(island.transform.position.x, 0.5065f, island.transform.position.z);
            _nextIsland = island.NextIsland.transform.position;
            island.enabled = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Island island))
        {
            Island._instantiatedLog = null;
            island.enabled = false;
        }
    }

    private void Update()
    {

    }
    public IEnumerator MoveToPosition(Vector3 positionToMove)
    {
        while(transform.position != positionToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, 0.01f);
            yield return null;
        }
        transform.LookAt(new Vector3(_nextIsland.x,transform.position.y, _nextIsland.z));
        _animator.SetBool("IsWalking", false);
        if (IsGeneral == true)
        {
            PlayerManager.instance.OtherCharacterWalking();
            yield break;
        }
    }

    public IEnumerator MoveToCenterRecentIsland(Vector3 pos, float delay)
    {
        while (transform.position != _recentIsland)
        {
            if(localTime >= delay)
            {
                transform.position = Vector3.MoveTowards(transform.position, _recentIsland, 0.01f);
                transform.LookAt(_recentIsland);
                yield return null;
            }
            else
            {
                localTime += Time.deltaTime;
                print(localTime);
                yield return null;
            }
        }
        localTime = 0;
        StartCoroutine(MoveToCenterNextIsland(pos));
    }


    public IEnumerator MoveToCenterNextIsland(Vector3 pos)
    {
        if (transform.position != _nextIsland)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextIsland, 0.01f);
            transform.LookAt(_nextIsland);
            yield return null;
        }
        StartCoroutine(MoveToPosition(pos));
    }
}
