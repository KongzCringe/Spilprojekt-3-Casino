using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMenuScript : MonoBehaviour
{
    [SerializeField] GameObject mouse;
    Image buttonImage;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.activeInHierarchy == true && buttonImage.color == Color.white)
        {
            buttonImage.color = Color.gray;
        }
        else if(mouse.activeInHierarchy == false && buttonImage.color == Color.gray)
        {
            buttonImage.color = Color.white;
        }
    }
}
