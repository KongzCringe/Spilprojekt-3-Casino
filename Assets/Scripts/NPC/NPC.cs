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

    private int money;
    private int chips;

    private int joy = 100;

    private bool VIP;

    private GameLoop gameLoop;

    private bool targetReached;

    enum Agenda
    {
        Slot,
        Exchange,
        None
    }
    
    enum State
    {
        Idle,
        Moving,
        Gambling,
        Exchanging
    }

    public void StartNPC(GameLoop gameLoop)
    {
        this.gameLoop = gameLoop;
        
        var rnd = new Random();
        var vipChance = rnd.Next(0, 100);
        
        VIP = vipChance < 5;
        
        money = rnd.Next(VIP ? 1000 : 10, VIP ? 5000 : 200);

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
                var exchangeCounters = gameLoop.GetExchangeCounter();
                
                var counterIndex = rnd.Next(0, exchangeCounters.Count - 1);

                var positions = exchangeCounters[counterIndex].GetComponent<ExchangeCounter>().GetPositions();
                
                var posIndex = rnd.Next(0, positions.Length - 1);
                
                targetNode = Grid.GetNode(new Vector2(positions[posIndex].x, positions[posIndex].z));
                
                path = PathFinding.FindPath(startNode, targetNode);

                nodeIndex = path.Count - 1;

                MoveObject();
                
                break;
            
            case Agenda.Slot:
                var slotMachines = gameLoop.GetSlotMachines();
                
                var slotIndex = rnd.Next(0, slotMachines.Count - 1);
                
                var slotPosition = slotMachines[slotIndex].GetComponent<SlotmachineScript>().GetPosition();
                
                targetNode = Grid.GetNode(new Vector2(slotPosition.x, slotPosition.z));
                
                path = PathFinding.FindPath(startNode, targetNode);

                nodeIndex = path.Count - 1;

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
            task = Agenda.None;
            state = State.Idle;
        }
        nodeIndex--;
    }

    private void GetNextTask()
    {
        
    }
}
