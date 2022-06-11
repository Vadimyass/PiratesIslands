using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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
    }

    private void Awake()
    {
        SpawnIslands();
        SetupIslands();
    }

    private void SpawnIslands()
    {
        _islandSpawnPoint = _startIslandSpawnPoint.position;
        for (int i = 0; i < level+5; i++)
        {
            _islands.Add(_islandPool.GetNextIsland(IslandReferenceData.IslandType.Normal, _islandSpawnPoint));
            var distance = Random.Range(2.0f, 3.0f);
            var side = Random.Range(1, 4);
            switch (side)
            {
                case 1:
                    _islandSpawnPoint += (_islands[i].transform.right * -distance);
                    break;
                case 2:
                    _islandSpawnPoint += (_islands[i].transform.forward * distance);
                    break;
                case 3:
                    _islandSpawnPoint += (_islands[i].transform.right * distance);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetupIslands()
    {
        for (int i = 1; i < level+5; i++)
        {
            Vector3 newDir = Vector3.RotateTowards(_islands[i-1].transform.position, (_islands[i].transform.position-_islands[i-1].transform.position), 8, 0.0F);
            _islands[i-1].transform.rotation = Quaternion.LookRotation(newDir);
            _islands[i-1].transform.Rotate(0, 90, 0);
            _islands[i - 1].NextIsland = _islands[i];
            _islands[i].enabled = false;
        } 
    }
}
