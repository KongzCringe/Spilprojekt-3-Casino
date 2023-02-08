using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private const int AmountOfTiles = 1;
        private int _tempAmountOfTiles;
        
        public static NodeBase[,] grid = new NodeBase[300, 150];
        
        private List<GameObject> _listOfDebugging = new ();

        private NodeBase _startNode;
        public static NodeBase endNode;
        
        private void Start()
        {
            StartCoroutine(GenerateGrid());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    //if (endNode != null) endNode.obj.GetComponent<Renderer>().material.color = Color.gray;
                    endNode = GetNode(hit.point);
                    //endNode.obj.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }

        private IEnumerator GenerateGrid()
        {
            var x = 0f;

            var increment = 1f / AmountOfTiles;
            
            
            _tempAmountOfTiles = AmountOfTiles;
            var bounds = ground[0].GetComponent<Collider>().bounds;
            for (float i = 0; i < grid.GetLength(0); i += increment)
            {
                var z = 0f;
                for (float j = 0; j < grid.GetLength(1); j += increment)
                {
                    yield return null;
                    
                    var node = new NodeBase {position = new Vector2(bounds.min.x + x, bounds.min.z + z)};
                    grid[(int) x, (int) z] = node;

                    var walkable = IsWalkable(new Vector2(i, j));
                    
                    print(walkable);

                    node.walkable = walkable;
                    
                    var nodePos = node.position;
                    
                    var obj = Instantiate(debugging, new Vector3(i, 0, j), Quaternion.identity);
                    node.obj = obj;
                    
                    if (walkable)
                    {
                        
                    }

                    //_listOfDebugging.Add(obj);

                    var collider = Physics.OverlapSphere(new Vector3(nodePos.x, 0, nodePos.y), increment, obstacleMask);

                    if (collider.Length != 0)
                    {
                        node.walkable = false;
                        //node.obj.GetComponent<Renderer>().material.color = Color.black;
                    }

                    z += increment;
                }

                x += increment;
            }
        }

        private bool IsWalkable(Vector2 pos)
        {
            foreach (var floor in ground)
            {
                var bounds = floor.GetComponent<Renderer>().bounds;
                var closestPoint = bounds.ClosestPoint(new Vector3(pos.x, 0, pos.y));
                var distance = Vector3.Distance(floor.transform.position, closestPoint);
                
                if (distance < 0.01f) return true;
            }

            return false;
        }
        
        /*
            var x = 0f;

            var increment = 1f / AmountOfTiles;
            
            
            _tempAmountOfTiles = AmountOfTiles;
            var bounds = ground[0].GetComponent<Collider>().bounds;
            for (float i = 0; i < bounds.max.x - bounds.min.x; i += increment)
            {
                var z = 0f;
                for (float j = 0; j < bounds.max.z - bounds.min.z; j += increment)
                {
                    var node = new NodeBase {position = new Vector2(bounds.min.x + x, bounds.min.z + z)};

                    grid[(int) (bounds.min.x + x), (int) (bounds.min.z + z)] = node;
                    var nodePos = node.position;
                    
                    //var obj = Instantiate(debugging, new Vector3(nodePos.x, 0, nodePos.y), Quaternion.identity);
                    //node.obj = obj;
                    
                    //_listOfDebugging.Add(obj);

                    var collider = Physics.OverlapSphere(new Vector3(nodePos.x, 0, nodePos.y), increment, obstacleMask);

                    if (collider.Length != 0)
                    {
                        node.walkable = false;
                        //node.obj.GetComponent<Renderer>().material.color = Color.black;
                    }

                    z += increment;
                }

                x += increment;
            }
         */
        
        public static NodeBase GetNode(Vector3 position)
        {
            var x = Mathf.RoundToInt(position.x);
            var z = Mathf.RoundToInt(position.z);
            return grid[x, z];
        }
    }
}