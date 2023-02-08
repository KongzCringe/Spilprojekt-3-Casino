using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouseScript : MonoBehaviour
{
    [SerializeField] bool pickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0) && pickedUp == true)
        {
            other.gameObject.transform.parent = null;
            Debug.Log("Place");
            pickedUp = false;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && gameObject.transform.childCount < 1 && pickedUp == false)
            {
                pickedUp = true;
                other.gameObject.transform.parent = gameObject.transform;
                other.transform.localPosition = new Vector3(0, 0, 0);
                Debug.Log("Pickup");

            }
        }
        
        

    }
}
