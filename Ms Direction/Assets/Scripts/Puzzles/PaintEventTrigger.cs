using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintEventTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.instance.Invoke2DEventPopup();
    }
}
