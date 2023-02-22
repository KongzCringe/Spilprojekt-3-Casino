using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = Script.Grid;

public class MoveMouseScript : MonoBehaviour
{
    [SerializeField] bool pickedUp = false;
    Collider otherObject;
    bool spaceOccupied;
    Color colorSave;

    private Grid grid;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (otherObject == null && gameObject.transform.childCount > 0)
        {
            otherObject = gameObject.transform.GetChild(0).GetComponent<Collider>();
        }
        if (Input.GetMouseButtonDown(0) && pickedUp == true && spaceOccupied == false)
        {
            //Debug.Log("Place");

            grid.GenerateGrid();
            
            pickedUp = false;
            otherObject.gameObject.GetComponent<Renderer>().material.color = colorSave;
            otherObject.isTrigger = false;
            otherObject.gameObject.transform.parent = null;
        }
        
        else if (Input.GetMouseButtonDown(0) && gameObject.transform.childCount < 1 && otherObject != null)
        {
            if (otherObject.gameObject == null ) return;
            
            if (OpenCloseMenuButtonScript.GetCasinoOpen() && GameLoop.GetNpcsInCasino().Count >= 0) return;

            if (otherObject.GetComponent<SlotmachineScript>() && 
                otherObject.GetComponent<SlotmachineScript>().IsOccupied()) return;

            if (otherObject.GetComponent<ExchangeCounter>() &&
                otherObject.GetComponent<ExchangeCounter>().IsOccupied()) return;

            //Debug.Log("Pickup");
            pickedUp = true;
            otherObject.gameObject.transform.parent = gameObject.transform;
            otherObject.transform.localPosition = new Vector3(0, 0, 0);
            otherObject.isTrigger = true;
            colorSave = otherObject.gameObject.GetComponent<Renderer>().material.color;
            otherObject.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }


 
        if (pickedUp == false && gameObject.transform.childCount > 0 && otherObject != null && gameObject.layer == 3)
        {
            otherObject.gameObject.transform.parent = null;
            spaceOccupied = false;
        }
    }

    private void OnDrawGizmos()
    {
        var childCollider = GetComponentInChildren<Collider>();
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(childCollider.bounds.center, childCollider.bounds.size);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Wall")
        {
            otherObject = other;
        }
        if (pickedUp == true)
        {
            spaceOccupied = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        otherObject = null;
        if (pickedUp == true)
        {
            spaceOccupied = false;
        }
    }

}
