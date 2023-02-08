using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Grid = Script.Grid;
using Random = System.Random;

public class NPC : MonoBehaviour
{
    private bool moving;
    private NodeBase targetNode;

    private int nodeIndex;

    private List<NodeBase> path;

    private int money;
    private int chips;

    private int joy = 100;

    private bool VIP;
    

    void Start()
    {
        var rnd = new Random();
        var vipChance = rnd.Next(0, 100);
        
        VIP = vipChance > 5;
        
        money = rnd.Next(VIP ? 1000 : 10, VIP ? 5000 : 200);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var startNode = Grid.GetNode(transform.position);
            path = PathFinding.FindPath(startNode, Grid.endNode);

            nodeIndex = path.Count - 1;

            foreach (var nodeBase in path)
            {
                nodeBase.obj.GetComponent<Renderer>().material.color = Color.blue;
            }

            MoveObject();
        }
        
        if (!moving) return;
        
        var position = transform.position;
        var targetPos = new Vector3(targetNode.position.x, position.y, targetNode.position.y);
        
        transform.position = Vector3.MoveTowards(position, targetPos, 5f * Time.deltaTime);

        if (Vector2.Distance(position, targetPos) < 0.1f) MoveObject();
    }
    
    private void MoveObject()
    {
        moving = true;
        if (nodeIndex != 0) targetNode = path[nodeIndex];
        nodeIndex--;
    }
}
