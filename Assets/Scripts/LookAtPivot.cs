using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPivot : MonoBehaviour
{
    public Transform pivotPoint;
    public GameObject original;

    // Update is called once per frame
    void Update()
    {
        transform.position = original.transform.position;

        transform.LookAt(pivotPoint);
        //transform.Translate(Vector3.right * Time.deltaTime);
    }
}
