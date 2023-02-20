using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptionsScript : MonoBehaviour
{
    [SerializeField] GameObject options;
    bool onoff = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onoff = !onoff;
            options.SetActive(onoff);
        }
    }
}
