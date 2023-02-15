using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Grid = Script.Grid;
using Random = System.Random;

public class NPC : MonoBehaviour
{
    private NodeBase targetNode;

    private int nodeIndex;

    private List<NodeBase> path;

    private Agenda task;
    private State state;

    private NodeBase Target;
    private Vector3 spawnPosition;

    private int money;
    private int chips;
    private int tier;

    private int joy = 100;

    private bool VIP;

    private GameLoop gameLoop;
    private SlotmachineScript slotScript;

    private bool targetReached;

    private GameObject atObject;

    GameObject counter;
    private static readonly int StartWalking = Animator.StringToHash("StartWalking");
    private static readonly int StopWalking = Animator.StringToHash("StopWalking");

    private enum Agenda
    {
        Slot,
        Exchange,
        Leave,
        None
    }

    private enum State
    {
        Idle,
        Moving,
        Gambling,
        Exchanging,
        Leaving
    }

    public void StartNPC(GameLoop gameLoop)
    {
        this.gameLoop = gameLoop;
        
        var rnd = new Random();

        var vipChance = rnd.Next(0, 100);
        //VIP = vipChance < 5;

        tier = rnd.Next(1, GetHighestTier());
        
        money = rnd.Next(5 * tier, 25 * tier);

        if (//gameLoop.GetExchangeCounter().Count <= 0 || 
            gameLoop.GetSlotMachines().Count <= 0 || 
            gameLoop.GetNpcsInCasino().Count >= gameLoop.GetSlotMachines().Count)
        {
            task = Agenda.Leave;
        }
        else
        {
            gameLoop.NpcEnteredCasino(gameObject);
            task = Agenda.Exchange;
        }

        var animator = GetComponent<Animator>();
        animator.SetTrigger(StartWalking);
        
        UpdateState();
    }

    public int GetMoney()
    {
        return money;
    }
    
    void Update()
    {
        if (state != State.Moving) return;

        var position = transform.position;
        var targetPos = new Vector3(targetNode.position.x, position.y, targetNode.position.y);
        
        transform.LookAt(targetPos);
        
        transform.position = Vector3.MoveTowards(position, targetPos, 10f * Time.deltaTime);

        if (Vector2.Distance(new Vector2(position.x, position.z), new Vector2(targetPos.x, targetPos.z)) < 0.1f) MoveObject();
    }

    private GameObject GetSlot()
    {
        var highestTier = GetHighestTier();
        foreach (var slotMachine in gameLoop.GetSlotMachines().Where(x => x.GetComponent<SlotClass>().GetSlotTier() == highestTier))
        {
            if (slotMachine.GetComponent<SlotmachineScript>().IsOccupied()) continue;
            return slotMachine;
        }

        return null;
    }

    private int GetHighestTier()
    {
        var highestTier = 0;
        
        foreach (var slotMachine in gameLoop.GetSlotMachines().Where(x => chips / x.GetComponent<SlotmachineScript>().bet > 5))
        {
            if (slotMachine.GetComponent<SlotClass>().GetSlotTier() > highestTier)
            {
                highestTier = slotMachine.GetComponent<SlotClass>().GetSlotTier();
            }
        }

        return highestTier == 0 ? 1 : highestTier;
    }

