using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = Script.Grid;

public class SellMouseScript : MonoBehaviour
{
    bool delete;
    bool spaceOccupied;
    Collider otherObject;
    Color colorSave;

    GameLoop gameLoop;
    private Grid grid;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        grid = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delete == true && spaceOccupied == false)
        {
            delete = false;
        }

        if (spaceOccupied == true && otherObject != null)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.red;
            Ray rayray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(rayray, out hit, 10000, 6);
            if (/*hit.collider.gameObject.layer == 6 &&*/ Input.GetMouseButtonDown(0) && spaceOccupied == true)
            {
                Debug.Log(otherObject.gameObject);
                if (otherObject.gameObject.tag != "Wall" && otherObject.gameObject.layer == 3)
                {
                    delete = true;
                }
                
            }
        }

        if (delete == true)
        {
            if (otherObject.gameObject == null) return;
            
            if (OpenCloseMenuButtonScript.GetCasinoOpen() && GameLoop.GetNpcsInCasino().Count >= 0) return;

            if (otherObject.GetComponent<SlotmachineScript>() &&
                otherObject.GetComponent<SlotmachineScript>().IsOccupied())
            {
                return;
            }

            if (otherObject.GetComponent<ExchangeCounter>() &&
                otherObject.GetComponent<ExchangeCounter>().IsOccupied())
            {
                return;
            }
            
            otherObject.gameObject.GetComponent<AutoDestroyScript>().SellBuilding();

            var obj = otherObject.gameObject;

            if (obj.transform.CompareTag("Exchange")) gameLoop.RemoveExchangeCounter(obj);
            else if (obj.transform.CompareTag("Slot")) gameLoop.RemoveSlotMachine(obj);

            grid.GenerateGrid();

            gameLoop.RemovePlacedObject(obj);

            delete = false;
            spaceOccupied = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        spaceOccupied = true;
        if (other.gameObject.tag != "Wall")
        {
            if (otherObject != null)
            {
                otherObject.gameObject.GetComponent<Renderer>().material.color = colorSave;
                otherObject = null;
            }
            otherObject = other;
            colorSave = otherObject.gameObject.GetComponent<Renderer>().material.color;
            otherObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        spaceOccupied = false;
        if (other.gameObject.tag != "Wall" && otherObject != null)
        {
            otherObject.gameObject.GetComponent<Renderer>().material.color = colorSave;
            otherObject = null;
            
        }

        

    }

    private void OnTriggerStay(Collider other)
    {
        spaceOccupied = true;
        
        //if (otherObject = null)
        //{
        //    otherObject = other;
        //}
        //if (other == null)
        //{
        //    spaceOccupied = false;
        //}


    }


}
