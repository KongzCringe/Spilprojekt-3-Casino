using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotmachineScript : MonoBehaviour
{
    GameObject winChance;
    Slider winSlider;
    public int machineMoney;
    [SerializeField] public int bet;
    
    private bool isOccupied;
    Vector3 position;

    private float notOccupiedForSeconds = 0;
    
    private GameObject occupiedBy;

    // Start is called before the first frame update
    void Start()
    {
        winChance = GameObject.FindWithTag("Winrate");
        winSlider = winChance.GetComponent<Slider>();
        
        isOccupied = false;
        
        occupiedBy = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (occupiedBy != null)
        {
            if (Vector3.Distance(position, occupiedBy.transform.position) > 1.5f)
            {
                notOccupiedForSeconds += Time.deltaTime;
            }

            if (notOccupiedForSeconds > 3)
            {
                notOccupiedForSeconds = 0;
                isOccupied = false;
                occupiedBy = null;
            }
        }
        */
        
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


            //1000 0.2
            //500 1.5
            //100 5
            //10 10
            //5 15
            //1 66.5
            switch (win)
            {
                case int n when n < 665:
                    machineMoney -= bet;
                    break;

                case int n when n < 855:
                    machineMoney -= (bet*2);
                    break;

                case int n when n < 945:
                    machineMoney -= (bet * 10);
                    break;

                case int n when n < 995:
                    machineMoney -= (bet * 100);
                    break;

                case int n when n < 999:
                    machineMoney -= (bet * 500);
                    break;

                case 1000:
                    machineMoney -= (bet * 1000);
                    break;
                default:
                    break;
            }









            //if (win > 665)
            //{
            //    if (win > 855)
            //    {
            //        if (win > 945)
            //        {
            //            if (win > 995)
            //            {
            //                if (win > 999)
            //                {
            //                    machineMoney -= 1000;
            //                }
            //                else
            //                {
            //                    machineMoney -= 500;
            //                }
            //            }
            //            else
            //            {
            //                machineMoney -= 100;
            //            }
            //        }
            //        else
            //        {
            //            machineMoney -= 10;
            //        }
            //    }
            //    else
            //    {
            //        machineMoney -= 5;
            //    }
            //}
            //else
            //{
            //    machineMoney -= 1;
            //}
            

        }
        else
        {
            machineMoney += bet;
        }
    }
    
    public void NotOccupied(GameObject npc)
    {
        if (occupiedBy != npc) return;
            
        isOccupied = false;
        occupiedBy = null;
    }
    

    public Vector3 GetPosition(GameObject NPC)
    {
        if (isOccupied)
        {
            print("Occupied");
            return Vector3.zero;
        }

        position = transform.GetChild(0).position;

        isOccupied = true;

        occupiedBy = NPC;

        return position;
    }
}
