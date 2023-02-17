using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SlotmachineScript : MonoBehaviour
{
    GameObject winChance;
    Slider winSlider;
    public int machineMoney;
    [SerializeField] public int bet;
    [SerializeField] AudioSource jackpotSound;
    [SerializeField] AudioSource playSound;

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
        position = GetStandPoint().transform.position;
        occupiedBy = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isOccupied)
        {
            var dist = Vector3.Distance(GetStandPoint().transform.position, occupiedBy.transform.position);

            if (dist > Vector3.Distance(GetStandPoint().transform.position, transform.position) + 5)
            {
                occupiedBy = null;
                isOccupied = false;
            }
        }
        */
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void SlotFunction()
    {
        if (playSound.isPlaying == false)
        {
            playSound.Play();
        }
        
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
                    jackpotSound.Play();
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
            machineMoney += (bet*10);
        }
    }
    
    public void NotOccupied(GameObject npc)
    {
        if (occupiedBy != npc) return;

        isOccupied = false;
        occupiedBy = null;
    }

    public AudioSource GetJackpotSound()
    {
        return jackpotSound;
    }
    
    public AudioSource GetPlaySound()
    {
        return playSound;
    }


    public Vector3 GetPosition(GameObject NPC)
    {
        if (isOccupied)
        {
            return Vector3.zero;
        }

        position = transform.GetChild(0).position;

        isOccupied = true;

        occupiedBy = NPC;

        return position;
    }

    private GameObject GetStandPoint()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "StandPoint")
            {
                return transform.GetChild(i).gameObject;
            }
        }

        return null;
    }
}
