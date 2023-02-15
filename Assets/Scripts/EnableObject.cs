using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{

    [SerializeField] private GameObject UI;
    [SerializeField] private Animator Anim;
    
    private GameObject BuildMode;

    private bool MouseHover = false;

    private void Start()
    {
        BuildMode = GameObject.FindWithTag("DENHER");

    }

    void OnMouseEnter()
    {
        if (MouseHover == false)
        {
            Anim.SetTrigger("UP");
            MouseHover = true;
        }

        if (BuildMode.transform.position.y < -93.5f) 
        {
            Anim.SetBool("MouseHover", true);   
        }
        else
        {
            Anim.SetBool("MouseHover", false);
        }
    }
    
    void OnMouseExit()
    {
        MouseHover = false;
        Anim.SetTrigger("DOWN");
        
        Anim.SetBool("MouseHover", false);
    }

    public void Disable()
    {
        UI.SetActive(false);
    }
    
    public void Enable()
    {
        UI.SetActive(true);
    }
}
