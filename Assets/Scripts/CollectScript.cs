using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectScript : MonoBehaviour
{
    Collider otherObject;
    [SerializeField] MoneyScript moneyScript;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && otherObject != null && otherObject.GetComponent<SlotmachineScript>())
        {
            gameObject.GetComponent<AudioSource>().Play();
            MoneyScript.moneyCount += (otherObject.GetComponent<SlotmachineScript>().machineMoney - (otherObject.GetComponent<SlotmachineScript>().bet*1000));
            otherObject.GetComponent<SlotmachineScript>().machineMoney = (otherObject.GetComponent<SlotmachineScript>().bet * 1000);
        }   
        
    }

    public void MoneyTake(GameObject SlotMachine)
    {
        var Slotscript = SlotMachine.GetComponent<SlotmachineScript>();
        gameObject.GetComponent<AudioSource>().Play();
        MoneyScript.moneyCount += (Slotscript.machineMoney - (Slotscript.bet*1000));
        Slotscript.machineMoney = (Slotscript.bet * 1000);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        otherObject = other;
    }

    private void OnTriggerExit(Collider other)
    {
        otherObject = null;
    }
}
