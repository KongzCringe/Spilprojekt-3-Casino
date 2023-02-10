using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        Vector3 target = new Vector3(traitorMenu.GetComponent<RectTransform>().localPosition.x, targetMenu, traitorMenu.GetComponent<RectTransform>().localPosition.z);
        Vector3 current = new Vector3(traitorMenu.GetComponent<RectTransform>().localPosition.x, traitorMenu.GetComponent<RectTransform>().localPosition.y, traitorMenu.GetComponent<RectTransform>().localPosition.z);
        traitorMenu.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(current, target, step);



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
