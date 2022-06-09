using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelScript : MonoBehaviour
{
    private Vector3 centerCamera;
    [SerializeField] private Image[] Stars;
    [SerializeField] private Material _fillStar;
    private void OnEnable()
    {
        int index = 0;
        centerCamera = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        foreach (Image star in UIManager.instance.scoreView.stars)
        {
            StartCoroutine(AnimateStarsToCenter(star,index));
            index++;
        }

    }

    private IEnumerator AnimateStarsToCenter(Image star, int index)
    {
        while (star.transform.position != centerCamera)
        {
            star.transform.position = Vector3.MoveTowards(star.transform.position, centerCamera,25);

            yield return null;
        }
        star.gameObject.SetActive(false);
        Stars[index].gameObject.SetActive(true);
        if(star.sprite == UIManager.instance.scoreView.fillStar)
        {
            Stars[index].material = _fillStar;
        }

    }

}
