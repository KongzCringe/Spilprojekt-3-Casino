using System;
using System.Collections;
using System.Collections.Generic;
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

        [Range(1, 32)] [SerializeField] private int amountOfTiles;
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
            if (amountOfTiles != tempAmountOfTiles) GenerateGrid();
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
                    //CreateChildren(new Vector2(bounds.min.x + x, bounds.min.z + z));
                    
                    var node = new NodeBase
                    {
                        position = new Vector3(x, 0, z)
                    };
                    
                    grid[(int) x * amountOfTiles, (int) z * amountOfTiles] = node;

                    Instantiate(debugging, node.position, Quaternion.identity);
                }
            }
        }
        
        private void CreateChildren(Vector2 pos)
        {
            var increment = 1f / amountOfTiles;
            print(increment);

            var node = new NodeBase
            {
                children =
                {
                    
                    new NodeBase
                    {
                        position = new Vector3(pos.x - increment, 0, pos.y - increment)
                    },
                    new NodeBase
                    {
                        position = new Vector3(pos.x + increment, 0, pos.y - increment)
                    },
                    new NodeBase
                    {
                        position = new Vector3(pos.x - increment, 0, pos.y + increment)
                    }
                }
            };
            
            grid[(int) pos.x, (int) pos.y] = node;

            foreach (var child in node.children)
            {
                Instantiate(debugging, child.position, Quaternion.identity);
            }
        }
    }
}