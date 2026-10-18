using UnityEngine;
using UnityEngine.UI;

public class TestInventoryItem : MonoBehaviour, I_InventoryItem
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private string itemName;
    public Image Icon { get { return icon; } }
    public string ItemName { get { return itemName; } }
    public void Interact(Inventory inventory)
    {
        Debug.Log("Test item used: " + itemName);
    }
}