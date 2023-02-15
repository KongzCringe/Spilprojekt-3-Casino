using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmissionToggler : MonoBehaviour
{
    public List<Material> materialsToToggle;
    private bool isEmissionEnabled = true;
    private int toggleCount = 0;

    private void Start()
    {
        // Start the coroutine to toggle materials
        StartCoroutine(ToggleMaterialsCoroutine());
    }

    private IEnumerator ToggleMaterialsCoroutine()
    {
        while (toggleCount < 10)
        {
            yield return new WaitForSeconds(0.5f);

            // Toggle emission for each material
            foreach (Material mat in materialsToToggle)
            {
                if (isEmissionEnabled)
                {
                    mat.DisableKeyword("_EMISSION");
                    isEmissionEnabled = !isEmissionEnabled;
                }
                
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", isEmissionEnabled ? Color.white : Color.black);
                
                print("done");
            }

            isEmissionEnabled = !isEmissionEnabled;
            toggleCount++;
        }
    }
}