using UnityEngine;

public class TestInventoryItem : InventoryItem
{
    public override void Interact(Inventory inventory)
    {
        Debug.Log("Test item used: " + itemName);
    }
}