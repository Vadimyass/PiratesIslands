using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlankConfig", menuName = "Installers/PlankConfig")]
public class PlankConfig : ScriptableObject
{
    [SerializeField] private List<PlankReferenceData> _plankReferenceData;
    public GameObject GetPlankConfig(PlankReferenceData.PlankType plankType)
    {
        foreach (var plankData in _plankReferenceData)
        {
            if (plankData.plankType == plankType)
            {
                return plankData.prefab;
            }
        }
        return null;
    }
}
[Serializable]
public struct PlankReferenceData
{
    public GameObject prefab;
    public PlankType plankType;
    public enum PlankType
    {
        Wood
    }
}