using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private UIStarsHandle StarsPanel;
    [SerializeField] private GameObject LosePanel;

    [NonSerialized] public ScoreView scoreView;

    [Inject]
    private void Construct(ScoreView ScoreView, UIStarsHandle starsHandle)
    {
        DontDestroyOnLoad(this);
        instance = this;
        Island.LevelOver += ShowWinnerPanel;
        scoreView = ScoreView;
        StarsPanel = starsHandle;
        WinPanel = transform.GetChild(1).gameObject;
    }
    private void ShowWinnerPanel(Island obj)
    {
        WinPanel.SetActive(true);
        StarsPanel.gameObject.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        //Destroy(this.gameObject);
    }
}
