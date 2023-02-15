using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenuScript : MonoBehaviour
{
    [SerializeField] float speed;
    bool openState;
    [SerializeField] float targetMenu = -645;
    [SerializeField] GameObject traitorMenu;
    [SerializeField] GameObject mouse;
    [SerializeField] GameObject collectMouse;
    [SerializeField] GameObject emptyMouse;
    [SerializeField] float open;
    [SerializeField] float close;

    private int amountOfExchangeDesks = 0;
    
    private GameLoop gameLoop;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLoop = FindObjectOfType<GameLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        Vector3 target = new Vector3(traitorMenu.GetComponent<RectTransform>().localPosition.x, targetMenu, traitorMenu.GetComponent<RectTransform>().localPosition.z);
        Vector3 current = new Vector3(traitorMenu.GetComponent<RectTransform>().localPosition.x, traitorMenu.GetComponent<RectTransform>().localPosition.y, traitorMenu.GetComponent<RectTransform>().localPosition.z);
        traitorMenu.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(current, target, step);

        if (transform.parent.name != "BuildMenu" ||
            gameLoop.GetExchangeCounter().Count == amountOfExchangeDesks) return;
        
        amountOfExchangeDesks = gameLoop.GetExchangeCounter().Count;
        UpdateExchangeBtn();
    }

    public void ButtonPress()
    {
        if (openState == false)
        {
            mouse.SetActive(true);
            collectMouse.SetActive(false);
            OpenMenu();
        }
        else
        {
            for (int i = 0; i < mouse.transform.childCount; i++)
            {
                mouse.transform.GetChild(i).gameObject.SetActive(false);
                emptyMouse.SetActive(true);
            }
            mouse.SetActive(false);
            collectMouse.SetActive(true);
            CloseMenu();
        }

        if (transform.parent.name == "BuildMenu")
        {
            UpdateExchangeBtn();
        }
    }

    public void UpdateExchangeBtn()
    {
        var parent = transform.parent;

        var button = parent.Find("Exchange Desk");

        var text = button.transform.GetChild(0).GetComponent<TMP_Text>();

        text.text = "Exchange Desk\nTier 1\n1000$\n(" + amountOfExchangeDesks + "/1)";
            
        if (amountOfExchangeDesks == 1)
        {
            button.GetComponent<Image>().color = Color.red;
        }
        else
        {
            button.GetComponent<Image>().color = Color.white;
        }
    }

    void OpenMenu()
    {
        openState = true;
        targetMenu = open;
    }

    void CloseMenu()
    {
        openState = false;
        targetMenu = close;
    }


}
