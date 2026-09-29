using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public Transform camPos;
    public PlayerInteractor playerInteractor;
    public LayerMask collisionDetection;
    Throwable attachedThrow;

    bool interactPressed, throwPressed;
    [SerializeField]
    float offset;
    [SerializeField]
    float lookSpeed;

    void Update()
    {
        if (attachedThrow != null)
            HandleInput();
    }

    private void FixedUpdate()
    {
        if(attachedThrow != null)
        {
            HandleHoldMovement();
            if (interactPressed)
            {
                attachedThrow.Drop();
                DetachObject();
            }
            else if (throwPressed)
            {
                Vector3 throwDir = camPos.transform.TransformDirection(Vector3.forward);
                attachedThrow.Throw(throwDir);
                DetachObject();
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Interact"))
            interactPressed = true;
        if (Input.GetButtonDown("Throw"))
            throwPressed = true;
    }

    void HandleHoldMovement()
    {
        float fixDistance = offset;
        RaycastHit hit;
        //Rudamentary, but serves the purpose of emulating collisions well enough
        if(Physics.Raycast(camPos.transform.position, camPos.transform.TransformDirection(Vector3.forward), out hit, offset, collisionDetection))
        {
            fixDistance = hit.distance;
        }

        Debug.DrawRay(camPos.transform.position, camPos.transform.TransformDirection(Vector3.forward) * fixDistance, Color.white);
        Vector3 holdPos = camPos.transform.position + camPos.transform.TransformDirection(Vector3.forward) * fixDistance;
        attachedThrow.rb.MovePosition(Vector3.Lerp(attachedThrow.gameObject.transform.position, holdPos, Time.fixedDeltaTime * lookSpeed));
    }

    public void AttachObject(Throwable attach)
    {
        attachedThrow = attach;
        playerInteractor.enabled = false;
        Physics.IgnoreLayerCollision(3, 7, true);
    }
    
    public void DetachObject()
    {
        attachedThrow = null;
        playerInteractor.enabled = true;
        Physics.IgnoreLayerCollision(3, 7, false);

        interactPressed = false;
        throwPressed = false;
    }
}
