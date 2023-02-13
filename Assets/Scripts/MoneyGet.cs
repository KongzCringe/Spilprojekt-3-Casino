using UnityEngine;
using TMPro;

public class MoneyGet : MonoBehaviour
{
    public TextMeshPro textOutput;
    public SlotmachineScript machineMoney;

    void Update()
    {
        textOutput.text = machineMoney.ToString();
    }
}
