using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private const int MAX_SLOTS = 9;

    [Tooltip("The index of the current active item in the inventory list")]
    private int activeIndex = 0;
    // The actual listing of items within our inventory
    private InventorySlot[] inventoryList;
    // An updated list of the most recently accessed/used items in our inventory
    private InventorySlot[] recencyList;

    // Start is called before the first frame update
    void Start()
    {
        inventoryList = new InventorySlot[MAX_SLOTS];
        recencyList = new InventorySlot[MAX_SLOTS];

        for (int i = 0; i < MAX_SLOTS; i++)
        {
            inventoryList[i] = new InventorySlot();
            Debug.Log("Created slot " + i);
        }

        inventoryList[MAX_SLOTS - 1].mutable = false;


    }

    // Adds an item into our inventoryList
    public void Add(GameObject obj)
    {
        // HERE: 
        // 1. Get InventoryItem script from the passed object (MOST interactable items should have a prefab
        // attached to them, that is essentially the actual IMPLEMENTATION of whatever that item 
        // is supposed to actually spawn. For example, let's say we have a pair of scissors sitting on 
        // a desk in our game. The scissors object in our game will contain an interactable script that has an attached
        // prefab of the actual scissors object that we will use. The prefab is what should be passed to this function.)
        //
        // 2. Put InventoryItem script into InventorySlot container, then place into the end of inventory list,
        // calling Replace() if it is full.

        InventoryItem newItem = obj.GetComponent<InventoryItem>();
        bool itemAdded = false;

        for (int i = 0; i < MAX_SLOTS - 1; i++)
        {
            if (inventoryList[i].item == null)
            {
                inventoryList[i].item = newItem;
                itemAdded = true;
                Debug.Log("Item [" + newItem.name + "] added to inventory slot " + i);
                obj.SetActive(false);
                break;
            }
        }

        if (itemAdded == false)
        {
            Replace(obj);
        }
    }

    // Removes an item from our inventoryList
    void Remove(GameObject obj = null)
    {
        if (obj == null)
        {
            // Remove the currently active item, indicated by activeIndex.
            if (inventoryList[activeIndex].item != null && inventoryList[activeIndex].mutable == true)
            {
                inventoryList[activeIndex].Empty();
            }
            return;
        }
        // Else, remove the passed item
        else
        {
            // Add code
            InventoryItem itemToRemove = obj.GetComponent<InventoryItem>();

            for (int i = 0; i < MAX_SLOTS - 1; i++)
            {
                if (inventoryList[i].item.itemName == itemToRemove.itemName)
                {
                    inventoryList[i].Empty();
                    break;
                }
            }
            return;
        }
    }

    // Replaces an item within our inventoryList
    void Replace(GameObject obj)
    {
        // Do we want to use the recency list?
        // Or would it be better that this is just called on the activeItem index
        // and the current activeItem is replaced with the added item?
        // I think the second way is better and more common, but feel free to do either.
        if (inventoryList[activeIndex].item != null && inventoryList[activeIndex].mutable == true)
        {
            InventoryItem newItem = obj.GetComponent<InventoryItem>();
            inventoryList[activeIndex].item = newItem;
            Debug.Log("Replaced item at index " + activeIndex);
        }
    }

    // Change Active Item
    // @param i index to set as active
    void SetActiveItem(int i)
    {
        activeIndex = i;
    }

    // return list of inventory slots
    InventorySlot[] GetInventorySlotsList()
    {
        return inventoryList;
    }
}

public class InventorySlot
{
    public bool mutable = true;
    public InventoryItem item;

    public void ToggleObject()
    {
        item.gameObject.SetActive(!item.gameObject.activeSelf);
    }

    public void Empty()
    {
        item = null;
    }
}
