using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<Island> Islands;
    

    public static GameManager instance;
    

    private void Awake()
    {

        instance = this;
        
        for (int i = 0; i < Islands.Count; i++)
        {
            Islands[i].NextIsland = Islands[i + 1];
            Islands[i + 1].enabled = false;
        }

        Island.LevelOver += GameEnding;
    }

    private void GameEnding(Island obj)
    {
        obj.enabled = false;
    }
    
}

