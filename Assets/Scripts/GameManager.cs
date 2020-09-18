using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Islands;


    private void Awake()
    {
        for (int i = 0; i < Islands.Count; i++)
        {
            Islands[i].GetComponent<Island>().NextIsland = Islands[i + 1];
            Islands[i+1].GetComponent<Island>().enabled = false;
        }
        Island.LogDown += DisactivateIsland;
    }

    private void DisactivateIsland(GameObject island)
    {
        island.GetComponent<Island>().enabled = true;
    }
}

