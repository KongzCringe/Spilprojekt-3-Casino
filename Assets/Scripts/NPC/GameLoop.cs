using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotMachines = new ();
    [SerializeField] private List<GameObject> exchangeCounters = new ();

    [SerializeField] private GameObject SpawnRoad;

    [SerializeField] private GameObject[] playerModels;

    private float wait;
    private float timer;
    
    private List<GameObject> Npcs = new ();
    private List<GameObject> npcInCasino = new();

    private void Start()
    {
        var rnd = new Random();
        wait = rnd.Next(2, 3);
    }

    void Update()
    {
        timer += Time.deltaTime;

        //if (slotMachines.Count < 1 && exchangeCounters.Count < 1) return;
        
        if (timer >= wait)
        {
            timer = 0;
            var rnd = new Random();
            wait = rnd.Next(2, 3);

            var rndModels = rnd.Next(0, playerModels.Length - 1);

            var npc = Instantiate(playerModels[rndModels], 
                GetOppositeSpawn(), 
                Quaternion.identity);
            
            Npcs.Add(npc);
            
            npc.GetComponent<NPC>().StartNPC(this);
        }
    }
    
    public List<GameObject> GetSlotMachines()
    {
        return slotMachines;
    }
    
    public List<GameObject> GetExchangeCounter()
    {
        return exchangeCounters;
    }
    
    public List<GameObject> GetNpcsInCasino()
    {
        return npcInCasino;
    }

    public void NpcEnteredCasino(GameObject npc)
    {
        npcInCasino.Add(npc);
    }
    
    public void NpcLeftCasino(GameObject npc)
    {
        var newNpc = GetClosestNpc();
        npcInCasino.Remove(npc);
        
        if (newNpc == null) return;

        newNpc.GetComponent<NPC>().UpdateNpc();
    }

    public void AddSlotMachine(GameObject slotMachine)
    {
        slotMachines.Add(slotMachine);
    }
    
    public void RemoveSlotMachine(GameObject slotMachine)
    {
        slotMachines.Remove(slotMachine);
    }
    
    public void AddExchangeCounter(GameObject exchangeCounter)
    {
        exchangeCounters.Add(exchangeCounter);
    }
    
    public void RemoveExchangeCounter(GameObject exchangeCounter)
    {
        exchangeCounters.Remove(exchangeCounter);
    }
    
    public void RemoveNpc(GameObject npc)
    {
        Npcs.Remove(npc);
        if (npcInCasino.Contains(npc)) npcInCasino.Remove(npc);
    }

    private GameObject GetClosestNpc()
    {
        GameObject closestNpc = null;
        var closestDistance = double.MaxValue;

        foreach (var npc in Npcs)
        {
            print("npc: " + npc);
            if (npcInCasino.Contains(npc) || 
                Vector3.Distance(npc.transform.position, transform.position) >= closestDistance) continue;

            print("closest npc: " + npc);
            closestDistance = Vector3.Distance(npc.transform.position, transform.position);
            closestNpc = npc;
        }

        return closestNpc;
    }

    public Vector3 GetOppositeSpawn()
    {
        var spawnRoadPos = SpawnRoad.transform.position;
        var bounds = SpawnRoad.GetComponent<Renderer>().bounds;

        var rnd = new Random();

        if (rnd.Next(1, 3) == 1)
        {
            return new Vector3(bounds.max.x - 3, 0,
                spawnRoadPos.z);
        }
        
        return new Vector3(bounds.min.x + 3, 0,
            spawnRoadPos.z);
    }
}
