using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    private Material material;
    public Shader shader;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(shader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
