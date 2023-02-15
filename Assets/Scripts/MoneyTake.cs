using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTake : MonoBehaviour
{
    [SerializeField] CollectScript _CollectScript;
    
    public LayerMask clickableLayerMask;  public float clickableDistance = 100f;
            
    private void Start()
    {
        
        
    }

        
    void Update()
    {
        if (_CollectScript == null)
        {
            _CollectScript = FindObjectOfType<CollectScript>();
        }
        // Check for mouse button down event
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // Check if the ray hits a clickable object in the clickable layer mask
            if (Physics.Raycast(ray, out hit, clickableDistance, clickableLayerMask))
            {
                // Check if the hit object is the plane
                if (hit.collider.gameObject == gameObject)
                {
                    // Plane was clicked, so do something
                    TakeMoney();
                }
            }
        }
    }

    public void TakeMoney()
    {
        _CollectScript.MoneyTake(transform.parent.parent.parent.gameObject);
    }
}
