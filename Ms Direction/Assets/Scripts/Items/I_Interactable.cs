using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Interactable
{
    string DescriptionText { get; }

    // Function that will be called when this object has been interacted with.
    // @param caller The gameobject that interacted with this object
    void Interacted(GameObject caller = null);

    // Used to send the description text to the UI/UIManager.
    void BroadcastDescriptionText();
}
