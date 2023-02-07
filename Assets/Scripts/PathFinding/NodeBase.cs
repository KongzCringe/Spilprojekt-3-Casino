using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = Script.Grid;

public class NodeBase
{
    public NodeBase connectedNode;
    public bool walkable = true;
    
    //Cost of path to current node
    public float G;
    
    //Expected cost to reach the goal
    public float H;
    
    //Total cost
    public float F => G + H;
    
    public int costMultiplyer = 1;

    public GameObject obj;

    public Vector3 position;
    
    public List<NodeBase> children = new();

    private List<NodeBase> neighbors;

    //Gets distance to another node (Cost of path to next node)
    public float GetDistance(NodeBase other) => Vector2.Distance(position, other.position) * costMultiplyer;
    
    //Sets the connected node to backtrack path
    public void SetConnection(NodeBase nodeBase) => connectedNode = nodeBase;

    
    public List<NodeBase> GetNeighbors()
    {
        neighbors = new List<NodeBase>();

        //Adds the neighbors of the node to list (Vertical, Horizontal)
        IsNeighborLegal(position + new Vector3(1,0));
        IsNeighborLegal(position + new Vector3(-1,0));
        IsNeighborLegal(position + new Vector3(0,1));
        IsNeighborLegal(position + new Vector3(0,-1));
        
        //Adds the neighbors of the node to list (Diagonal)
        IsNeighborLegal(position + new Vector3(1,1));
        IsNeighborLegal(position + new Vector3(-1,-1));
        IsNeighborLegal(position + new Vector3(-1,1));
        IsNeighborLegal(position + new Vector3(1,-1));
        
        return neighbors;
    }

    private void IsNeighborLegal(Vector2 pos)
    {
        //Checks if the neighbor is inside the grid and is walkable
        if (!(pos.x < Grid.grid.GetLength(0)) || !(pos.x >= 0) ||
            !(pos.y < Grid.grid.GetLength(1)) || !(pos.y >= 0) ||
            !Grid.grid[(int) pos.x, (int) pos.y].walkable) return;
        
        neighbors.Add(Grid.grid[(int) pos.x, (int) pos.y]);
    }
    
    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;
}
