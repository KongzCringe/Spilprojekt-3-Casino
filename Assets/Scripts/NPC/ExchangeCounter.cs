using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ExchangeCounter : MonoBehaviour
{
    private List<bool> isOccupied;
    private List<Vector3> positions;
    
    private List<GameObject> occupiedBy;
    
    private void Start()
    {
        var listOfStandPoint = GetStandPoints();

        isOccupied = new List<bool>();
        positions = new List<Vector3>();
        occupiedBy = new List<GameObject>();

        for (int i = 0; i < listOfStandPoint.Count; i++)
        {
            positions.Add(listOfStandPoint[i].transform.position);
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
        for (int i = 0; i < isOccupied.Count; i++)
        {
            if (occupiedBy[i] != npc) continue;
            
            isOccupied[i] = false;
            occupiedBy[i] = null;
        }
    }

    public void Occupy(GameObject npc)
    {
        isOccupied.Add(true);
        occupiedBy.Add(npc);
    }

    public bool IsOccupied()
    {
        return isOccupied.Any(x => x);
    }

    public Vector3 GetPosition(GameObject NPC)
    {
        /*
        if (isOccupied.All(x => x))
        {
            return Vector3.zero;
        }
        */
        
        Occupy(NPC);

        for (int i = 0; i < positions.Count; i++)
        {
            //if (occupiedBy[i]) continue;
            
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
