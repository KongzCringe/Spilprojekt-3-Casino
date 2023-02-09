using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public int deathTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position += Vector3.forward *  Time.deltaTime;
        StartCoroutine(DestroyCar());
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }
    
    IEnumerator DestroyCar()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}