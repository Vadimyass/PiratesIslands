using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
        Vector3 newDir = Vector3.RotateTowards(transform.forward, (_nextIsland-transform.position), 8, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
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
        if (Math.Round(transform.position.z, digitsAccuracy) != Math.Round(positionToMove.z, digitsAccuracy) && Math.Round(transform.position.x, digitsAccuracy) != Math.Round(positionToMove.x, digitsAccuracy))
        {
            while (Math.Round(transform.position.z, digitsAccuracy) != Math.Round(positionToMove.z, digitsAccuracy) && Math.Round(transform.position.x, digitsAccuracy) != Math.Round(positionToMove.x, digitsAccuracy))
            {
                _animator.SetBool("IsWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, positionToMove, 0.012f);
                transform.LookAt(new Vector3(positionToMove.x, transform.position.y, positionToMove.z));
                yield return new WaitForFixedUpdate();
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
                _animator.SetBool("IsWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, _recentIsland, 0.012f);
                transform.LookAt(new Vector3(_recentIsland.x, transform.position.y, _recentIsland.z));
                yield return new WaitForFixedUpdate();
            }
            else
            {
                localTime += Time.deltaTime; ;
                yield return new WaitForFixedUpdate();
            }
        }
        localTime = 0;
        StartCoroutine(MoveToPosition(pos));
    }
}
