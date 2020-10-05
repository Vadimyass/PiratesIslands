using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScore : MonoBehaviour
{
    private bool IsBusy = false;
    private Vector3 IslandPos;

    private void OnTriggerStay(Collider other)
    {
        if (!IsBusy)
        {
            if (other.TryGetComponent(out AdditionalScore additionalScore))
            {
                GameManager.instance.AddScore(15);
                PlayerManager.instance._generalCharacter.IsGeneral = true;
                PlayerManager.instance.WalkToNextIsland(PlayerManager.instance._generalCharacter._nextIsland);
                IsBusy = true;
                print("Trigger");
            }

            else if (other.TryGetComponent(out Island island))
            {
                GameManager.instance.AddScore(10);
                IsBusy = true;
                PlayerManager.instance._generalCharacter.IsGeneral = true;
                IslandPos = new Vector3(island.transform.position.x, PlayerManager.instance._generalCharacter.transform.position.y, island.transform.position.x);
                PlayerManager.instance.WalkToNextIsland(PlayerManager.instance._generalCharacter._nextIsland);
                print("island");
            }
            else
            {
                print("No");
                IsBusy = true;
                transform.parent.GetComponent<Rigidbody>().isKinematic = true;
                PlayerManager.instance._generalCharacter._recentIslandRef.enabled = true;
                PlayerManager.instance._generalCharacter.IsGeneral = false;
                StartCoroutine(PlayerManager.instance._generalCharacter.MoveToCenterRecentIsland(PlayerManager.instance._generalCharacter.target.position,0));
                PlayerManager.instance._generalCharacter.enabled = false;
;

            }
        }



    }
}
