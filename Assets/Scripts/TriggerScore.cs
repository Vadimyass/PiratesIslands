using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScore : MonoBehaviour
{
    private bool IsBusy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AdditionalScore additionalScore) && !IsBusy)
        {
            GameManager.instance.AddScore(30);
            GameManager.instance.FillProgressBar();
            IsBusy = true;
        }

        else if (other.TryGetComponent(out Island island) && !IsBusy)
        {
            GameManager.instance.FillProgressBar();
            GameManager.instance.AddScore(10);
            IsBusy = true;
        }

    }
}
