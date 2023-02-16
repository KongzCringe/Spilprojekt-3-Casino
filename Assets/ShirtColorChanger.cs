using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtColorChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] HatList;
    [SerializeField] private GameObject[] BrilleList;

    private int randomHat;
    private int randomBrille;
    
    // Start is called before the first frame update
    void Start()
    {
        randomBrille = Random.Range(0, BrilleList.Length);
        randomHat = Random.Range(0, HatList.Length);
        
        HatList[randomHat].SetActive(true);
        BrilleList[randomBrille].SetActive(true);
        
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
