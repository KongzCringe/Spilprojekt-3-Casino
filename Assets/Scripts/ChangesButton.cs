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
        //starter programmet når det er nødvendigt
        if (start == true)
        {
            StartCoroutine("process");
            ;
        }
    }
    //tæller ect der sker mens og når den er færdig med at vente de 5 sec
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
    //dismiss change button når du trykker på den
    public void buttonClick()
    {
        StopAllCoroutines();
        start = false;
        gameObject.SetActive(false);
    }

}
