using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AntiAliasScript : MonoBehaviour
{
    string oldChoice;
    bool choiceChanged;
    TMP_Dropdown dropMenu;
    // Start is called before the first frame update
    void Start()
    {
        dropMenu = gameObject.GetComponent<TMP_Dropdown>();
        oldChoice = dropMenu.captionText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //checker om du har valgt om
        if (oldChoice != dropMenu.captionText.text)
        {
            oldChoice = dropMenu.captionText.text;
            choiceChanged = true;
        }
        //hvis valg af Anti-Alias er ændret så ændre anti alias til det valgte antialias niveu enten 0 2 4 eller 8
        if (choiceChanged == true)
        {
            switch (dropMenu.value)
            {
                case 0:
                    QualitySettings.antiAliasing = 2;
                    choiceChanged = false;
                    Debug.Log("Anti-Alias = " + QualitySettings.antiAliasing);
                    break;

                case 1:
                    QualitySettings.antiAliasing = 4;
                    choiceChanged = false;
                    Debug.Log("Anti-Alias = " + QualitySettings.antiAliasing);
                    break;

                case 2:
                    QualitySettings.antiAliasing = 8;
                    choiceChanged = false;
                    Debug.Log("Anti-Alias = " + QualitySettings.antiAliasing);
                    break;

                case 3:
                    QualitySettings.antiAliasing = 0;
                    choiceChanged = false;
                    Debug.Log("Anti-Alias = " + QualitySettings.antiAliasing);
                    break;

                default:
                    break;
            }
        }

    }
}
