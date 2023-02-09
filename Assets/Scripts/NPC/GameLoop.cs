using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Random = System.Random;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotMachines = new List<GameObject>();
    [SerializeField] private List<GameObject> exchangeCounter = new List<GameObject>();

    [SerializeField] private GameObject SpawnRoad;

    [SerializeField] private GameObject[] playerModels;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var rnd = new Random();
            var rndModels = rnd.Next(0, playerModels.Length - 1);
            
            var bounds = SpawnRoad.GetComponent<Renderer>().bounds;
            
            var npc = Instantiate(playerModels[rndModels], 
                new Vector3(bounds.min.x + 3, playerModels[rndModels].transform.position.y, SpawnRoad.transform.position.z), 
                Quaternion.identity);
            
            npc.GetComponent<NPC>().StartNPC(this);
        }
        
        /*
        if (timer >= wait)
        {
            timer = 0;
            var rnd = new Random();
            wait = rnd.Next(1, 5);

            var rndModels = rnd.Next(0, playerModels.Length - 1);
            
            var bounds = SpawnRoad.GetComponent<Renderer>().bounds;
            
            var npc = Instantiate(playerModels[rndModels], 
                new Vector3(bounds.min.x, playerModels[rndModels].transform.position.y, SpawnRoad.transform.position.z), 
                Quaternion.identity);
            
            npc.GetComponent<NPC>().StartNPC(this);
        }
        */
    }
    
    public List<GameObject> GetSlotMachines()
    {
        return slotMachines;
    }
    
    public List<GameObject> GetExchangeCounter()
    {
        return exchangeCounter;
    }
}
