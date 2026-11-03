using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface I_InventoryItem
{
    public Image Icon { get; }
    public string ItemName { get; }
    public void Interact(Inventory inventory);
}
