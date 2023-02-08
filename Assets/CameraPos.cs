using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public GameObject original; // The game object whose movement we want to clone
    public Vector3 offset; // The offset position
    public float rotationSpeed = 5.0f;
    public Vector3 minBoundaries = new Vector3(-10, -10, -10);
    public Vector3 maxBoundaries = new Vector3(10, 10, 10);
    public float boundarySmoothing = 0.1f;
    private Vector3 targetPosition;
    public float speed = 10.0f;

    void Start()
    {
        targetPosition = transform.position;  
    }

    private void Update()
    {
        transform.position = original.transform.position + offset; // Update the cloned object's position

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

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }


        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
