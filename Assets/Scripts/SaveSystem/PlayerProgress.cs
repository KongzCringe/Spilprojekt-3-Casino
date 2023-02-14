using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public string[] objectNames;
    public float3[] objectPositions;
    public float3[] objectRotations;

    public int money;
    
    public PlayerProgress(GameObject[] objects, int money)
    {
        objectNames = new string[objects.Length];
        objectPositions = new float3[objects.Length];
        objectRotations = new float3[objects.Length];

        this.money = money;
        
        for (int i = 0; i < objects.Length; i++)
        {
            objectNames[i] = objects[i].name;
            objectPositions[i] = objects[i].transform.position;
            objectRotations[i] = objects[i].transform.eulerAngles;
        }
    }
    
}
