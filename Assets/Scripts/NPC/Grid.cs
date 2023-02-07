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
        [SerializeField] private GameObject ground;
        [SerializeField] private GameObject debugging;
        [SerializeField] private LayerMask obstacleMask;

        private const int AmountOfTiles = 2;
        private int _tempAmountOfTiles;
        
        public static NodeBase[,] grid = new NodeBase[10000, 10000];
        
        private List<GameObject> _listOfDebugging = new ();

        private NodeBase _startNode;
        public static NodeBase endNode;
        
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
                    //if (endNode != null) endNode.obj.GetComponent<Renderer>().material.color = Color.gray;
                    endNode = GetNode(hit.point);
                    //endNode.obj.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }

        private void GenerateGrid()
        {
            var x = 0f;

            var increment = 1f / AmountOfTiles;
            
            
            _tempAmountOfTiles = AmountOfTiles;
            var bounds = ground.GetComponent<Collider>().bounds;
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
        }
        
        public static NodeBase GetNode(Vector3 position)
        {
            var x = Mathf.RoundToInt(position.x);
            var z = Mathf.RoundToInt(position.z);
            return grid[x, z];
        }
    }
}