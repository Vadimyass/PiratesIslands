using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class Score : MonoBehaviour
{
    [SerializeField] public Sprite fillStar;
    [SerializeField] public Image[] stars;
    private ScoreController _scoreController;
    [SerializeField] private Slider _progressBar;

    [Inject]
    public void Construct(ScoreController scoreController)
    {
        _scoreController = scoreController;
        _progressBar.maxValue = _scoreController.MaxScore;
    }

    public void AddScore(int score)
    {
        _scoreController.AddScore(score);
        _progressBar.value = _scoreController.CurrentScore;
        CheckScoreForStars();
    }
    private void CheckScoreForStars()
    {
        if (_scoreController.CurrentScore >= _scoreController.MaxScore * 0.5f)
        {
            stars[0].sprite = fillStar;
            stars[0].transform.localScale = Vector3.MoveTowards(stars[0].rectTransform.localScale, new Vector3(1.4f, 1.4f), 0.1f);
        }
        else if (_scoreController.CurrentScore >= _scoreController.MaxScore * 0.75f)
        {
            stars[1].sprite = fillStar;
            stars[1].transform.localScale = Vector3.MoveTowards(stars[1].rectTransform.localScale, new Vector3(1.6f, 1.6f), 0.1f);
        }
        else if (_scoreController.CurrentScore >= _scoreController.MaxScore)
        {
            stars[2].sprite = fillStar;
            stars[2].transform.localScale = Vector3.MoveTowards(stars[2].rectTransform.localScale, new Vector3(1.8f,1.8f), 0.1f);
        }
    }
}
