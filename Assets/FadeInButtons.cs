using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeInButtons : MonoBehaviour
{
    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject settingsBtn;

    private TMP_Text playText;
    private TMP_Text settingsText;

    private Animation playAnim;
    private Animation settingsAnim;

    private bool hasStarted;

    private void Start()
    {
        playBtn.GetComponent<Button>().enabled = false;
        //settingsBtn.GetComponent<Button>().enabled = false;

        playText = playBtn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        //settingsText = settingsBtn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        playAnim = playText.GetComponent<Animation>();
        //settingsAnim = settingsText.GetComponent<Animation>();

        var startColor = playText.color;
        startColor.a = 0;

        playText.color = startColor;
        //settingsText.color = startColor;
        
        FadeIn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) FadeIn();

        if (!playAnim.isPlaying && hasStarted)
        {
            playBtn.GetComponent<Button>().enabled = true;
            //settingsBtn.GetComponent<Button>().enabled = true;
        }
    }

    private void FadeIn()
    {
        playAnim.Play();
        //settingsAnim.Play();

        hasStarted = true;
    }
}
