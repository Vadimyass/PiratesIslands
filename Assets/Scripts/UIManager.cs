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
    [SerializeField] private GameObject LosePanel;

    public SliderScript sliderScript;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        Island.LevelOver += ShowWinnerPanel;
        sliderScript = GetComponentInChildren<SliderScript>();
        WinPanel = transform.GetChild(1).gameObject;
        print(WinPanel.name);
    }

    private void ShowWinnerPanel(Island obj)
    {
        WinPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
