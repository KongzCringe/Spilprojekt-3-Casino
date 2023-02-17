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

    private void Start()
    {
        playBtn.GetComponent<Button>().enabled = false;
        settingsBtn.GetComponent<Button>().enabled = false;

        playText = playBtn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        settingsText = settingsBtn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        var startColor = playText.color;
        startColor.a = 0;

        playText.color = startColor;
        settingsText.color = startColor;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        var color = playText.color;
        color.a = 0;
        
        while (playText.color.a < 255 && settingsText.color.a < 255)
        {
            color.a += 5;

            playText.color = color;
            settingsText.color = color;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
