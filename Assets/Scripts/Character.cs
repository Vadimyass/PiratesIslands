using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector3 _nextIsland;
    private Vector3 _recentIsland;

    private Animator _animator;
    public bool EbalVRot;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        EbalVRot = false;
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
            island.enabled = false;
        }
    }
    public IEnumerator MoveToPosition(Vector3 positionToMove)
    {
        while(transform.position != positionToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, 0.01f);
            yield return null;
        }
        transform.LookAt(new Vector3(_nextIsland.x,transform.position.y, _nextIsland.z));
        if(EbalVRot == true)
        {
            PlayerManager.instance.OtherCharacterWalking();
            yield break;
        }
    }

    public IEnumerator MoveToCenterRecentIsland(Vector3 pos)
    {
        while (transform.position != _recentIsland)
        {
            transform.position = Vector3.MoveTowards(transform.position, _recentIsland, 0.01f);
            yield return null;
        }
        yield return StartCoroutine(MoveToCenterNextIsland(pos));
    }


    public IEnumerator MoveToCenterNextIsland(Vector3 pos)
    {
        if (transform.position != _nextIsland)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextIsland, 0.01f);
            yield return null;
        }
        yield return StartCoroutine(MoveToPosition(pos));
    }
}
