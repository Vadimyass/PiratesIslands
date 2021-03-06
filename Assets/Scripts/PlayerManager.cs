﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public List<Character> _characters = new List<Character>();
    private List<Character> _secondaryCharacters = new List<Character>();
    public Character _generalCharacter;

    public Vector3 nextIslandPos;

    public static PlayerManager instance;
    int index = 0;
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
        if (_generalCharacter == null)
        {

        }
        else
        {
            _generalCharacter.camera.SetActive(false);
        }
        _generalCharacter = _characters[index];
        print(_generalCharacter.name);
        _secondaryCharacters.Remove(_generalCharacter);
        index++;
        _generalCharacter.camera.SetActive(true);
    }

    public void WalkToNextIsland(Vector3 nextIsland)
    {
        _generalCharacter.IsGeneral = true;
        nextIslandPos = nextIsland;
        StartCoroutine(_generalCharacter.MoveToCenterRecentIsland(nextIslandPos ,0));

    }
    public void OtherCharacterWalking()
    {
        RTSWalk(nextIslandPos);
    }
    private void RTSWalk(Vector3 positionToMove)
    {
        List<Vector3> targetPositionList = new List<Vector3>
        {
            positionToMove + new Vector3(-0.1f,0,0.1f),
            positionToMove + new Vector3(0.1f,0,0.1f)
        };
        int index = 0;
        foreach (var character in _secondaryCharacters)
        {
            StartCoroutine(character.MoveToCenterRecentIsland(targetPositionList[index],(float)index / 2));
            character._animator.SetBool("IsWalking", true);
            index++;
        }
    }
}
