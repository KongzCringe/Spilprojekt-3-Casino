using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ExchangeCounter : MonoBehaviour
{
    private bool[] isOccupied;
    Vector3[] positions;
    
    private GameObject[] occupiedBy;
    
    private void Start()
    {
        var childCount = transform.childCount;
        isOccupied = new bool[childCount];
        positions = new Vector3[childCount];
        occupiedBy = new GameObject[childCount];
        
        for (int i = 0; i < isOccupied.Length; i++)
        {
            isOccupied[i] = false;
        }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            positions[i] = transform.GetChild(i).position;
        }
    }

    private void Update()
    {
        /*
        for (int i = 0; i < positions.Length; i++)
        {
            if (occupiedBy[i] == null) continue;
            if (Vector3.Distance(positions[i], occupiedBy[i].transform.position) > 1.5f)
            {
                isOccupied[i] = false;
                occupiedBy[i] = null;
            }
        }
        */
    }

    public void NotOccupied(GameObject npc)
    {
        for (int i = 0; i < isOccupied.Length; i++)
        {
            if (occupiedBy[i] != npc) continue;
            
            isOccupied[i] = false;
            occupiedBy[i] = null;
        }
    }

    public Vector3 GetPosition(GameObject NPC)
    {

        if (isOccupied.All(x => x))
        {
            return Vector3.zero;
        }

        for (int i = 0; i < isOccupied.Length; i++)
        {
            if (occupiedBy[i]) continue;
            
            print(i);
            print(transform.GetChild(i).position);
            isOccupied[i] = true;
            occupiedBy[i] = NPC;
            return transform.GetChild(i).position;
        }

        return Vector3.zero;


    }
}
