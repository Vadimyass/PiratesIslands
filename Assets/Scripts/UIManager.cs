using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

    public SliderScript sliderScript;

    private void Start()
    {
        instance = this;
        Island.LevelOver += ShowWinnerPanel;
        sliderScript = GetComponentInChildren<SliderScript>();

    }

    private void ShowWinnerPanel(Island obj)
    {
        WinPanel.SetActive(true);
    }
}
