using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScore : MonoBehaviour
{
    private bool IsBusy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsBusy)
        {

            if (other.TryGetComponent(out Island island))
            {
                PlayerManager.instance.WalkToNextIsland(new Vector3(island.transform.position.x,0.24f, island.transform.position.z));
                GameManager.instance.AddScore(10);
                IsBusy = true;
                island.enabled = true;
                print("island");
            }
            else if(other.TryGetComponent(out WaterTrigger water))
            {
                print("No");
                IsBusy = true;
                //transform.parent.GetComponent<Rigidbody>().isKinematic = true;
                PlayerManager.instance._generalCharacter._recentIslandRef.enabled = true;
                PlayerManager.instance._generalCharacter.IsGeneral = false;
                StartCoroutine(PlayerManager.instance._generalCharacter.MoveToCenterRecentIsland(PlayerManager.instance._generalCharacter.target.position,0));
                PlayerManager.instance._generalCharacter.enabled = false;
;

            }
        }



    }
}
