using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Character : MonoBehaviour
{
    [NonSerialized] public Vector3 _nextIsland;
    [NonSerialized] public Vector3 _recentIsland;
    [NonSerialized] public Island _recentIslandRef;

    [NonSerialized] public Animator _animator;
    [SerializeField] public bool IsGeneral;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [NonSerialized] private float localTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsGeneral = false;
        localTime = 0;
    }

    public void MoveToNextIsland()
    {
        _navMeshAgent.SetDestination(_nextIsland);
        Debug.DrawLine(_nextIsland, Vector3.up, Color.magenta, Mathf.Infinity);
        Debug.Log(_nextIsland);
        Debug.Log(_navMeshAgent.destination);
    }
}
