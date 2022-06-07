using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ScoreController
{
    private int _maxScore;
    private int _currentScore;
    public int MaxScore => _maxScore;
    public int CurrentScore =>_currentScore;

    public ScoreController(int maxScore)
    {
        _maxScore = maxScore;
    }
    public void AddScore(int Score)
    {
        _currentScore += Score;
    }
}
