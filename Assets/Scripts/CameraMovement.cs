using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float smoothFactor = 0.5f;
    public Vector3 minBoundaries = new Vector3(-10, -10, -10);
    public Vector3 maxBoundaries = new Vector3(10, 10, 10);
    public float boundarySmoothing = 0.1f;
    public float rotationSpeed = 5.0f;

    private Vector3 targetPosition;
    private float yaw = 0.0f;

    void Start()
    {
        targetPosition = transform.position;
        yaw = transform.eulerAngles.y;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forwards = transform.forward * vertical;
        Vector3 sideways = transform.right * horizontal;

        targetPosition += (forwards + sideways) * speed * Time.deltaTime;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, minBoundaries.x, maxBoundaries.x), Mathf.Clamp(targetPosition.y, minBoundaries.y, maxBoundaries.y), Mathf.Clamp(targetPosition.z, minBoundaries.z, maxBoundaries.z));

        Vector3 clampedTarget = targetPosition;
        Vector3 currentPosition = transform.position;
        Vector3 smoothedTarget = Vector3.Lerp(currentPosition, clampedTarget, boundarySmoothing);

        if ((clampedTarget - currentPosition).magnitude < (smoothedTarget - currentPosition).magnitude)
        {
            transform.position = clampedTarget;
        }
        else
        {
            transform.position = smoothedTarget;
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            yaw += rotationSpeed * mouseX * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, yaw, 0);
        }
    }
}