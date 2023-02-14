using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTake : MonoBehaviour
{
    private CollectScript _CollectScript;
    private void Start()
    {
        _CollectScript = FindObjectOfType<CollectScript>();
    }

    public void TakeMoney()
    {
        print("hey");
        _CollectScript.MoneyTake(gameObject);
    }
}
