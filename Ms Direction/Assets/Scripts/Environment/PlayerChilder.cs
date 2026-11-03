using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE:
// This is basically just Brian Winn's script from MI445.
// Credit goes to Michigan State University.


// In order for us to ensure that the player moves with this object
// it's probably best practice to make sure whatever we attach this to has
// some kind of collider.
[RequireComponent(typeof(Collider))]
public class PlayerChilder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
