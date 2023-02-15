using System.Collections;
using UnityEngine;

public class ToggleLights : MonoBehaviour
{
    [SerializeField] private GameObject lightComponent;
    [SerializeField] private GameObject GreenScreen;
    [SerializeField] private GameObject GreenScreen1;
    [SerializeField] private GameObject GreenScreen2;
    [SerializeField] private GameObject GreenScreen3;

    void Start()
    {
        Flicker();
    }

    public void Flicker()
    {
        StartCoroutine(FlickerCoroutine());
        StartCoroutine(FlickerScreen());
    }

    private IEnumerator FlickerCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            lightComponent.SetActive(!lightComponent.activeSelf);
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator FlickerScreen()
    {
        for (int i = 0; i < 20; i++)
        {
            GreenScreen.SetActive(!GreenScreen.activeSelf);
            yield return new WaitForSeconds(0.05f);
            GreenScreen1.SetActive(!GreenScreen1.activeSelf);
            yield return new WaitForSeconds(0.05f);
            GreenScreen2.SetActive(!GreenScreen2.activeSelf);
            yield return new WaitForSeconds(0.05f);
            GreenScreen3.SetActive(!GreenScreen3.activeSelf);
            yield return new WaitForSeconds(0.05f);
        }
    }
}