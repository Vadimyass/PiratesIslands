using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterFactory
{
    private DiContainer _container;
    private CharacterConfig _characterConfig;

    public CharacterFactory(DiContainer container, CharacterConfig characterConfig)
    {
        _container = container;
        _characterConfig = characterConfig;
    }

    public Character Create(CharacterReferenceData.CharacterType characterType,Vector3 position )
    {
        var reference = _characterConfig.GetCharacterConfig(characterType);
        return _container.InstantiatePrefab(reference, position, Quaternion.identity, null).GetComponent<Character>();
    }
}
