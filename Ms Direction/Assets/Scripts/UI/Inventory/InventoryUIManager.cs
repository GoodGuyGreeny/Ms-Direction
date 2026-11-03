
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [Header("Player Inventory")]
    [SerializeField] private Inventory inventory;

    [Header("Regular Inventory Slots")]
    [SerializeField] private InventorySlotUI[] slots =
        new InventorySlotUI[8];

    [Header("Employee Access Card")]
    [SerializeField] private InventorySlotUI accessCardSlot;

    private void Update()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (inventory == null)
            return;

        InventorySlot[] inventorySlots =
            inventory.GetInventorySlotsList();

        if (inventorySlots == null)
            return;

        int activeIndex = inventory.ActiveIndex;

        // Update regular inventory slots.
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
                continue;

            if (i >= inventorySlots.Length)
            {
                slots[i].SetItem(null);
                slots[i].SetSelected(false);
                continue;
            }

            I_InventoryItem item = inventorySlots[i].item;

            Sprite icon = null;

            if (item != null)
            {
                Debug.Log("Inventory item: " + item.ItemName);
            
                if (item.Icon == null)
                {
                    Debug.LogWarning("Item has no Image component assigned!");
                }
                else if (item.Icon.sprite == null)
                {
                    Debug.LogWarning("Item Image exists, but has no sprite!");
                }
                else
                {
                    icon = item.Icon.sprite;
                    Debug.Log("Icon found: " + icon.name);
                }
            }

            slots[i].SetItem(icon);
            slots[i].SetSelected(i == activeIndex);
        }

        // Update the permanent access card slot.
        if (accessCardSlot != null && inventorySlots.Length > 8)
        {
            I_InventoryItem card = inventorySlots[8].item;

            Sprite cardIcon = null;

            if (card != null && card.Icon != null)
                cardIcon = card.Icon.sprite;

            accessCardSlot.SetItem(cardIcon);
            accessCardSlot.SetSelected(false);
        }
    }
}
