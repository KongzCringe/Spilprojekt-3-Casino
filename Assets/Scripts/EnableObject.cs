using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{

    [SerializeField] private GameObject UI;
    [SerializeField] private Animator Anim;
    
    private GameObject BuildMode;

    public bool MouseHover = false;
    
    private Outline _outline;
    
    //private Color startcolor;

    private void Start()
    {
        BuildMode = GameObject.FindWithTag("DENHER");
        
        _outline = GetComponent<Outline>();
    }

    void OnMouseEnter()
    {
        //startcolor = GetComponent<Renderer>().material.color;
        //GetComponent<Renderer>().material.color = Color.yellow;
        _outline.enabled = true;
        
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
        _outline.enabled = false;
        
        MouseHover = false;
        Anim.SetTrigger("DOWN");
        
        Anim.SetBool("MouseHover", false);
        
        //GetComponent<Renderer>().material.color = startcolor;
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
