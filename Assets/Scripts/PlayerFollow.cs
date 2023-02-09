using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform bar;
    private float speed = 2.5f;
  
     void Start() {
        bar = GameObject.Find("CameraPos").transform;
     }
  
     void Update()
     {
        // transform.position = new Vector3(bar.position.x + 10, transform.position.y, bar.position.z + 5);
        transform.position = Vector3.Lerp(transform.position, bar.position, Time.deltaTime * speed);
     }
 }

