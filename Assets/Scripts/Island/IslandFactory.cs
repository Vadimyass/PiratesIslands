using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IslandFactory
{
    private DiContainer _container;
    private IslandConfig _islandConfig;

    public IslandFactory(DiContainer container, IslandConfig islandConfig)
    {
        Debug.Log(islandConfig);
        _container = container;
        _islandConfig = islandConfig;
    }

    public Island Create(IslandReferenceData.IslandType islandType,Vector3 position )
    {
        Debug.Log("Create Island");
        var reference = _islandConfig.GetIslandConfig(islandType);
        return _container.InstantiatePrefab(reference, position, Quaternion.identity, null).GetComponent<Island>();
    }
}
