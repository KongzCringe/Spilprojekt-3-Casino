using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielScript : MonoBehaviour
{
    [SerializeField] GameObject ToggleableObject;
    bool onoff = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Daniel()
    {
        onoff = !onoff;
        ToggleableObject.SetActive(onoff);
    }
}
