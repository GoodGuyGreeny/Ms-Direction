using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrenchItem : MonoBehaviour, I_InventoryItem
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private string itemName;
    public Image Icon { get { return icon; } }
    public string ItemName { get { return itemName; } }

    public void Interact(Inventory inventory)
    {

    }
}
