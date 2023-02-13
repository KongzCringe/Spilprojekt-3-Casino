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
        var listOfStandPoint = GetStandPoints();

        isOccupied = new bool[listOfStandPoint.Count];
        positions = new Vector3[listOfStandPoint.Count];
        occupiedBy = new GameObject[listOfStandPoint.Count];
        
        for (int i = 0; i < isOccupied.Length; i++)
        {
            isOccupied[i] = false;
        }

        for (int i = 0; i < listOfStandPoint.Count; i++)
        {
            positions[i] = listOfStandPoint[i].transform.position;
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
            
            isOccupied[i] = true;
            occupiedBy[i] = NPC;
            return positions[i];
        }

        return Vector3.zero;


    }
    
    private List<GameObject> GetStandPoints()
    {
        var standPoints = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "StandPoint")
            {
                 standPoints.Add(transform.GetChild(i).gameObject);
            }
        }

        return standPoints;
    }
}
