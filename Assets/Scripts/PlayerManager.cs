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
    private void Construct(CameraController cameraController)
    {
        _cameraController = cameraController;
    }

    public void SetCharacters(List<Character> characters)
    {
        _characters = characters;
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
    private void Awake()
    {
        instance = this;
    }

    public void AppointGeneralCharacter()
    {
        _generalCharacter = _characters[index];
        print(_generalCharacter.name);
        _secondaryCharacters.Remove(_generalCharacter);
        index++;
        _cameraController.ChangeCameraFollower(_generalCharacter.transform);
    }

    public void WalkToNextIsland(Island nextIsland)
    {
        _generalCharacter.IsGeneral = true;
        nextIslandPos = new Vector3(nextIsland.transform.position.x,0.24f, nextIsland.transform.position.z);
        _generalCharacter.MoveToNextIsland();
        foreach (Character character in _characters)
        {
            character._recentIslandRef = nextIsland;
            character._nextIsland = nextIslandPos;
        }
    }
}
