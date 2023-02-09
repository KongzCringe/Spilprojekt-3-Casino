using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouseScript : MonoBehaviour
{
    [SerializeField] bool pickedUp = false;
    Collider otherObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && pickedUp == true)
        {
            
            Debug.Log("Place");
            pickedUp = false;
        }
        else if (Input.GetMouseButtonDown(0) && gameObject.transform.childCount < 1 && otherObject != null)
        {
            pickedUp = true;
            otherObject.gameObject.transform.parent = gameObject.transform;
            otherObject.transform.localPosition = new Vector3(0, 0, 0);
            Debug.Log("Pickup");
        }


 
        if (pickedUp == false && gameObject.transform.childCount > 0)
        {
            otherObject.gameObject.transform.parent = null;
        }
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
