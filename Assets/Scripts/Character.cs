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
            _recentIsland = new Vector3(island.transform.position.x, 0.5065f, island.transform.position.z);
            _nextIsland = new Vector3(island.NextIsland.transform.position.x, 0.5065f, island.NextIsland.transform.position.z);
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
        while(transform.position != positionToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, 0.012f);
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
                transform.position = Vector3.MoveTowards(transform.position, _recentIsland, 0.012f);
                transform.LookAt(_recentIsland);
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
        if (transform.position != _nextIsland)
        {
            _animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, _nextIsland, 0.012f);
            transform.LookAt(_nextIsland);
            yield return null;
        }
        StartCoroutine(MoveToPosition(pos));
    }
}
