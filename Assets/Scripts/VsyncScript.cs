using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VsyncScript : MonoBehaviour
{
    [SerializeField] TMP_Text buttonText;
    bool vsyncBool = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //knap der skifter mellem at have vsync på og af
    public void buttonPressed()
    {
        vsyncBool = !vsyncBool;
        if (vsyncBool == true)
        {
            QualitySettings.vSyncCount = 1;
            buttonText.text = "Vsync ON";
            Debug.Log(QualitySettings.vSyncCount);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            buttonText.text = "Vsync OFF";
            Debug.Log(QualitySettings.vSyncCount);

        }
    }
}
