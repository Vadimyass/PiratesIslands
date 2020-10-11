using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStarsHandle : MonoBehaviour
{
    private List<GameObject> _stars;
    void Start()
    {
        DontDestroyOnLoad(GetComponentInParent<Canvas>());
        Island.LevelOver += ShowStar;
        _stars = new List<GameObject>();
        var transforms = transform.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < transforms.Length; i++)
        {
            _stars.Add(transforms[i].gameObject);
            transforms[i].gameObject.SetActive(false);
        }
    }

    private void ShowStar(Island obj)
    {
        transform.GetChild(3).gameObject.SetActive(true);
        foreach (var star in _stars)
        {
            star.SetActive(true);
        }
        StartCoroutine(RotateStar());
    }

    private IEnumerator RotateStar()
    {
        while (true)
        {
            foreach (var star in _stars)
            {
                //star.transform.RotateAround(star.transform.position, star.transform.position + Vector3.up, 50 * Time.deltaTime);
                star.transform.RotateAroundLocal(Vector3.up,Time.deltaTime);
            }
            yield return null;
        }
    }
}
