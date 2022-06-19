using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class PlankPool : MonoBehaviour
{
    private PlankFactory _plankFactory;
    public List<WoddenPlank> ActivePlanks => _activePlanks;
    private readonly List<WoddenPlank> _activePlanks = new List<WoddenPlank>();

    [Inject]
    public void Construct(PlankFactory plankFactory)
    {
        _plankFactory = plankFactory;
        Debug.Log(plankFactory);
    }

    public WoddenPlank GetNextPlank(PlankReferenceData.PlankType plankType,Transform targetTransform)
    {
        var plank = _plankFactory.Create(plankType, targetTransform);
        _activePlanks.Add(plank);
        return plank;
    }
}
