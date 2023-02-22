using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabberDabberFix : MonoBehaviour
{
    GameObject buildStuff;
    private OpenMenuScript _openMenuScript;
    [SerializeField] GameObject JabseUI;
    // Start is called before the first frame update
    void Start()
    {
        buildStuff = GameObject.FindWithTag("DENHER");

        _openMenuScript = NPC.FindChild(buildStuff, "MenuButton").GetComponent<OpenMenuScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_openMenuScript.GetOpenState())
        {
            print("Opened");
            JabseUI.SetActive(false);
        }
        else
        {
            print("Closed");
            JabseUI.SetActive(true);
        }
    }
}
