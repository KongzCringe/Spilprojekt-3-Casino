using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MoneyVisionButton : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    [SerializeField] GameObject ppVision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {
        if (text.text == "Money Vision: On")
        {
            text.text = "Money Vision: Off";
            ppVision.SetActive(false);
        }
        else
        {
            text.text = "Money Vision: On";
            ppVision.SetActive(true);
        }
    }
}
