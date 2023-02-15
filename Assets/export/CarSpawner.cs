using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] Cars;
    public int maxRange;
    
    [SerializeField] private GameObject Cam;
    
    // Start is called before the first frame update
    void Start()
    {
        Cam.SetActive(false);
        
        Cam.SetActive(true);
        
        StartCoroutine(SpawnRandomObject());
    }

    IEnumerator SpawnRandomObject()
    {
        while (true)
        {
            int whichCar = Random.Range (0, maxRange);
        
            GameObject myObj = Instantiate (Cars [whichCar],transform.position, transform.rotation * Quaternion.Euler(0f,0f,0f )) as GameObject;

            myObj.transform.position = transform.position;
            
        
            yield return new WaitForSeconds(Random.Range(4, 6));
            
        }
    }
}
