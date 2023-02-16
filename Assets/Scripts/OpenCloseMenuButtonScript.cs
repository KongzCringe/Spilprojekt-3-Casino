using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OpenCloseMenuButtonScript : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    
    private static bool casinoOpen = true;

    public static bool GetCasinoOpen()
    {
        return casinoOpen;
    }
    
    public void ButtonPress()
    {
        casinoOpen = !casinoOpen;

        text.text = casinoOpen ? "Opened" : "Closed";
    }
}
