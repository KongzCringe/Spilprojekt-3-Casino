using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = System.Random;

namespace Script
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GameObject ground;
        [SerializeField] private GameObject debugging;

        private static int amountOfTiles = 1;
        private int tempAmountOfTiles;
        
        public static NodeBase[,] grid = new NodeBase[10000, 10000];
        
        private List<GameObject> listOfDebugging = new ();

        private NodeBase startNode;
        private NodeBase endNode;
        
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
                    startNode = GetNode(hit.point);
                    startNode.obj.GetComponent<Renderer>().material.color = Color.green;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    endNode = GetNode(hit.point);
                    endNode.obj.GetComponent<Renderer>().material.color = Color.black;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && startNode != null && endNode != null)
            {
                var path = PathFinding.FindPath(startNode, endNode);

                foreach (var node in path)
                {
                    node.obj.GetComponent<Renderer>().
                }
            }
        }

        private void GenerateGrid()
        {
            foreach (var obj in listOfDebugging)
            {
                Destroy(obj);
            }
            tempAmountOfTiles = amountOfTiles;
            var bounds = ground.GetComponent<Collider>().bounds;
            for (float x = 0; x < bounds.max.x - bounds.min.x; x += 1f / amountOfTiles)
            {
                for (float z = 0; z < bounds.max.z - bounds.min.z; z += 1f / amountOfTiles)
                {
                    var node = new NodeBase
                    {
                        position = new Vector3(bounds.min.x + x, 0, bounds.min.z + z)
                    };
                    
                    grid[(int) (bounds.min.x + x) * amountOfTiles, (int) (bounds.min.z + z) * amountOfTiles] = node;

                    var obj = Instantiate(debugging, node.position, Quaternion.identity);
                    node.obj = obj;
                    
                    listOfDebugging.Add(obj);
                }
            }
        }
        
        public static NodeBase GetNode(Vector3 position)
        {
            var x = Mathf.RoundToInt(position.x) * amountOfTiles;
            var z = Mathf.RoundToInt(position.z) * amountOfTiles;
            return grid[x, z];
        }
    }
}