    private void UpdateState()
    {
        var rnd = new Random();
        
        var pos = transform.position;
        var startNode = Grid.GetNode(new Vector2(pos.x, pos.z));
        
        switch (task)
        {
            case Agenda.Exchange:
                var exchangeCounters = gameLoop.GetExchangeCounter();
                
                var counterIndex = rnd.Next(0, exchangeCounters.Count - 1);
                
                atObject = exchangeCounters[counterIndex];

                var position = exchangeCounters[counterIndex].GetComponent<ExchangeCounter>().GetPosition(gameObject);
                counter = exchangeCounters[counterIndex];
                
                if (position == Vector3.zero)
                {
                    task = Agenda.Leave;
                    UpdateState();
                    break;
                }

                targetNode = Grid.GetNode(new Vector2(position.x, position.z));
                
                path = PathFinding.FindPath(startNode, targetNode);

                if (path == null)
                {
                    task = Agenda.Leave;
                    UpdateState();
                    break;
                }
                
                nodeIndex = path.Count - 1;

                state = State.Exchanging;
                MoveObject();
                break;
            
            case Agenda.Slot:
                var slotMachines = gameLoop.GetSlotMachines();
                
                var slotmachine = GetSlot();
                
                if (slotmachine == null)
                {
                    task = Agenda.Leave;
                    UpdateState();
                    break;
                }

                slotScript = slotmachine.GetComponent<SlotmachineScript>();
                atObject = slotmachine;

                var slotPosition = slotScript.GetPosition(gameObject);
                
                if (slotPosition == Vector3.zero)
                {
                    task = Agenda.Leave;
                    UpdateState();
                    break;
                }
                
                targetNode = Grid.GetNode(new Vector2(slotPosition.x, slotPosition.z));
                
                path = PathFinding.FindPath(startNode, targetNode);

                if (path == null)
                {
                    task = Agenda.Leave;
                    state = State.Leaving;
                    MoveObject();
                    break;
                }

                nodeIndex = path.Count - 1;
                
                state = State.Gambling;
                MoveObject();
                break;
            
            case Agenda.Leave:
                var exit = gameLoop.GetOppositeSpawn();
                targetNode = Grid.GetNode(new Vector2(exit.x, exit.z));

                path = PathFinding.FindPath(startNode, targetNode);
                nodeIndex = path.Count - 1;

                if (gameLoop.GetNpcsInCasino().Contains(gameObject)) 
                    gameLoop.NpcLeftCasino(gameObject);

                state = State.Leaving;
                MoveObject();
                break;
            
            case Agenda.None:
                exit = gameLoop.GetOppositeSpawn();
                targetNode = Grid.GetNode(new Vector2(exit.x, exit.z));

                path = PathFinding.FindPath(startNode, targetNode);
                nodeIndex = path.Count - 1;

                state = State.Leaving;
                MoveObject();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
        
        var animator = GetComponent<Animator>();
        animator.SetTrigger(StartWalking);
    }

    public void UpdateNpc()
    {
        gameLoop.NpcEnteredCasino(gameObject);
        task = Agenda.Exchange;

        var animator = GetComponent<Animator>();
        animator.SetTrigger(StartWalking);

        UpdateState();
    }
    
    private void MoveObject()
    {
        state = State.Moving;
        if (nodeIndex >= 0) targetNode = path[nodeIndex];
        else
        {
            var animator = GetComponent<Animator>();
            animator.SetTrigger(StopWalking);
            
            transform.LookAt(atObject.transform);

            switch (task)
            {
                case Agenda.Slot:
                    StartCoroutine(SlotMachine());
                    state = State.Gambling;
                    break;
                    
                case Agenda.Exchange:
                    counter.GetComponent<AudioSource>().Play();
                    StartCoroutine(Exchange(4));
                    state = State.Exchanging;
                    break;
                
                case Agenda.Leave:
                    gameLoop.RemoveNpc(gameObject);
                    Destroy(gameObject);
                    break;
                
                default:
                    task = Agenda.None;
                    state = State.Idle;
                    break;
            }
        }
        nodeIndex--;
    }

    private IEnumerator Exchange(int timer)
    {
        yield return new WaitForSeconds(timer);
        
        var amount = Mathf.Min(money, 100);
        
        money -= amount;
        chips += amount;

        GetNextTask();
    }

    private IEnumerator SlotMachine()
    {
        var amountEachIteration = slotScript.bet / chips / 10;
        
        var iteration = 0;
        while (chips > 0)
        {
            iteration++;

            //var ui = FindChild(gameObject, "UI");

            //var child = ui.transform.GetChild(0).gameObject;
            
            //child.GetComponent<Slider>().value = amountEachIteration * iteration;

            if (iteration % 10 == 0)
            {
                chips -= slotScript.bet;
                slotScript.SlotFunction();   
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        
        GetNextTask();
    }

    private void GetNextTask()
    {
        if (chips == 0 && money != 0)
        {
            atObject.GetComponent<SlotmachineScript>().NotOccupied(gameObject);
            task = Agenda.Exchange;
            UpdateState();
        }
        else if (chips != 0)
        {
            task = Agenda.Slot;
            //atObject.GetComponent<ExchangeCounter>().NotOccupied(gameObject);
            UpdateState();
        }
        else
        {
            if (atObject.GetComponent<SlotmachineScript>()) atObject.GetComponent<SlotmachineScript>().NotOccupied(gameObject);
            else atObject.GetComponent<ExchangeCounter>().NotOccupied(gameObject);
            task = Agenda.Leave;
            UpdateState();
        }
    }

    private static GameObject FindChild(GameObject parent, string name)
    {
        var result = parent.transform.Find(name);
        return result == null ? null : result.gameObject;
    }
}
