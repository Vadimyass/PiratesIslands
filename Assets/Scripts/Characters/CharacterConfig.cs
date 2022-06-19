using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Installers/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private List<CharacterReferenceData> _characterReferenceData;
    public GameObject GetCharacterConfig(CharacterReferenceData.CharacterType characterType)
    {
        foreach (var characterData in _characterReferenceData)
        {
            if (characterData.characterType == characterType)
            {
                return characterData.prefab;
            }
        }
        return null;
    }
}
[Serializable]
public struct CharacterReferenceData
{
    public GameObject prefab;
    public CharacterType characterType;
    public enum CharacterType
    {
        Captain,
        Sailor,
        Sailor2
    }
}