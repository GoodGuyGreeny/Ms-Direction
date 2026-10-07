using UnityEngine;

public class TestInventoryItem : InventoryItem
{
    public override void Interact()
    {
        Debug.Log("Test item used: " + itemName);
    }
}