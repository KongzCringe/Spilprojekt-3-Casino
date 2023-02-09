using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeCounter : MonoBehaviour
{
    public Vector3[] GetPositions()
    {
        var positions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            positions[i] = transform.GetChild(i).position;
        }
        return positions;
    }
}
