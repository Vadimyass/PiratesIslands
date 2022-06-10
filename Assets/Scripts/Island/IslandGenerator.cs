using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField] private List<Island> _islands;
    
    [SerializeField] private Transform _startIslandSpawnPoint;
    private Vector3 _islandSpawnPoint;
    private IslandPool _islandPool;
    [SerializeField] private int level;
    [Inject]
    public void Construct(IslandPool islandPool)
    {
        _islandPool = islandPool;
        _islandSpawnPoint = _startIslandSpawnPoint.position;
        for (int i = 0; i < level+5; i++)
        {
            Debug.Log("Try Try to create Island");
            _islands.Add(_islandPool.GetNextIsland(IslandReferenceData.IslandType.Normal, _islandSpawnPoint));
            _islandSpawnPoint = new Vector3(_islandSpawnPoint.x-2, _islandSpawnPoint.y, _islandSpawnPoint.z);
        }
        for (int i = 1; i < level+5; i++)
        {
            _islands[i - 1].NextIsland = _islands[i];
            _islands[i].enabled = false;
        }
    }
}
