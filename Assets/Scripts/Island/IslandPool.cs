using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IslandPool : MonoBehaviour
{
    private IslandFactory _islandFactory;
    public List<Island> ActiveIslands => _activeIslands;
    private readonly List<Island> _activeIslands = new List<Island>();

    [Inject]
    public void Construct(IslandFactory islandFactory)
    {
        Debug.Log("Island Pool Constructed");
        _islandFactory = islandFactory;
        Debug.Log(islandFactory);
    }

    public Island GetNextIsland(IslandReferenceData.IslandType islandType,Vector3 targetPostion)
    {
        Debug.Log("Try to create Island");
        var island = _islandFactory.Create(islandType, targetPostion);
        _activeIslands.Add(island);
        return island;
    }
}
