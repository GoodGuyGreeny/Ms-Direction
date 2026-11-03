using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour, I_Interactable
{
    [Header("Effects")]
    [SerializeField] private GameObject pickupEffect;

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

            if (pickupEffect == null)
            {
                Debug.Log("No step sound effect applied!");
            }
            else
            {
                Instantiate(pickupEffect);
                AudioManager.instance.ApplyVolume(pickupEffect.GetComponent<AudioSource>(), AudioManager.AudioType.SFX);
            }
        }
        
    }

    public void BroadcastDescriptionText()
    {
        
    }

}