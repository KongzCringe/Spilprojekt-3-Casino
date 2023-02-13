using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FullscreenScript : MonoBehaviour
{
    //en int til switchen
    public int state = 1;
    public int oldState;
    public bool changed = false;
    public bool changeButton = false;

    [SerializeField] TMP_Dropdown screenChoice;
    [SerializeField] ChangesButton changeButtonScript;
    [SerializeField] GameObject changeButtonObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checker hvilken indstilling der er valgt
        if (screenChoice.captionText.text == "Fullscreen" && state != 1)
        {
            oldState = state;
            state = 1;
            changed = true;

        }
        else if (screenChoice.captionText.text == "Borderless" && state != 2)
        {
            oldState = state;
            state = 2;
            changed = true;

        }
        else if (screenChoice.captionText.text == "Windowed" && state != 3)
        {
            oldState = state;
            state = 3;
            changed = true;

        }
        //indstiller indstillingen
        if (changed == true)
        {
            changed = false;
            switch (state)
            {
                case 1:
                    Debug.Log("fullscreen");
                    if (changeButton == true)
                    {
                        changeButton = false;
                    }
                    else
                    {
                        changeButtonObject.SetActive(true);
                        changeButtonScript.start = true;
                    }
                    
                    Screen.fullScreen = true;
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;

                case 2:
                    Debug.Log("borderless");
                    if (changeButton == true)
                    {
                        changeButton = false;
                    }
                    else
                    {
                        changeButtonObject.SetActive(true);
                        changeButtonScript.start = true;
                    }
                    Screen.fullScreen = true;
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;

                case 3:
                    Debug.Log("windowed");
                    if (changeButton == true)
                    {
                        changeButton = false;
                    }
                    else
                    {
                        changeButtonObject.SetActive(true);
                        changeButtonScript.start = true;
                    }
                    Screen.fullScreen = false;
                    Screen.fullScreenMode = FullScreenMode.Windowed;

                    break;

                default:
                    break;
            }

        }
        
    }
}
