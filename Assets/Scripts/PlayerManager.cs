using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public List<Character> _characters = new List<Character>();
    private List<Character> _secondaryCharacters = new List<Character>();
    private Character _generalCharacter;

    private Vector3 nextIslandPos;

    public static PlayerManager instance;
    private void Start()
    {
        instance = this;
        _generalCharacter = _characters[0];
        Island.LogDown += WalkToNextIsland;
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

    private void WalkToNextIsland(Vector3 nextIsland)
    {
        _generalCharacter.EbalVRot = true;
        nextIslandPos = nextIsland;
        StartCoroutine(_generalCharacter.MoveToPosition(nextIslandPos));

    }
    public void OtherCharacterWalking()
    {
        RTSWalk(nextIslandPos);
    }
    private void RTSWalk(Vector3 positionToMove)
    {
        List<Vector3> targetPositionList = new List<Vector3>
        {
            positionToMove + new Vector3(0.1f,0,-0.1f),
            positionToMove + new Vector3(0.2f,0,-0.1f),
            positionToMove + new Vector3(0.1f,0,-0.2f),
        };
        int index = 0;
        foreach (var character in _secondaryCharacters)
        {
            StartCoroutine(character.MoveToCenterRecentIsland(targetPositionList[index]));
                //StartCoroutine(character.MoveToPosition(positionToMove));
                //StartCoroutine(character.MoveToPosition(targetPositionList[index]));
            index++;
        }
    }
}
