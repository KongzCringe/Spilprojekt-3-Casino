using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float offset = 90f;
    public float fadeDistance = 2f;
    //public GameObject target1;
   // public GameObject target2;

    private Transform mainCameraTransform;
    private float targetRotation;
    //private Renderer renderer1;
    //private Renderer renderer2;
    //private Material material1;
    //private Material material2;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        //renderer1 = target1.GetComponent<Renderer>();
        //renderer2 = target2.GetComponent<Renderer>();
        // material1 = renderer1.material;
        //material2 = renderer2.material;
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

        //float distance = Vector3.Distance(objectPos, target1.transform.position);
        //float fadeAmount1 = Mathf.Clamp01(1f - (distance / fadeDistance));
        //material1.SetFloat("_Cutoff", fadeAmount1);

        //distance = Vector3.Distance(objectPos, target2.transform.position);
        //float fadeAmount2 = Mathf.Clamp01(1f - (distance / fadeDistance));
        //material2.SetFloat("_Cutoff", fadeAmount2);
    }
}