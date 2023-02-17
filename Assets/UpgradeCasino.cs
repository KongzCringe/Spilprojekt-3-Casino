using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCasino : MonoBehaviour
{
    [SerializeField] GameObject casinoTier1;
    [SerializeField] GameObject casinoTier2;
    [SerializeField] GameObject casinoTier3;

    int casinoLevel = 1;
    public bool upgraded;
    int upgradeCost = 50000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (upgraded == true)
        {
            switch (casinoLevel)
            {
                case 2:
                    MoneyScript.moneyCount -= upgradeCost;
                    upgradeCost = 100000;
                    gameObject.GetComponent<TMP_Text>().text = "Upgrade Casino\n$100000";
                    casinoTier2.SetActive(true);
                    casinoTier1.SetActive(false);
                    upgraded = false;
                    break;

                case 3:
                    MoneyScript.moneyCount -= upgradeCost;
                    upgradeCost = 999999999;
                    gameObject.GetComponent<TMP_Text>().text = "Upgrade Casino\nFully Upgraded";
                    casinoTier3.SetActive(true);
                    casinoTier2.SetActive(false);
                    upgraded = false;
                    break;
                default:
                    break;
            }
        }

        
    }

    public void Upgrade()
    {
        if (MoneyScript.moneyCount > upgradeCost)
        {
            upgraded = true;
            casinoLevel++;
        }
    }
}
