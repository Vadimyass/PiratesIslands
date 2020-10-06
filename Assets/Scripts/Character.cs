using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [NonSerialized] public Vector3 _nextIsland;
    [NonSerialized] public Vector3 _recentIsland;
    [NonSerialized] public Island _recentIslandRef;

    [NonSerialized] public Animator _animator;
    [NonSerialized] public bool IsGeneral;
    public GameObject camera;
    [NonSerialized] private float localTime;

    [SerializeField] public Transform target;
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
            if (!island.enabled)
            {
                island.enabled = true;
            }
            _recentIsland = new Vector3(island.transform.position.x, 0.24f, island.transform.position.z);
            _nextIsland = new Vector3(island.NextIsland.transform.position.x, 0.24f, island.NextIsland.transform.position.z);
            _recentIslandRef = island;
        }
        //else if(collision.collider.TryGetComponent(out WaterTrigger water))
        //{
        //    Island._instantiatedLog.SetActive(false);
        //    PlayerManager.instance.AppointGeneralCharacter();
        //    Destroy(this.gameObject);
        //}
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Island island))
        {
            if (island.enabled)
            {
                island._instantiatedLog = null;
                island.enabled = false;
            }
        }
    }
    public IEnumerator MoveToPosition(Vector3 positionToMove)
    {
        while(Math.Round(transform.position.x, 3) != Math.Round(positionToMove.x, 3) && Math.Round(transform.position.z, 3) != Math.Round(positionToMove.z, 3))
        {
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, 0.012f);
            transform.LookAt(new Vector3(positionToMove.x, transform.position.y, positionToMove.z));
            yield return null;
        }
        transform.LookAt(new Vector3(_nextIsland.x, transform.position.y, _nextIsland.z));
        _animator.SetBool("IsWalking", false);
        if (IsGeneral == true)
        {
            PlayerManager.instance.OtherCharacterWalking();
            yield break;
        }
    }

    public IEnumerator MoveToCenterRecentIsland(Vector3 pos, float delay)
    {
        while (Math.Round(transform.position.x, 3) != Math.Round(_recentIsland.x, 3) && Math.Round(transform.position.z, 3) != Math.Round(_recentIsland.z, 3))
        {
            if(localTime >= delay)
            {
                transform.position = Vector3.MoveTowards(transform.position, _recentIsland, 0.012f);
                transform.LookAt(new Vector3(_recentIsland.x, transform.position.y, _recentIsland.z));
                yield return null;
            }
            else
            {
                localTime += Time.deltaTime;;
                yield return null;
            }
        }
        localTime = 0;
        StartCoroutine(MoveToCenterNextIsland(pos));
    }


    public IEnumerator MoveToCenterNextIsland(Vector3 pos)
    {
        transform.LookAt(new Vector3(_nextIsland.x, transform.position.y, _nextIsland.z));
        if (Math.Round(transform.position.x, 3) != Math.Round(_nextIsland.x,3) && Math.Round(transform.position.z, 3) != Math.Round(_nextIsland.z, 3))
        {
            _animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, _nextIsland, 0.012f);
            yield return null;
        }
        StartCoroutine(MoveToPosition(pos));
    }
}
