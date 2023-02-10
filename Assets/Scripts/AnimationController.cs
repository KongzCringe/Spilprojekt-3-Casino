using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Send the message to the Animator to activate the trigger parameter named "Jump"
            animator.SetTrigger("StartWalking");
        }

        if (Input.GetKey(KeyCode.S))
        {
            //Send the message to the Animator to activate the trigger parameter named "Jump"
            animator.SetTrigger("StopWalking");
        }
    }
}
