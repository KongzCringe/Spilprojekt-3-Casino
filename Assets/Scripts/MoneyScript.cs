using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyScript : MonoBehaviour
{
    public int moneyCount;
    [SerializeField] TMP_Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moneyCount.ToString() != moneyText.text)
        {
            moneyText.text = moneyCount.ToString() + "$";
        }
        if (moneyCount < 0)
        {
            moneyText.color = Color.red;
        }
        else
        {
            moneyText.color = Color.green;
        }
    }
}
