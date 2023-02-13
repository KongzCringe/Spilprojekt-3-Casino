using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangesButton : MonoBehaviour
{
    public bool start;
    [SerializeField] TMP_Text text;
    [SerializeField] FullscreenScript settingScript;
    [SerializeField] TMP_Dropdown settingObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //starter programmet n�r det er n�dvendigt
        if (start == true)
        {
            StartCoroutine("process");
            ;
        }
    }
    //t�ller ect der sker mens og n�r den er f�rdig med at vente de 5 sec
    IEnumerator process()
    {
        start = false;
        for (int i = 1; i < 6; i++)
        {
            text.text = "Keep changes? " + (6 - i);
            yield return new WaitForSeconds(1);
        }
        settingScript.changeButton = true;
        settingObject.value = (settingScript.oldState - 1);
        settingScript.changed = true;
        settingScript.state = settingScript.oldState;
        gameObject.SetActive(false);
    }
    //dismiss change button n�r du trykker p� den
    public void buttonClick()
    {
        StopAllCoroutines();
        start = false;
        gameObject.SetActive(false);
    }

}
