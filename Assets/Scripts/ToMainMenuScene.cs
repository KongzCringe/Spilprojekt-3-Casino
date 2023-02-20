using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenuScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(10.2f);

            SceneManager.LoadScene(2);
        }
    

   
}
