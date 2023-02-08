using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = System.Random;

namespace Script
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private List<GameObject> ground;
        [SerializeField] private GameObject debugging;
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private LayerMask floorMask;

        private const int AmountOfTiles = 2;
        private int _tempAmountOfTiles;

        public static NodeBase[,] grid;
        
        private List<GameObject> _listOfDebugging = new ();

        private NodeBase _startNode;
        public static NodeBase endNode;

        private Vector2 startTile = Vector2.zero;
        private Vector2 endTile = Vector2.zero;
        
        private void Start()
        {
            GenerateGrid();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    endNode = GetNode(hit.point);
                }
            }
        }

        private void GenerateGrid()
        {
            grid = new NodeBase[1000, 1000];
            var increment = 1f / AmountOfTiles;

            foreach (var floor in ground)
            {
                var bounds = floor.GetComponent<Collider>().bounds;
                var size = bounds.size;

                var x = 0f;
                
                for (float i = 0; i < size.x; i += increment)
                {
                    var z = 0f;

                    for (float j = 0; j < size.z; j += increment)
                    {
                        var node = new NodeBase {position = new Vector2(bounds.min.x + i, bounds.min.z + j)};
                        grid[(int) x, (int) z] = node;

                        var obj = Instantiate(debugging, new Vector3(bounds.min.x + i, 0, bounds.min.z + j), Quaternion.identity);

                        node.walkable = IsWalkable(new Vector2( bounds.min.x + i, bounds.min.z + j));

                        var nodePos = node.position;

                        var collider = Physics.OverlapSphere(new Vector3(nodePos.x, 0, nodePos.y), increment, obstacleMask);

                        if (collider.Length != 0) 
                            node.walkable = false;

                        if (!node.walkable) obj.GetComponent<Renderer>().material.color = Color.red;
                    
                        z += increment;
                    }
                    x += increment;
                }
            }
        }

        private bool IsWalkable(Vector2 pos)
        {
            var vec3Pos = new Vector3(pos.x, 0, pos.y);
            
            foreach (var floor in ground)
            {
                var floorPos = floor.transform.position;
                if (!(Vector3.Distance(vec3Pos, floorPos) <
                      Vector3.Distance(floor.GetComponent<Renderer>().bounds.min, floor.GetComponent<Renderer>().bounds.max))) continue;
                
                print("test");
                
                var collider = Physics.OverlapSphere(new Vector3(pos.x, 0, pos.y), 1f / AmountOfTiles / 4, floorMask);

                return collider.Length != 0;
            }

            return false;
        }
        
        /*
            for (float i = 0; i < grid.GetLength(0); i += increment)
            {
                var z = 0f;
                for (float j = 0; j < grid.GetLength(1); j += increment)
                {

                    var node = new NodeBase {position = new Vector2(bounds.min.x + x, bounds.min.z + z)};
                    grid[(int) x, (int) z] = node;

                    var walkable = IsWalkable(new Vector2(i, j));
                    
                    print(walkable);

                    node.walkable = walkable;

                    if (walkable)
                    {
                        if (startTile == Vector2.zero) startTile = new Vector2(i, j);
                        if (endTile == Vector2.zero) endTile = new Vector2(i, j);
                        
                        if (endTile.x < i) endTile.x = i;
                        if (endTile.y < j) endTile.y = j;
                    }
                    
                    var nodePos = node.position;

                    var collider = Physics.OverlapSphere(new Vector3(nodePos.x, 0, nodePos.y), increment, obstacleMask);

                    if (collider.Length != 0) 
                        node.walkable = false;
                    
                    z += increment;
                }

                x += increment;
            }
            
            testArea();
         */
        
        public static NodeBase GetNode(Vector3 position)
        {
            var x = Mathf.RoundToInt(position.x);
            var z = Mathf.RoundToInt(position.z);
            return grid[x, z];
        }
    }
}