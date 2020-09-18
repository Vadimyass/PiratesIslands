using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScore : MonoBehaviour
{
    public bool IsBusy;

    public static Action<Island> CharacterEnter = delegate { };



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Character>(out Character character))
        {

        }
    }
}
