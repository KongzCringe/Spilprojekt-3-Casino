using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject CameraObj;
    public int speed = 5;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = CameraObj.transform.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
    }
}
