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

    private float wait;
    private float timer;
    
    private void Start()
    {
        var rnd = new Random();
        wait = rnd.Next(1, 2);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (slotMachines.Count < 1 && exchangeCounter.Count < 1) return;
        
        if (timer >= wait)
        {
            timer = 0;
            var rnd = new Random();
            wait = rnd.Next(1, 2);

            var rndModels = rnd.Next(0, playerModels.Length - 1);
            
            var bounds = SpawnRoad.GetComponent<Renderer>().bounds;
            
            var npc = Instantiate(playerModels[rndModels], 
                new Vector3(bounds.min.x, playerModels[rndModels].transform.position.y, SpawnRoad.transform.position.z), 
                Quaternion.identity);
            
            npc.GetComponent<NPC>().StartNPC(this);
        }
        
        /*
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
    
    public Vector3 GetOppositeSpawn(Vector3 spawnPosition)
    {
        var spawnRoadPos = SpawnRoad.transform.position;
        var bounds = SpawnRoad.GetComponent<Renderer>().bounds;

        var rnd = new Random();

        if (rnd.Next(1, 2) == 1)
        {
            return new Vector3(bounds.max.x - 3, 0,
                spawnRoadPos.z);
        }
        
        return new Vector3(bounds.min.x + 3, 0,
            spawnRoadPos.z);
    }
}
