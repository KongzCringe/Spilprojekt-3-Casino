using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    //[SerializeField] private GameObject Obj;
    [SerializeField] private GameObject CameraObj;
    public int speed = 5;

    bool toggle = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraObj.transform);
        
        Vector3 direction = CameraObj.transform.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.time);
        
        
        // float Distance = Vector3.Distance(Obj.transform.position, transform.position);

        // if (Distance < 2F)
       // {
            
       //}
    }
}
