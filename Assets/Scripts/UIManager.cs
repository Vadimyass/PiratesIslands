using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject StarsPanel;
    [SerializeField] private GameObject LosePanel;

    public Score score;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        Island.LevelOver += ShowWinnerPanel;
        score = GetComponentInChildren<Score>();
        WinPanel = transform.GetChild(1).gameObject;
    }

    private void ShowWinnerPanel(Island obj)
    {
        WinPanel.SetActive(true);
        StarsPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        //Destroy(this.gameObject);
    }
}
