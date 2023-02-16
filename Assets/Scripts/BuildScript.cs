using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = Script.Grid;

public class BuildScript : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int cost;
    bool spaceOccupied;
    bool delete;
    [SerializeField] GameObject emptyMouse;
    int money;
    [SerializeField] LayerMask mask;
    [SerializeField] Collider otherObject;

    private Grid grid;

    private GameLoop gameLoop;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        grid = FindObjectOfType<Grid>();
    }

    private void OnTriggerEnter(Collider other)
    {
        spaceOccupied = true;
        otherObject = other;
    }

    private void OnTriggerExit(Collider other)
    {
        spaceOccupied = false;
        otherObject = null;


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

    // Update is called once per frame
    void Update()
    {

        if (spaceOccupied == true || MoneyScript.moneyCount < cost)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            //if (Input.GetMouseButtonDown(1) && otherObject.gameObject.tag != "Wall" && otherObject.gameObject.layer == 3 && spaceOccupied == true)
            //{
            //    delete = true;
            //}
        }
        else
        {
            Ray rayray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(rayray, out hit, 10000, mask);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.layer == 6 && MoneyScript.moneyCount > cost)
            {
                var obj = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
                obj.name = prefab.name;
                
                if (obj.transform.CompareTag("Exchange")) gameLoop.AddExchangeCounter(obj);
                else if (obj.transform.CompareTag("Slot")) gameLoop.AddSlotMachine(obj);
                
                grid.GenerateGrid();
                
                gameLoop.AddPlacedObject(obj);
                
                MoneyScript.moneyCount -= cost;
                gameObject.SetActive(false);
                emptyMouse.SetActive(true);
            }
        }

        
        

    }

    
}
