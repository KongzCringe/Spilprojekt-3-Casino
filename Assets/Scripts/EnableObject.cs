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
    
    
    
    //private Color startcolor;

    private void Start()
    {
        BuildMode = GameObject.FindWithTag("DENHER");
        
    }

    void OnMouseEnter()
    {
        //startcolor = GetComponent<Renderer>().material.color;
        //GetComponent<Renderer>().material.color = Color.yellow;
        
        
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
