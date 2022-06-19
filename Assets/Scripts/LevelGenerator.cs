using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    private List<Island> _islands = new List<Island>();
    private List<Character> _characters = new List<Character>();
    [SerializeField] private List<Transform> _charsSpawnPoints = new List<Transform>();
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Transform _startIslandSpawnPoint;

    private PlayerManager _playerManager;
    private Transform _islandSpawnPoint;
    private IslandPool _islandPool;
    private CharacterPool _characterPool;
    [SerializeField] private int level;
    private int[,] _islandMatrix = new int[10, 10];
    private int x;
    private int y;

    [Inject]
    public void Construct(IslandPool islandPool, CharacterPool characterPool, PlayerManager playerManager)
    {
        _islandPool = islandPool;
        _characterPool = characterPool;
        _playerManager = playerManager;
    }

    private void Awake()
    {
        SpawnIslands();
        SetupIslands();
        SpawnCharacters();
    }

    private void SpawnCharacters()
    {
        _characters.Add(_characterPool.GetNextCharacter(CharacterReferenceData.CharacterType.Captain, _charsSpawnPoints[0].position));
        _characters.Add(_characterPool.GetNextCharacter(CharacterReferenceData.CharacterType.Sailor, _charsSpawnPoints[1].position));
        _characters.Add(_characterPool.GetNextCharacter(CharacterReferenceData.CharacterType.Sailor2, _charsSpawnPoints[2].position));
        foreach (var character in _characters)
        {
            character._recentIslandRef = _islands[0];
            character._nextIsland = new Vector3(_islands[1].transform.position.x, 0.24f,_islands[1].transform.position.z);
        }
        PlayerManager.instance.SetCharacters(_characters);
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
            _spawnPointsParent.transform.rotation = Quaternion.LookRotation(newDir);
            _islands[i-1].transform.Rotate(0, 90, 0);
            _spawnPointsParent.transform.Rotate(0,90,0);
            _islands[i - 1].NextIsland = _islands[i];
            _islands[i].enabled = false;
        }
    }
}
