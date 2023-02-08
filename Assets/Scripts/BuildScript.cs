using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int cost;
    bool spaceOccupied;
    bool delete;
    GameObject moneyObject;
    [SerializeField] GameObject emptyMouse;
    int money;
    // Start is called before the first frame update
    void Start()
    {
        moneyObject = GameObject.FindWithTag("Money");
        

    }

    private void OnTriggerEnter(Collider other)
    {
        spaceOccupied = true;
    }

    private void OnTriggerExit(Collider other)
    {
        spaceOccupied = false;
        
    }

    private void OnTriggerStay(Collider other)
    {
        spaceOccupied = true;
        if (other == null)
        {
            spaceOccupied = false;
        }
        
        if (delete == true)
        {
            other.gameObject.GetComponent<AutoDestroyScript>().SellBuilding();
            delete = false;
            spaceOccupied = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (delete == true && spaceOccupied == false)
        {
            delete = false;
        }
        if (spaceOccupied == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (Input.GetMouseButtonDown(1))
            {
                delete = true;
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
                moneyObject.GetComponent<MoneyScript>().moneyCount -= cost;
                gameObject.SetActive(false);
                emptyMouse.SetActive(true);
            }
        }

        
        

    }

    
}
