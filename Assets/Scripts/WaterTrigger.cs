using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Character character))
        {
            PlayerManager.instance.AppointGeneralCharacter();
            Destroy(character.gameObject);
            Destroy(Island._instantiatedLog);
            Island._instantiatedLog = null;
        }
    }
}
