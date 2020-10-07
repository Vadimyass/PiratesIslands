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
        int digitsAccuracy = 6;
        print(positionToMove);
        if (Math.Round(transform.position.z, digitsAccuracy) != Math.Round(positionToMove.z, digitsAccuracy) && Math.Round(transform.position.x, digitsAccuracy) != Math.Round(positionToMove.x, digitsAccuracy))
        {
            print(positionToMove);
            while (Math.Round(transform.position.z, digitsAccuracy) != Math.Round(positionToMove.z, digitsAccuracy) && Math.Round(transform.position.x, digitsAccuracy) != Math.Round(positionToMove.x, digitsAccuracy))
            {

                transform.position = Vector3.MoveTowards(transform.position, positionToMove, 0.012f);
                transform.LookAt(new Vector3(positionToMove.x, transform.position.y, positionToMove.z));
                yield return null;
            }
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
        while (Math.Round(transform.position.x,8) != Math.Round(_recentIsland.x,3) && Math.Round(transform.position.z,3) != Math.Round(_recentIsland.z,3))
        {
            if (localTime >= delay)
            {
                transform.position = Vector3.MoveTowards(transform.position, _recentIsland, 0.012f);
                transform.LookAt(new Vector3(_recentIsland.x, transform.position.y, _recentIsland.z));
                yield return null;
            }
            else
            {
                localTime += Time.deltaTime; ;
                yield return null;
            }
        }
        localTime = 0;
        StartCoroutine(MoveToPosition(pos));
    }

    /*
    public IEnumerator MoveToCenterNextIsland(Vector3 pos)
    {
        while (Math.Round(transform.position.x, 0) != Math.Round(IslandPos.x, 0) && Math.Round(transform.position.z, 0) != Math.Round(IslandPos.z, 0))
        {
            _animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, IslandPos, 0.015f);
            transform.LookAt(new Vector3(IslandPos.x, transform.position.y, IslandPos.z));
            yield return null;
        }
        StartCoroutine(MoveToPosition(pos));
    }*/
}
