using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float smoothFactor = 0.5f;
    public float rotationSpeed = 5.0f;
    public Transform pivotPoint;

    private Vector3 targetPosition;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        targetPosition = transform.position;
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        //transform.LookAt(pivotPoint1);
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //Vector3 forwards = transform.forward * vertical;
        //Vector3 sideways = transform.right * horizontal;

        //targetPosition += (forwards + sideways) * speed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

       
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        //if (Input.GetMouseButton(1))
        //{
        //    float mouseX = Input.GetAxis("Mouse X");
        //    float mouseY = Input.GetAxis("Mouse Y");

        //    yaw += rotationSpeed * mouseX * Time.deltaTime;
        //    pitch -= rotationSpeed * mouseY * Time.deltaTime;
        //    transform.rotation = Quaternion.Euler(0, yaw, 0);
        //    transform.position = pivotPoint.position - (transform.rotation * Vector3.forward * Vector3.Distance(transform.position, pivotPoint.position));
        //}
    }
}