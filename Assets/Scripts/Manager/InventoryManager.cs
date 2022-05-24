using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    public InventoryUI invenUI;
    
    private void Awake() {
        instance = this;
    }
}
