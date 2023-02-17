using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.Video;

public class FadeInButtons : MonoBehaviour
{
    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject videoPlayerObj;

    private GameObject settingsBtn;
    private VideoPlayer videoPlayer;

    private TMP_Text playText;
    private TMP_Text settingsText;

    private Animation playAnim;
    private Animation settingsAnim;

    private bool hasStarted;
    private bool fadedIn;

    private void Start()
    {
        playBtn.GetComponent<Button>().enabled = false;
        videoPlayer = videoPlayerObj.GetComponent<VideoPlayer>();
        //settingsBtn.GetComponent<Button>().enabled = false;

        playText = playBtn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        //settingsText = settingsBtn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        playAnim = playText.GetComponent<Animation>();
        //settingsAnim = settingsText.GetComponent<Animation>();

        var startColor = playText.color;
        startColor.a = 0;

        playText.color = startColor;
        //settingsText.color = startColor;
    }

    private void Update()
    {
        if (!hasStarted && videoPlayer.isPlaying) hasStarted = true;
        
        if (hasStarted && !videoPlayer.isPlaying && !fadedIn) FadeIn();
    }

    private void FadeIn()
    {
        fadedIn = true;
        playAnim.Play();
        //settingsAnim.Play();

        hasStarted = true;
        
        playBtn.GetComponent<Button>().enabled = true;
    }
}
