using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteScript : MonoBehaviour
{
    [SerializeField] public int bet;
    public int machineMoney;
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
        int win = Random.Range(1, 101);
        int thisBet = (Random.Range(bet, (bet * 2 + 1)));
        if (win < winSlider.value)
        {
            machineMoney -= thisBet;

        }
        else
        {
            machineMoney += ((thisBet) * 10);
        }

        }
}
