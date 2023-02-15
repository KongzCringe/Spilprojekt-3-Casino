using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonScript : MonoBehaviour
{
    bool alreadyPressed;
    [SerializeField] GameObject mouse;
    [SerializeField] GameObject mouseIcon;
    [SerializeField] GameObject emptyMouse;

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
        if (transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("(1/1)")) return;
        
        if (mouseIcon.activeSelf == true)
        {
            alreadyPressed = true;
        }
        else
        {
            alreadyPressed = false;
        }
        for (int i = 0; i < mouse.transform.childCount; i++)
        {
            mouse.transform.GetChild(i).gameObject.SetActive(false);
            emptyMouse.SetActive(true);
        }
        if (alreadyPressed == false)
        {
            mouseIcon.SetActive(true);
            emptyMouse.SetActive(false);
        }
        
    }
}
