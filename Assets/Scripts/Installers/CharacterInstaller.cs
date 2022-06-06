using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private List<Character> _characters;
    [SerializeField] private List<Transform> _charactersSpawnPositions;
    public override void InstallBindings()
    {        

        List<Character> characters = new List<Character>();
        var character1 =
            Container.InstantiatePrefabForComponent<Character>(
                _characters[0], _charactersSpawnPositions[0].position, Quaternion.identity, null);
        characters.Add(character1);
        var character2 =
            Container.InstantiatePrefabForComponent<Character>(
                _characters[1], _charactersSpawnPositions[1].position, Quaternion.identity, null);
        characters.Add(character2);
        var character3 =
            Container.InstantiatePrefabForComponent<Character>(
                _characters[2], _charactersSpawnPositions[2].position, Quaternion.identity, null);
        characters.Add(character3);
        
        Container.Bind<List<Character>>()
            .FromInstance(characters)
            .AsSingle();
    }
}