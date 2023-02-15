using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPivot : MonoBehaviour
{
    public Transform pivotPoint;
    public GameObject original;

    void Start()
    {
        transform.LookAt(pivotPoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(pivotPoint);
    }
}
