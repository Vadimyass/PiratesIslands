using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

public class PlankFactory
{
    private DiContainer _container;
    private PlankConfig _plankConfig;

    public PlankFactory(DiContainer container, PlankConfig plankConfig)
    {
        Debug.Log(plankConfig);
        _container = container;
        _plankConfig = plankConfig;
    }

    public WoddenPlank Create(PlankReferenceData.PlankType plankType,Transform parent)
    {
        Debug.Log("Create Plank");
        var reference = _plankConfig.GetPlankConfig(plankType);
        return _container.InstantiatePrefab(reference, parent).GetComponent<WoddenPlank>();
    }
}
