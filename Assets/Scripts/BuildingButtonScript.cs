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
    
    void Update()
    {
        var btn = GetComponent<Button>();

        if (GameLoop.GetExchangeCounter().Count > 0 && btn.enabled && transform.name == "Exchange Desk")
        {
            var ownedText = NPC.FindChild(gameObject, "Owned");

            if (ownedText != null)
            {
                var text = ownedText.GetComponent<TextMeshProUGUI>();
                
                text.color = Color.red;
                text.text = GameLoop.GetExchangeCounter().Count + "/1";
            }
            btn.enabled = false;
        }
        
        else if (GameLoop.GetExchangeCounter().Count == 0 && !btn.enabled && transform.name == "Exchange Desk")
        {
            var ownedText = NPC.FindChild(gameObject, "Owned");

            if (ownedText != null)
            {
                var text = ownedText.GetComponent<TextMeshProUGUI>();
                
                text.color = Color.white;
                text.text = GameLoop.GetExchangeCounter().Count + "/1";
            }
            
            btn.enabled = true;
        }
        
        
    }
    public void ButtonPress()
    {
        if (GameLoop.GetExchangeCounter().Count > 0  && transform.name == "Exchange Desk") return;
        
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
