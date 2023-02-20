using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroHoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _animator;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool("Enter", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Enter", false);
    }
}
