using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    
    
    void Start()
    {
        var bounds = GetComponent<Collider>().bounds;

        transform.position += new Vector3(bounds.extents.x, 0, bounds.extents.z);
    }
}
