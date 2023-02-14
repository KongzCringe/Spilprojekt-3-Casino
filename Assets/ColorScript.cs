using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorScript : MonoBehaviour
{
    Volume PP;
    VolumeProfile PPProfile;
    UnityEngine.Rendering.Universal.ColorAdjustments Adjustments;
    [SerializeField] GameObject moneyObject;
    float money;
    // Start is called before the first frame update
    void Start()
    {
        PP = gameObject.GetComponent<Volume>(); 
        PPProfile = PP.profile;
        if (!PPProfile.TryGet(out Adjustments)) throw new System.NullReferenceException(nameof(Adjustments));
        money = MoneyScript.moneyCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        Adjustments.saturation.Override((money/10000)*2);
    }
}
