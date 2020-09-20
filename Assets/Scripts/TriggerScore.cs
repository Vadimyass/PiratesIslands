using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScore : MonoBehaviour
{
    public bool IsBusy;



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Character>(out Character character))
        {
        }
    }
}
