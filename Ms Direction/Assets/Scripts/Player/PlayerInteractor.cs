using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Transform camPos;
    public float interactDistance;
    bool interactPressed = false, interactAllowed = true;

    void Update()
    {
        interactPressed = Input.GetButtonDown("Interact");

        Vector3 rayLocation = camPos.transform.position;
        Debug.DrawLine(camPos.transform.position, camPos.transform.position + camPos.transform.TransformDirection(Vector3.forward) * interactDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(camPos.transform.position, camPos.transform.TransformDirection(Vector3.forward), out hit, interactDistance))
        {
            I_Interactable interactable = hit.collider.gameObject.GetComponent<I_Interactable>();
            if (interactable != null)
                InteractionWithObject(interactable);
        }
    }

    // Called when a valid object is interacted with.
    // Broadcasts the description text of the object and calls the objects interacted event.
    //
    // @param interactable Object with I_Interactable interface attached
    void InteractionWithObject(I_Interactable interactable)
    {
        interactable.BroadcastDescriptionText();
        if (interactPressed && interactAllowed)
        {
            interactable.Interacted(this.gameObject);
            interactAllowed = false;
            Invoke(nameof(ResetInteract), 0.2f);
        }
    }

    // Called to reset the interactAllowed boolean.
    // Helps add a buffer to interactions.
    void ResetInteract()
    {
        interactAllowed = true;
    }
}