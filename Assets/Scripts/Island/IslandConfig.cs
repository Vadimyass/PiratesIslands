using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IslandConfig", menuName = "Installers/IslandConfig")]
public class IslandConfig : ScriptableObject
{
    [SerializeField] private List<IslandReferenceData> _islandReferenceData;
    public GameObject GetIslandConfig(IslandReferenceData.IslandType islandType)
    {
        foreach (var islandData in _islandReferenceData)
        {
            if (islandData.islandType == islandType)
            {
                return islandData.prefab;
            }
        }
        return null;
    }
}
[Serializable]
public struct IslandReferenceData
{
    public GameObject prefab;
    public IslandType islandType;
    public enum IslandType
    {
        Normal
    }
}