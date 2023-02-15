using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{

    [SerializeField] private GameObject UI;
    [SerializeField] private Animator Anim;

    private bool MouseHover = false;

    void OnMouseEnter()
    {
        if (MouseHover == false)
        {
            Anim.SetTrigger("UP");
            MouseHover = true;
        }
        
        Anim.SetBool("MouseHover", true);
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
