using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField] private List<Island> _islands;
    
    [SerializeField] private Transform _startIslandSpawnPoint;
    private Transform _islandSpawnPoint;
    private IslandPool _islandPool;
    [SerializeField] private int level;
    private NavMeshSurface _surface;
    private int[,] _islandMatrix = new int[10, 10];
    private int x;
    private int y;

    [Inject]
    public void Construct(IslandPool islandPool, NavMeshSurface surface)
    {
        _islandPool = islandPool;
        _surface = surface;
    }

    private void Awake()
    {
        SpawnIslands();
        SetupIslands();
    }

    private void SpawnIslands()
    {
        _islandSpawnPoint = _startIslandSpawnPoint;
        x = 4; 
        y = 9;
        _islandMatrix[x, y] = 1;
        for (int i = 0; i < level+5; i++)
        {
            _islands.Add(_islandPool.GetNextIsland(IslandReferenceData.IslandType.Normal, _islandSpawnPoint.position));
            MoveIslandSpawnPoint();
        }
        Debug.Log(_islandMatrix);
    }

    private void MoveIslandSpawnPoint()
    {
        var distance = Random.Range(3.0f, 4.0f);
        var side = Random.Range(1, 5);
        switch (side)
        {
            //left
            case 1:
                if (x!=0 && _islandMatrix[x - 1, y] != 1)
                {
                    _islandMatrix[x - 1, y] = 1;
                    x -= 1;
                    _islandSpawnPoint.position += Vector3.left * distance;
                }
                else
                {
                    MoveIslandSpawnPoint();
                }
                break;
            //right
            case 2:
                if (x!=9 && _islandMatrix[x + 1, y] != 1)
                {
                    _islandMatrix[x + 1, y] = 1;
                    x += 1;
                    _islandSpawnPoint.position += Vector3.right * distance;
                }
                else
                {
                    MoveIslandSpawnPoint();
                }
                break;
            //forward
            case 3:
                if (y!=0 && _islandMatrix[x, y - 1] != 1)
                {
                    _islandMatrix[x, y - 1] = 1;
                    y -= 1;
                    _islandSpawnPoint.position += Vector3.forward * distance;
                }
                else
                {
                    MoveIslandSpawnPoint();
                }
                break;
            //back
            case 4:
                if (y!=9 && _islandMatrix[x, y + 1] != 1 )
                {
                    _islandMatrix[x, y + 1] = 1;
                    y += 1;
                    _islandSpawnPoint.position += Vector3.back * distance;
                }
                else
                {
                    MoveIslandSpawnPoint();
                } 
                break;
            default:
                break;
        }
    }
    private void SetupIslands()
    {
        for (int i = 1; i < _islands.Count; i++)
        {
            Vector3 newDir = Vector3.RotateTowards(_islands[i-1].transform.position, (_islands[i].transform.position-_islands[i-1].transform.position), 360, 0.0F);
            _islands[i-1].transform.rotation = Quaternion.LookRotation(newDir);
            _islands[i-1].transform.Rotate(0, 90, 0);
            _islands[i - 1].NextIsland = _islands[i];
            _islands[i].enabled = false;
        }
    }
}
