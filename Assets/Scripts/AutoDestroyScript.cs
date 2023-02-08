using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyScript : MonoBehaviour
{
    [SerializeField] int cost;
    GameObject moneyObject;
    // Start is called before the first frame update
    void Start()
    {
        moneyObject = GameObject.FindWithTag("Money");
    }

    public void SellBuilding()
    {
        moneyObject.GetComponent<MoneyScript>().moneyCount += (cost / 10 * 8);
        gameObject.transform.position = new Vector3(-1231, -1241, -1412);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.y < -1000)
        {
            Destroy(gameObject);
        }
    }

    
}
