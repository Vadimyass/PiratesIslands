using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> Islands;

    [SerializeField] private Slider ProgressBar;

    public static GameManager instance;
    
    public int MaxScore;

    public int score;

    private void Awake()
    {
        MaxScore = Islands.Count * 15;
        ProgressBar.maxValue = MaxScore;

        instance = this;

        for (int i = 0; i < Islands.Count; i++)
        {
            Islands[i].GetComponent<Island>().NextIsland = Islands[i + 1];
            Islands[i + 1].GetComponent<Island>().enabled = false;
        }
        Island.LevelOver += GameEnding;
    }

    private void GameEnding(Island obj)
    {
        obj.enabled = false;
    }

    public void AddScore(int Score)
    {
        score += Score;
        ProgressBar.value += Score;
    }
}

