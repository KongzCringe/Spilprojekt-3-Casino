using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Unity.Mathematics;
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

        private const int AmountOfTiles = 4;
        private int _tempAmountOfTiles;

        public static NodeBase[,] grid;

        private NodeBase _startNode;
        public static NodeBase endNode;

        private Vector2 startTile = Vector2.zero;
        private Vector2 endTile = Vector2.zero;
        
        private void Start()
        {
            StartCoroutine(GenerateGrid());
        }

        private void Update()
        {
            /*
            if (!Input.GetMouseButtonDown(0)) return;
            
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //if (endNode != null) endNode.obj.GetComponent<Renderer>().material.color = Color.gray;
                var hitPos = new Vector2(hit.point.x, hit.point.z);
                endNode = GetNode(hitPos);

                if (!endNode.walkable) return;
                    
                //if (endNode == null) print("null");
                //endNode.obj.GetComponent<Renderer>().material.color = Color.black;
            }
            */
        }

        private IEnumerator GenerateGrid()
        {
            ground.Sort(new SortObjectInSquare());
            
            const float increment = 1f / AmountOfTiles;
            
            var max = GetMax();
            var min = GetMin();
            
            var obj = Instantiate(debugging, new Vector3(min.x, 0, min.y), Quaternion.identity);
            obj.GetComponent<Renderer>().material.color = Color.black;

            obj = Instantiate(debugging, new Vector3(max.x, 0, max.y), quaternion.identity);
            obj.GetComponent<Renderer>().material.color = Color.black;

            var xSize = (max.x - min.x) / increment + AmountOfTiles;
            var ySize = (max.y - min.y) / increment + AmountOfTiles;
            
            grid = new NodeBase[(int) xSize, (int) ySize];
            //print(grid.GetLength(0));
            //print(grid.GetLength(1));
            
            var pos = ground[0].transform.position;
            
            int x = 0, z = 0;
            
            for (float i = 0; i < grid.GetLength(0) / AmountOfTiles; i += increment)
                {
                    for (float j = 0; j < grid.GetLength(1) / AmountOfTiles; j += increment)
                    {
                        var walkableBool = IsWalkable(new Vector2(min.x + i, min.y + j));
                        //if (!walkableBool) continue;
                        
                        var node = new NodeBase
                        {
                            position = new Vector2(min.x + i, min.y + j),
                            gridPosition = new Vector2(x, z),
                            walkable = walkableBool,
                            //obj = Instantiate(debugging, new Vector3(min.x + i, 0, min.y + j), Quaternion.identity),
                        };
                        
                        //print("x: " + node.gridPosition.x + " z: " + node.gridPosition.y);
                        
                        //print("xInit: " + xInit + " zInit: " + zInit + " xOut: " + xOut + " zOut: " + zOut);
                        
                        grid[x, z] = node;

                        var nodePos = node.position;
                        var collider = Physics.OverlapSphere(new Vector3(nodePos.x, 0, nodePos.y), increment / 1.5f, obstacleMask);

                        if (collider.Length != 0)
                            node.walkable = false;
                        
                        //if (!node.walkable) node.obj.GetComponent<Renderer>().material.color = Color.red;

                        //yield return new WaitForSeconds(0.05f);
                        
                        z++;
                    }

                    z = 0;
                    x++;
                }

            yield return new WaitForSeconds(0.25f);
        }

        private bool IsWalkable(Vector2 pos)
        {
            var vec3Pos = new Vector3(pos.x, 0, pos.y);
            
            foreach (var floor in ground)
            {
                var floorPos = floor.transform.position;
                if (!(Vector3.Distance(vec3Pos, floorPos) <
                      Vector3.Distance(floor.GetComponent<Renderer>().bounds.min, floor.GetComponent<Renderer>().bounds.max))) continue;

                var collider = Physics.OverlapSphere(new Vector3(pos.x, 0, pos.y), 1f / AmountOfTiles / 4, floorMask);

                return collider.Length != 0;
            }

            return false;
        }

        private Vector2 GetMax()
        {
            var max = Vector2.zero;

            foreach (var floor in ground)
            {
                var bounds = floor.GetComponent<Renderer>().bounds;
                if (bounds.max.x > max.x) max.x = bounds.max.x;
                if (bounds.max.z > max.y) max.y = bounds.max.z;
            }

            return max;
        }
        
        private Vector2 GetMin()
        {
            var min = new Vector2(float.MaxValue, float.MaxValue);

            foreach (var floor in ground)
            {
                var bounds = floor.GetComponent<Renderer>().bounds;
                if (bounds.min.x < min.x) min.x = bounds.min.x;
                if (bounds.min.z < min.y) min.y = bounds.min.z;
            }

            return min;
        }

        /*
            foreach (var floor in ground)
            {
                if (math.abs(floor.transform.position.z - pos.z) > 0.1f)
                {
                    pos = floor.transform.position;
                    
                    xOut = 0;
                    xInit = 0;
                    
                    zOut += tempZ;
                    
                }
                xOut += xInit;

                xInit = 0;
                

                var bounds = floor.GetComponent<Collider>().bounds;
                var size = bounds.size;
                
                for (float i = 0; i < size.x; i += increment)
                {
                    for (float j = 0; j < size.z; j += increment)
                    {
                        var node = new NodeBase
                        {
                            position = new Vector2(bounds.min.x + i, bounds.min.z + j),
                            gridPosition = new Vector2(xOut + xInit, zOut + zInit),
                            obj = Instantiate(debugging, new Vector3(bounds.min.x + i, 0, bounds.min.z + j), Quaternion.identity),
                            walkable = IsWalkable(new Vector2( bounds.min.x + i, bounds.min.z + j))
                        };
                        
                        //print("x: " + node.gridPosition.x + " z: " + node.gridPosition.y);
                        
                        //print("xInit: " + xInit + " zInit: " + zInit + " xOut: " + xOut + " zOut: " + zOut);
                        
                        grid[xOut + xInit, zOut + zInit] = node;

                        var nodePos = node.position;
                        var collider = Physics.OverlapSphere(new Vector3(nodePos.x, 0, nodePos.y), increment, obstacleMask);

                        if (collider.Length != 0)
                            node.walkable = false;
                        
                        if (!node.walkable) node.obj.GetComponent<Renderer>().material.color = Color.red;

                        //yield return new WaitForSeconds(0.25f);
                        
                        zInit++;
                    }

                    tempZ = zInit;
                    zInit = 0;
                    xInit++;
                }
            }
            
            print("x: " + zOut);
            print("z: " + zOut);
            
            yield return new WaitForSeconds(0.25f);
         */
        
        public static NodeBase GetNode(Vector2 position)
        {
            var dist = float.MaxValue;
            var closestNode = new NodeBase();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == null) continue;
                    var node = grid[i, j];
                    var nodePos = node.position;

                    if (dist > Vector2.Distance(nodePos, position))
                    {
                        dist = Vector2.Distance(nodePos, position);
                        closestNode = node;
                    }
                }
            }

            //print(closestNode.gridPosition);
            
            return closestNode;
        }
    }
}