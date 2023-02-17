using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyScript : MonoBehaviour
{
    bool safety;
    public static int moneyCount;
    int oldMoney = 0;
    [SerializeField] TMP_Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (oldMoney < moneyCount && ((moneyCount - oldMoney) / 100) > 1)
        {
            safety = true;
            oldMoney += ((moneyCount-oldMoney)/100);
        }
        else
        {
            if (oldMoney > moneyCount && ((moneyCount - oldMoney) / 100) < -1)
            {
                safety = true;
                oldMoney += ((moneyCount - oldMoney) / 100);
            }
            else
            {
                safety = false;
            }
        }
        

        if (oldMoney != moneyCount && safety == false)
        {
            //oldMoney = moneyCount;
            if (oldMoney > moneyCount)
            {
                oldMoney--;

            }
            else
            {
                oldMoney++;

            }
        }


        if (oldMoney.ToString() != moneyText.text)
        {
            moneyText.text = oldMoney.ToString() + "$";
        }
        if (moneyCount < 0)
        {
            moneyText.color = Color.red;
        }
        else
        {
            moneyText.color = Color.green;
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moneyCount += 5000;
        }
        
    }
}
