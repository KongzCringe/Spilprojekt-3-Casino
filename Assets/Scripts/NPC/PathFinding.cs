using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private bool moving;
    private NodeBase targetNode;
    private GameObject objectToMove;
    
    public static List<NodeBase> FindPath(NodeBase startNode, NodeBase endNode)
    {
        //print("lol");
        var toVisit = new List<NodeBase> { startNode };
        var visited = new List<NodeBase>();

        //Loops while there are nodes to visit
        while (toVisit.Any())
        {
            //print("toVisit: " + toVisit.Count);
            var current = toVisit[0];

            foreach (var node in toVisit)
                if (node.F < current.F || node.F == current.F && node.H < current.H) current = node;
            
            visited.Add(current);

            //current.obj.GetComponent<Renderer>().material.color = visitedColor;
            //SetCosts(current);
            
            toVisit.Remove(current);

            //If end node is reached, return path
            if (current == endNode)
            {
                //print("EndNode Reached");
                var currentPathTile = endNode;
                var path = new List<NodeBase>();
                var count = 1000;
                
                //Loops while there are still tiles in the path
                while (currentPathTile != startNode) {
                    path.Add(currentPathTile);
                    //if (currentPathTile == endNode) currentPathTile.obj.GetComponent<Renderer>().material.color = endColor;
                    //else currentPathTile.obj.GetComponent<Renderer>().material.color = currentPathTile.costMultiplyer == 2 ? pathSwamp : pathColor;

                    currentPathTile = currentPathTile.connectedNode;

                    count--;
                    if (count < 0) throw new Exception();
                    //print("Backtracking");
                }

                //currentPathTile.obj.GetComponent<Renderer>().material.color = startColor;
                
                //Debug.Log(path.Count);
                return path;
            }

            //print("GridPos: " + current.gridPosition);
            
            //Loops through all neighbours of current node
            foreach (var neighbor in current.GetNeighbors().Where(t => t.walkable && !visited.Contains(t)))
            {
                //print("Neibors: " + current.GetNeighbors().Count);
                var inSearch = toVisit.Contains(neighbor);
                var costToNeighbor = current.G + current.GetDistance(neighbor);
                
                //If the cost to the neighbor is lower than the current cost, set the new cost and set the current node as the connected node
                if (!inSearch || costToNeighbor < neighbor.G) {
                    //print("First");
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    //If the neighbor is not in the search list, add it
                    if (!inSearch) {
                        //print("Second");
                        neighbor.SetH(neighbor.GetDistance(endNode));
                        toVisit.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private static void SetCosts(NodeBase node)
    {
        FindChild(node.obj, "G").GetComponent<TMP_Text>().text = Convert.ToInt32(node.G * 10).ToString();
        FindChild(node.obj, "H").GetComponent<TMP_Text>().text = Convert.ToInt32(node.H * 10).ToString();
        FindChild(node.obj, "F").GetComponent<TMP_Text>().text = Convert.ToInt32(node.F * 10).ToString();
    }

    private static GameObject FindChild(GameObject obj, string name)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).name == name) return obj.transform.GetChild(i).gameObject;
        }

        return null;
    }
}
