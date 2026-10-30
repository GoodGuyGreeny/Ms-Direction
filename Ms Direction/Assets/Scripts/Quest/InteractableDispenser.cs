using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableDispenser : MonoBehaviour, I_Interactable
{
    [Header("Interaction")]
    [SerializeField] private string localDescriptionText = "Take Item";

    public string DescriptionText => localDescriptionText;

    [Header("Dispenser Settings")]
    [SerializeField] private GameObject itemPrefab;

    public void Interacted(GameObject caller = null)
    {
        if (caller == null)
            return;

        Inventory inventory = caller.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.LogWarning("Dispenser: Player has no Inventory.");
            return;
        }

        if (itemPrefab == null)
        {
            Debug.LogWarning("Dispenser: No item prefab assigned.");
            return;
        }

        if (itemPrefab.GetComponent<I_InventoryItem>() == null)
        {
            Debug.LogWarning("Dispenser: Item prefab must implement I_InventoryItem.");
            return;
        }

        // Create a unique instance for the inventory.
        GameObject newItem = Instantiate(itemPrefab);

        // Add it using the existing inventory system.
        inventory.Add(newItem);

        Debug.Log("Dispensed: " + itemPrefab.name);
    }

    public void BroadcastDescriptionText()
    {
        Debug.Log(localDescriptionText);
    }
}