using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielLookAt : MonoBehaviour
{
    private Transform camTrans;
    
    void Start()
    {
        camTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camTrans);
    }
}
