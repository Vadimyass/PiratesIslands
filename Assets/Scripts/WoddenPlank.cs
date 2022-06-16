using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Water;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Zenject;

public class WoddenPlank : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;
    private ScoreView _scoreView;
    [SerializeField] private Transform _startPlankPoint;
    [SerializeField] private Transform _endPlankPoint;
    private NavMeshSurface _surface;

    [Inject]
    public void Construct(ScoreView scoreView, NavMeshSurface surface)
    {
        _scoreView = scoreView;
        _surface = surface;
    }
    public IEnumerator CheckOnFalling(Vector3 direction)
    {
        var distance = Vector3.Distance(_startPlankPoint.position, _endPlankPoint.position);
        var checkPoint = _startPlankPoint.position +(direction * distance);
        yield return new WaitForSeconds(2);
        RaycastHit hit;
        if (Physics.Raycast(checkPoint, Vector3.down, out hit, Mathf.Infinity))
        {
            _surface.transform.position = checkPoint;
            _surface.BuildNavMesh();
            if (hit.collider.TryGetComponent(out WaterArea water))
            {
                print("No");
                PlayerManager.instance._generalCharacter._recentIslandRef.enabled = true;
                PlayerManager.instance._generalCharacter.IsGeneral = false;
                PlayerManager.instance._generalCharacter.MoveToNextIsland();
                PlayerManager.instance._generalCharacter.enabled = false;
            }
            else if (hit.collider.TryGetComponent(out Island island))
            {
                print("island");
                PlayerManager.instance.WalkToNextIsland(new Vector3(island.transform.position.x,0.24f, island.transform.position.z));
                _scoreView.AddScore(10);
                island.enabled = true;
            }
        }
    }
}
