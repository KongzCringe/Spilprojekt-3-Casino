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
    
    [SerializeField] private GameObject[] placeables;

    public List<GameObject> placedObjects = new ();

    private float wait;
    private float timer;
    
    private List<GameObject> Npcs = new ();
    private List<GameObject> npcInCasino = new();

    private void Start()
    {
        var save = SaveSystem.LoadPlayerProgress();
        
        if (save != null) LoadPlayerProgress(save);
        else
        {
            foreach (var slotMachine in slotMachines)
            {
                placedObjects.Add(slotMachine);
            }

            foreach (var exchangeCounter in exchangeCounters)
            {
                placedObjects.Add(exchangeCounter);
            }
        }

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

    public void AddPlacedObject(GameObject placedObject)
    {
        placedObjects.Add(placedObject);
    }
    
    public void RemovePlacedObject(GameObject placedObject)
    {
        placedObjects.Remove(placedObject);
    }

    private void LoadPlayerProgress(PlayerProgress playerProgress)
    {
        for (int i = 0; i < playerProgress.objectNames.Length; i++)
        {
            var placeable = placeables.FirstOrDefault(x => x.name == playerProgress.objectNames[i]);
            if (placeable == null) continue;
            
            var placedObject = Instantiate(placeable, 
                
                new Vector3(
                    playerProgress.objectPositions[i].x, 
                    playerProgress.objectPositions[i].y, 
                    playerProgress.objectPositions[i].z),
                
                Quaternion.Euler(
                    playerProgress.objectRotations[i].x,
                    playerProgress.objectRotations[i].y,
                    playerProgress.objectRotations[i].z));

            placedObject.name = placeable.name;
            
            MoneyScript.moneyCount = playerProgress.money;

            placedObjects.Add(placedObject);
        }
    }
    
    private void OnApplicationQuit()
    {
        var array = new GameObject[placedObjects.Count];

        for (int i = 0; i < placedObjects.Count; i++)
        {
            print(placedObjects[i].name);
            array[i] = placedObjects[i];    
        }
        
        SaveSystem.SavePlayerProgress(array);
        
        print("Saved");
    }
}
