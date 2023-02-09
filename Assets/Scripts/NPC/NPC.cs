using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Packages.Rider.Editor.UnitTesting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
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

    private int joy = 100;

    private bool VIP;

    private GameLoop gameLoop;
    private SlotmachineScript slotScript;

    private bool targetReached;

    enum Agenda
    {
        Slot,
        Exchange,
        Leave,
        None
    }
    
    enum State
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
        spawnPosition = transform.position;

        var rnd = new Random();
        
        var Entering = rnd.Next(1,3) == 1;
        
        var vipChance = rnd.Next(0, 100);
        VIP = vipChance < 5;
        
        money = rnd.Next(VIP ? 1000 : 3, VIP ? 5000 : 10);
        
        print(Entering);

        if (!Entering)
        {
            task = Agenda.None;
            UpdateState();

            return;
        }

        task = Agenda.Exchange;
        
        UpdateState();
    }
    
    void Update()
    {

        if (state != State.Moving) return;

        var position = transform.position;
        var targetPos = new Vector3(targetNode.position.x, position.y, targetNode.position.y);
        
        transform.position = Vector3.MoveTowards(position, targetPos, 5f * Time.deltaTime);

        if (Vector2.Distance(new Vector2(position.x, position.z), new Vector2(targetPos.x, targetPos.z)) < 0.1f) MoveObject();
    }

    private void UpdateState()
    {
        var rnd = new Random();
        
        var pos = transform.position;
        var startNode = Grid.GetNode(new Vector2(pos.x, pos.z));
        
        switch (task)
        {
            case Agenda.Exchange:
                print("Going to exchange");
                var exchangeCounters = gameLoop.GetExchangeCounter();
                
                var counterIndex = rnd.Next(0, exchangeCounters.Count - 1);

                var positions = exchangeCounters[counterIndex].GetComponent<ExchangeCounter>().GetPositions();
                
                var posIndex = rnd.Next(0, positions.Length - 1);
                
                targetNode = Grid.GetNode(new Vector2(positions[posIndex].x, positions[posIndex].z));
                
                path = PathFinding.FindPath(startNode, targetNode);

                nodeIndex = path.Count - 1;

                state = State.Exchanging;
                MoveObject();
                break;
            
            case Agenda.Slot:
                print("Going to slot");
                var slotMachines = gameLoop.GetSlotMachines();
                
                var slotIndex = rnd.Next(0, slotMachines.Count - 1);

                slotScript = slotMachines[slotIndex].GetComponent<SlotmachineScript>();
                
                var slotPosition = slotScript.GetPosition();
                
                targetNode = Grid.GetNode(new Vector2(slotPosition.x, slotPosition.z));
                
                path = PathFinding.FindPath(startNode, targetNode);

                nodeIndex = path.Count - 1;
                
                state = State.Gambling;
                MoveObject();
                break;
            
            case Agenda.Leave:
                var exit = gameLoop.GetOppositeSpawn(spawnPosition);
                targetNode = Grid.GetNode(new Vector2(exit.x, exit.z));

                path = PathFinding.FindPath(startNode, targetNode);
                nodeIndex = path.Count - 1;

                state = State.Leaving;
                MoveObject();
                break;
            
            case Agenda.None:
                exit = gameLoop.GetOppositeSpawn(spawnPosition);
                targetNode = Grid.GetNode(new Vector2(exit.x, exit.z));

                path = PathFinding.FindPath(startNode, targetNode);
                nodeIndex = path.Count - 1;

                state = State.Leaving;
                MoveObject();
                break;
        }
    }
    
    private void MoveObject()
    {
        state = State.Moving;
        if (nodeIndex >= 0) targetNode = path[nodeIndex];
        else
        {
            switch (task)
            {
                case Agenda.Slot:
                    StartCoroutine(SlotMachine());
                    state = State.Gambling;
                    break;
                    
                case Agenda.Exchange:
                    StartCoroutine(Exchange(4));
                    state = State.Exchanging;
                    break;
                
                case Agenda.Leave:
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
        print("Starts exchanging");
        yield return new WaitForSeconds(timer);
        
        var amount = Mathf.Min(money, 2);
        money -= amount;
        chips += amount;
        
        print("Exchanged " + amount + " money for chips");
        
        GetNextTask();
    }

    private IEnumerator SlotMachine()
    {
        while (chips > 0){
            print("Playing slotmachine");
            yield return new WaitForSeconds(1);
            chips--;
            //slotScript.SlotFunction();
        }
        
        GetNextTask();
    }
    
    

    private void GetNextTask()
    {
        if (chips == 0 && money != 0)
        {
            task = Agenda.Exchange;
            UpdateState();
        }
        else if (chips != 0)
        {
            task = Agenda.Slot;
            UpdateState();
        }
        else
        {
            task = Agenda.Leave;
            UpdateState();
        }
    }
}
