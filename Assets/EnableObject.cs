using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{

    [SerializeField] private GameObject UI;
    [SerializeField] private Animator Anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Anim.SetTrigger("UP");
    }
    
    void OnMouseExit()
    {
        Anim.SetTrigger("DOWN");
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
