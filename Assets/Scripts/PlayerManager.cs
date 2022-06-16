using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public List<Character> _characters = new List<Character>();
    private List<Character> _secondaryCharacters = new List<Character>();
    public Character _generalCharacter;
    private CameraController _cameraController;
    public Vector3 nextIslandPos;

    public static PlayerManager instance;
    int index = 0;

    [Inject]
    private void Construct(List<Character> characters, CameraController cameraController)
    {
        _characters = characters;
        _cameraController = cameraController;
    }
    private void Start()
    {
        instance = this;
        AppointGeneralCharacter();
        foreach (var character in _characters)
        {
            if(character == _generalCharacter)
            {
            }
            else
            {
                _secondaryCharacters.Add(character);
            }
        }
    }

    public void AppointGeneralCharacter()
    {
        _generalCharacter = _characters[index];
        print(_generalCharacter.name);
        _secondaryCharacters.Remove(_generalCharacter);
        index++;
        _cameraController.ChangeCameraFollower(_generalCharacter.transform);
    }

    public void WalkToNextIsland(Vector3 nextIsland)
    {
        _generalCharacter.IsGeneral = true;
        nextIslandPos = nextIsland;
        _generalCharacter.MoveToNextIsland();

    }
}
