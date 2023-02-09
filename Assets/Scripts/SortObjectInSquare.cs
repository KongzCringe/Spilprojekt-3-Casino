using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortObjectInSquare : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        var aPos = a.transform.position;
        var bPos = b.transform.position;

        return aPos.z != bPos.z ? aPos.z.CompareTo(bPos.z) : aPos.x.CompareTo(bPos.x);
    }
}