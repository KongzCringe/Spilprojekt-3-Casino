using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabberDabberFix : MonoBehaviour
{
    GameObject buildStuff;
    [SerializeField] GameObject JabseUI;
    // Start is called before the first frame update
    void Start()
    {
        buildStuff = GameObject.FindWithTag("DENHER");
    }

    // Update is called once per frame
    void Update()
    {
        if (buildStuff.transform.position.y > -400)
        {
            JabseUI.SetActive(false);
        }
        else
        {
            JabseUI.SetActive(true);
        }
    }
}
