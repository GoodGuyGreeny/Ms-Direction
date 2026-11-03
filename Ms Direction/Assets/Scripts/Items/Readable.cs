using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(PopupSpawner))]
public class Readable : MonoBehaviour, I_Interactable
{
    [SerializeField]
    private string localDescriptionText;
    public string DescriptionText { get { return localDescriptionText; } }
    public void Interacted(GameObject caller = null)
    {
        this.GetComponent<PopupSpawner>().SpawnPopup();
    }

    public void BroadcastDescriptionText()
    {

    }
}
