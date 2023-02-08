using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameLoop : MonoBehaviour
{
    private List<GameObject> slotMachines = new List<GameObject>();
    private List<GameObject> exchangeCounter = new List<GameObject>();

    private GameObject[] playerModels;

    private int wait;
    private float timer;
    
    private void Start()
    {
        var rnd = new Random();
        wait = rnd.Next(1, 5);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= wait)
        {
            
        }
    }
}
