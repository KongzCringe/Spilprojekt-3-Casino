using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeScript : MonoBehaviour
{
    [SerializeField] GameObject buildMenu;
    [SerializeField] GameObject buildMode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buildMenu.activeInHierarchy == true)
        {
            buildMode.SetActive(true);
        }
        else
        {
            buildMode.SetActive(false);
        }
    }
}
