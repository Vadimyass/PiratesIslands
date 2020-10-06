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
    [SerializeField] public bool IsGeneral;
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
        //else if(collision.collider.TryGetComponent(out WaterTrigger water))
        //{
        //    Island._instantiatedLog.SetActive(false);
        //    PlayerManager.instance.AppointGeneralCharacter();
        //    Destroy(this.gameObject);
        //}
    }

    public IEnumerator MoveToPosition(Vector3 positionToMove)
    {
        while(transform.position.x != positionToMove.x && transform.position.z != positionToMove.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, 0.015f);
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
        while (transform.position.x != _recentIsland.x && transform.position.z !=  _recentIsland.z)
        {
            if(localTime >= delay)
            {
                if (IsGeneral)
                {
                    print("recent");
                }
                transform.position = Vector3.MoveTowards(transform.position, _recentIsland, 0.015f);
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
        Vector3 IslandPos = _nextIsland;
        while (transform.position.x != IslandPos.x && transform.position.z != IslandPos.z)
        {
            if (IsGeneral)
            {
                print("next");
            }
            _animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, IslandPos, 0.015f);
            transform.LookAt(new Vector3(IslandPos.x, transform.position.y, IslandPos.z));
            yield return null;
        }
        StartCoroutine(MoveToPosition(pos));
    }
}
