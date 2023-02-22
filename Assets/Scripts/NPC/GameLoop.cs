using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class GameLoop : MonoBehaviour
{
    private static List<GameObject> slotMachines = new ();
    private static List<GameObject> exchangeCounters = new ();

    [SerializeField] private GameObject SpawnRoad;

    [SerializeField] private GameObject Door;

    [SerializeField] private GameObject[] playerModels;
    
    [SerializeField] private GameObject[] placeables;

    public List<GameObject> placedObjects = new ();

    private int npcSpawned = 0;

    private float wait;
    private float timer;
    
    private List<GameObject> Npcs = new ();
    private List<GameObject> npcInCasino = new();

    private void Start()
    {
        PlayerProgress save = null; //SaveSystem.LoadPlayerProgress();
        
        if (save != null) LoadPlayerProgress(save);
        else
        {
            MoneyScript.moneyCount = 5000;
            
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
        //Set variable to 1 if slotMachines.count is 0 else set it to slotMachines.count
        
        var slotmachinesCount = slotMachines.Count == 0 ? 1 : slotMachines.Count;

        wait = (float) (rnd.Next(2, 3) / slotmachinesCount);
    }

    public GameObject GetDoor()
    {
        return Door;
    }

    void Update()
    {
        timer += Time.deltaTime;

        print(wait);

        //if (slotMachines.Count < 1 && exchangeCounters.Count < 1) return;
        
        if (timer >= wait)
        {
            timer = 0;
            var rnd = new Random();
            wait = (rnd.Next(2, 3) / (slotMachines.Count == 0 ? 1 : slotMachines.Count));

            wait = wait == 0 ? 0.75f : wait;
            
            var rndModels = rnd.Next(0, playerModels.Length - 1);

            var npc = Instantiate(playerModels[rndModels], 
                GetOppositeSpawn() + new Vector3(0, 0.3f, 0), 
                Quaternion.identity);

            npc.name = "NPC " + npcSpawned;
            
            Npcs.Add(npc);
            
            npc.GetComponent<NPC>().StartNPC(this);
        }
    }
    
    public static List<GameObject> GetSlotMachines()
    {
        return slotMachines;
    }
    
    public static List<GameObject> GetExchangeCounter()
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
        
        if (newNpc == null || !OpenCloseMenuButtonScript.GetCasinoOpen()) return;

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
            if (npcInCasino.Contains(npc) || 
                Vector3.Distance(npc.transform.position, transform.position) >= closestDistance ||
                npc.GetComponent<NPC>().GetMoney() <= 0) continue;
            
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
            array[i] = placedObjects[i];    
        }
        
        SaveSystem.SavePlayerProgress(array);
    }
}
