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
    
    private int _score { get; set; }

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < Islands.Count; i++)
        {
            Islands[i].GetComponent<Island>().NextIsland = Islands[i + 1];
            Islands[i+1].GetComponent<Island>().enabled = false;
        }
    }

    public void AddScore(int Score)
    {
        _score += Score;
        print(_score);
    }
    public void FillProgressBar()
    {
        ProgressBar.value += (100 / (Islands.Count - 1));
    }
}

