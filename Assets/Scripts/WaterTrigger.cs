using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterTrigger : MonoBehaviour
{
    private WoddenPlank _log;
    private void Start()
    {
        Island.LogDown += Logging;
    }

    private void Logging(WoddenPlank obj)
    {
        _log = obj;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Character character))
        {
            PlayerManager.instance.AppointGeneralCharacter();
            character._recentIslandRef.enabled = true;
            Destroy(character.gameObject);
            Destroy(_log.gameObject);
        }
    }
}
