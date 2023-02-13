using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyFadeScript : MonoBehaviour
{
    [SerializeField] GameObject machine;
    GameObject mouse;
    SlotmachineScript moneyScript;
    float distance;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        mouse = GameObject.FindWithTag("CollectMouse");
        moneyScript = machine.GetComponent<SlotmachineScript>();
        text = gameObject.GetComponent<TMP_Text>();
        Debug.Log(machine +" "+ mouse + " " + moneyScript + " " + distance + " " + text );
    }


    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, mouse.transform.position);
        
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255-(distance*10*2.55f));
        
        Debug.Log(distance +" "+ text.color);
    }
}
