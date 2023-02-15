using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteScript : MonoBehaviour
{
    GameObject winChance;
    Slider winSlider;
    // Start is called before the first frame update
    void Start()
    {
        winChance = GameObject.FindWithTag("Winrate");
        winSlider = winChance.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RouletteFunction()
    {

    }
}
