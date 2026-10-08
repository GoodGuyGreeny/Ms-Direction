using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour, I_Interactable
{
    [SerializeField]
    private string localDescriptionText;
    public string DescriptionText { get { return localDescriptionText; } }

    [SerializeField]
    private InventoryItem someKindOfItem;

    public void Interacted(GameObject caller = null)
    {
        Inventory possibleInv = caller.gameObject.GetComponent<Inventory>();
        if (possibleInv != null)
        {
            Debug.Log("In this");
            possibleInv.Add(this.gameObject);
        }
        
    }

    public void BroadcastDescriptionText()
    {

    }

}