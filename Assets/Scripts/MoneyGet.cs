using UnityEngine;
using TMPro;

public class MoneyGet : MonoBehaviour
{
    public TextMeshPro textOutput;
    public SlotmachineScript SMscript;

    void Update()
    {
        //textOutput.fontSize = 25 - (SMscript.machineMoney / (SMscript.machineMoney / 10));
        if ((SMscript.machineMoney - (SMscript.bet * 1000)) > -0.01f)
        {   
            textOutput.color = Color.green;
            textOutput.text = "$" + (SMscript.machineMoney - (SMscript.bet * 1000));
        }
        else
        {
            textOutput.color = Color.red;
            textOutput.text = "$" + (SMscript.machineMoney - (SMscript.bet * 1000));
        }
        
    }
}
