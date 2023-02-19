using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UIChooser : MonoBehaviour
{
    [SerializeField] private GameObject otherGameObject1;
    [SerializeField] private GameObject otherGameObject2;
    [SerializeField] private GameObject thisGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMouseDown()
    {
        thisGameObject.SetActive(true);
        
        otherGameObject1.SetActive(false);
        
        otherGameObject2.SetActive(false);
        
        print("done");
    }
}
