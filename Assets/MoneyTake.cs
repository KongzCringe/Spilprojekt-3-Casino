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
        _CollectScript.MoneyTake(gameObject);
    }
}
