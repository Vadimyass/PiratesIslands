using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterPool : MonoBehaviour
{
    private CharacterFactory _characterFactory;
    public List<Character> ActiveCharacters => _activeCharacters;
    private readonly List<Character> _activeCharacters = new List<Character>();

    [Inject]
    public void Construct(CharacterFactory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    public Character GetNextCharacter(CharacterReferenceData.CharacterType characterType,Vector3 targetPostion)
    {
        var character = _characterFactory.Create(characterType, targetPostion);
        _activeCharacters.Add(character);
        return character;
    }
}
