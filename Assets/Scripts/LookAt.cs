using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float offset = 90f;
    public float fadeDistance = 2f;
    

    private Transform mainCameraTransform;
    private float targetRotation;
   

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
      
    }

    private void Update()
    {
        Vector3 targetDirection = mainCameraTransform.position - transform.position;
        targetRotation = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg + offset;

        float currentRotation = transform.rotation.eulerAngles.y;
        currentRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Distance(Camera.main.transform.position, transform.position);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePosition);

        
    }
}