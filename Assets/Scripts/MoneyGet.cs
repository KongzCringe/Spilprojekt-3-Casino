using UnityEngine;
using TMPro;

public class MoneyGet : MonoBehaviour
{
    public TextMeshPro textOutput;
    public SlotmachineScript SMscript;

    void Update()
    {
        //textOutput.fontSize = 25 - (SMscript.machineMoney / (SMscript.machineMoney / 10));
        textOutput.text =  "$" + SMscript.machineMoney.ToString();
    }
}
