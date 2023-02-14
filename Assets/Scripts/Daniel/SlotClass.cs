using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotClass : MonoBehaviour
{
    [SerializeField] private int slotTier;
    
    public int GetSlotTier()
    {
        return slotTier;
    }
}
