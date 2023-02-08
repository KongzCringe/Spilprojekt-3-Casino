using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotmachineScript : MonoBehaviour
{
    GameObject winChance;
    Slider winSlider;
    public int machineMoney;
    // Start is called before the first frame update
    void Start()
    {
        winChance = GameObject.FindWithTag("Winrate");
        winSlider = winChance.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            SlotFunction();
        }
    }

    public void SlotFunction()
    {
        int win = Random.Range(1, 101);
        if (win < winSlider.value)
        {
            win = Random.Range(1, 1001);
            if (win > 665)
            {
                if (win > 855)
                {
                    if (win > 945)
                    {
                        if (win > 995)
                        {
                            if (win > 999)
                            {
                                machineMoney -= 1000;
                            }
                            else
                            {
                                machineMoney -= 500;
                            }
                        }
                        else
                        {
                            machineMoney -= 100;
                        }
                    }
                    else
                    {
                        machineMoney -= 10;
                    }
                }
                else
                {
                    machineMoney -= 5;
                }
            }
            else
            {
                machineMoney -= 1;
            }
            //1000 0.2
            //500 1.5
            //100 5
            //10 10
            //5 15
            //1 66.5

        }
        else
        {
            machineMoney += 1;
        }
    }
    
    public Vector3 GetPosition()
    {
        return transform.GetChild(0).position;
    }
}
