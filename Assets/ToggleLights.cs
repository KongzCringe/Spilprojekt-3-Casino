using System;
using System.Collections;
using UnityEngine;

public class ToggleLights : MonoBehaviour
{
    [SerializeField] private GameObject lightComponent;
    [SerializeField] private GameObject GreenScreen;
    [SerializeField] private GameObject GreenScreen1;
    [SerializeField] private GameObject GreenScreen2;
    [SerializeField] private GameObject GreenScreen3;
    
    [SerializeField] private GameObject PivotLights;

    private bool Activated;
    
    void Start()
    {
        Flicker();
    }

    private void Update()
    {
        if (Activated)
        {
            PivotLights.transform.Rotate (0,0,50 * Time.deltaTime * 5); //rotates 50 degrees per second around z axis
        }
    }

    public void Flicker()
    {
        StartCoroutine(FlickerCoroutine());
        StartCoroutine(FlickerScreen());

        Activated = true;
    }

    private IEnumerator FlickerCoroutine()
    {
        PivotLights.SetActive(!PivotLights.activeSelf);
        
        

        for (int i = 0; i < 10; i++)
        {
            lightComponent.SetActive(!lightComponent.activeSelf);
            
            yield return new WaitForSeconds(0.5f);
        }
        
        PivotLights.SetActive(!PivotLights.activeSelf);

        Activated = false;
    }

    private IEnumerator FlickerScreen()
    {
        for (int i = 0; i < 5; i++)
        {
            GreenScreen.SetActive(!GreenScreen.activeSelf);
            yield return new WaitForSeconds(0.1f);
            GreenScreen.SetActive(!GreenScreen.activeSelf);
            yield return new WaitForSeconds(0.1f);
            GreenScreen1.SetActive(!GreenScreen1.activeSelf);
            yield return new WaitForSeconds(0.1f);
            GreenScreen1.SetActive(!GreenScreen1.activeSelf);
            yield return new WaitForSeconds(0.1f);
            GreenScreen2.SetActive(!GreenScreen2.activeSelf);
            yield return new WaitForSeconds(0.1f);
            GreenScreen2.SetActive(!GreenScreen2.activeSelf);
            yield return new WaitForSeconds(0.1f);
            GreenScreen3.SetActive(!GreenScreen3.activeSelf);
            yield return new WaitForSeconds(0.1f);
            GreenScreen3.SetActive(!GreenScreen3.activeSelf);
            yield return new WaitForSeconds(0.1f);
        }
    }
}