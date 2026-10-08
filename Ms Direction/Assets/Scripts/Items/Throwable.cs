using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Throwable : MonoBehaviour, I_Interactable
{
    [SerializeField]
    private string localDescriptionText;
    public string DescriptionText { get { return localDescriptionText; } }

    public Rigidbody rb;
    public bool thrown = false;
    [SerializeField]
    float throwMultiplier, upForceMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Interacted(GameObject caller)
    {
        PlayerThrow throwScript = caller.GetComponent<PlayerThrow>();
        if (throwScript != null)
        {
            rb.isKinematic = true;
            throwScript.AttachObject(this);
        }
    }

    public void BroadcastDescriptionText()
    {
        // Later we'll send this through a scriptable object event system
        Debug.Log(localDescriptionText);
    }

    public void Drop()
    {
        rb.isKinematic = false;
    }

    public void Throw(Vector3 throwDirection)
    {
        rb.isKinematic = false;
        thrown = true;
        rb.AddForce(throwDirection * throwMultiplier + Vector3.up * upForceMultiplier, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Player")
            thrown = false;
    }
}
