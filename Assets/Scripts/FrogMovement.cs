using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    [SerializeField] private Rigidbody rb;
    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
 
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fisk")
        {
            transform.position = new Vector3(-1.54f, 1.215f, 0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !moving)
        {
            moving = true;
            m_Animator.SetTrigger("Right");
            rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            StartCoroutine(Moving());
        }

        if (Input.GetKeyDown(KeyCode.W) && !moving)
        {
            moving = true;
            m_Animator.SetTrigger("Front");
            rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            StartCoroutine(Moving());
        }

        if (Input.GetKeyDown(KeyCode.A) && !moving)
        {
            moving = true;
            m_Animator.SetTrigger("Left");
            rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            StartCoroutine(Moving());
        }
        
        if (Input.GetKeyDown(KeyCode.S) && !moving)
        {
             moving = true;
             m_Animator.SetTrigger("Back");
             rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
             StartCoroutine(Moving());
        }
        
    }

    IEnumerator Moving()
    {
        yield return new WaitForSeconds(0.1f);
        moving = false;
    }
}

