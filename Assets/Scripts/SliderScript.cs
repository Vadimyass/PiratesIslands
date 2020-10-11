using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] public Sprite _fillStar;
    [SerializeField] public Image[] _stars;

    private void FixedUpdate()
    {
        if (GameManager.instance.score >= GameManager.instance.MaxScore)
        {
            _stars[2].sprite = _fillStar;
            _stars[2].transform.localScale = Vector3.MoveTowards(_stars[2].rectTransform.localScale, new Vector3(1.8f,1.8f), 0.1f);
        }

        else if (GameManager.instance.score >= 75)
        {
            _stars[1].sprite = _fillStar;
            _stars[1].transform.localScale = Vector3.MoveTowards(_stars[1].rectTransform.localScale, new Vector3(1.6f, 1.6f), 0.1f);
        }
        else if (GameManager.instance.score >= 50)
        {
            _stars[0].sprite = _fillStar;
            _stars[0].transform.localScale = Vector3.MoveTowards(_stars[0].rectTransform.localScale, new Vector3(1.4f, 1.4f), 0.1f);
        }
    }
}
