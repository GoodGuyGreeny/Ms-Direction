using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// THIS IS AN ABSTRACT SUPERCLASS
// I will (obviously) be using these sparingly, but it felt "more correct" than an interface.
// Actual implementation will take place in subclasses (ex. there is a CoffeeInteract script,
// there is a StickyNoteInteract script, etc.)
public abstract class InventoryItem : MonoBehaviour
{
    public Image icon;
    public string itemName;
    protected void SetupInventoryItem(Image i, string n)
    {
        icon = i;
        itemName = n;
    }

    public abstract void Interact();
}
