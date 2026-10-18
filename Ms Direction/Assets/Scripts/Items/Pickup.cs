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
    private GameObject inventoryItem;

    public void Interacted(GameObject caller = null)
    {
        Inventory possibleInv = caller.gameObject.GetComponent<Inventory>();
        if (possibleInv != null)
        {
            Debug.Log("In this");
            possibleInv.Add(inventoryItem);
            this.gameObject.SetActive(false);
        }
        
    }

    public void BroadcastDescriptionText()
    {

    }

}