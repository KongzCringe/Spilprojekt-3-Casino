using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinrateTextScript : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChange()
    {
        text.text = "Winrate: " + slider.value.ToString() + "%";
    }
}
